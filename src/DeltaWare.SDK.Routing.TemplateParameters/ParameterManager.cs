using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeltaWare.SDK.Serialization.Types;
using DeltaWare.SDK.Serialization.Types.Attributes;

namespace DeltaWare.SDK.Routing.TemplateParameters
{
    public class ParameterManager
    {
        private readonly IParameterManagerSetting _setting;

        private readonly IObjectSerializer _objectSerializer;

        public ParameterManager(IObjectSerializer objectSerializer, IParameterManagerSetting setting = null)
        {
            _objectSerializer = objectSerializer ?? throw new ArgumentNullException(nameof(objectSerializer));
            _setting = setting ?? ParameterManagerSetting.Default;
        }

        public object BindQuery(ParameterInfo parameter, string query)
        {
            Dictionary<string, string> queryDictionary = BuildQueryDictionary(query);

            object queryObject = Activator.CreateInstance(parameter.ParameterType);

            foreach (PropertyInfo property in parameter.ParameterType.GetPublicProperties())
            {
                string name = property.Name;

                NameAttribute nameAttribute = property.GetCustomAttribute<NameAttribute>();

                if (nameAttribute != null)
                {
                    name = nameAttribute.Value;
                }

                if (!queryDictionary.TryGetValue(name, out string value))
                {
                    continue;
                }

                object propertyValue = _objectSerializer.Deserialize(value, property);

                property.SetValue(queryObject, propertyValue);
            }

            return queryObject;
        }

        private Dictionary<string, string> BuildQueryDictionary(string query)
        {
            return query
                .Split('&')
                .Select(querySection => querySection.Split('='))
                .ToDictionary(queryKvp => queryKvp[0], queryKvp => queryKvp[1]);
        }

        public void BindParameters(Dictionary<ParameterInfo, object> parameters, string template, string endpoint)
        {
            string[] templateSections = template.Split(_setting.SectionSeparator);
            string[] endpointSections = endpoint.Split(_setting.SectionSeparator);

            for (int i = 0; i < templateSections.Length; i++)
            {
                string templateSection = templateSections[i];

                if (!IsParameter(templateSection))
                {
                    continue;
                }

                ParameterDetails parameter = GetParameterDetails(templateSection);

                if (endpointSections.Length - 1 < i)
                {
                    if (parameter.Optional)
                    {
                        continue;
                    }

                    // TODO: Use better exception.
                    throw new Exception();
                }

                string value = endpointSections[i];

                if (string.IsNullOrWhiteSpace(value))
                {
                    if (parameter.Optional)
                    {
                        continue;
                    }

                    // TODO: Use better exception.
                    throw new Exception();
                }

                ParameterInfo parameterInfo = parameters.SingleOrDefault(p => p.Key.Name == parameter.Key).Key;

                if (parameterInfo == null)
                {
                    // TODO: Use better exception.
                    throw new Exception();
                }

                parameters[parameterInfo] = _objectSerializer.Deserialize(value, parameterInfo.ParameterType);
            }
        }

        private ParameterDetails GetParameterDetails(string templateSection)
        {
            string parameterKey = GetParameterKey(templateSection);
            
            if (IsParameterOptional(parameterKey))
            {
                return new ParameterDetails(parameterKey[..^1], true);
            }

            return new ParameterDetails(parameterKey, false);
        }

        private bool IsParameter(string value)
        {
            return value.StartsWith(_setting.ParameterStartDelimiter) && value.EndsWith(_setting.ParameterEndDelimiter);
        }

        private bool IsParameterOptional(string value)
        {
            if (value.EndsWith(_setting.ParameterEndDelimiter))
            {
                return value[^2] == _setting.ParameterOptionalKey;
            }

            return value.EndsWith(_setting.ParameterOptionalKey);
        }

        private string GetParameterKey(string value)
        {
            if (!IsParameter(value))
            {
                throw new ArgumentException($"The specified value \"{value}\" is not a parameter");
            }

            // Remove first characters from start and end of string.
            // EG "{MyParameter}" will be turned into "MyParameter"
            return value[1..^1];
        }

        private string RemoveOptionalKey(string value)
        {
            if (value.EndsWith(_setting.ParameterOptionalKey))
            {
                return value[..^1];
            }

            return value;
        }
    }
}
