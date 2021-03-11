using DeltaWare.SDK.Serialization.Arguments.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeltaWare.SDK.Serialization.Arguments
{
    /// <summary>
    /// Parses arguments assigning converting and assigning them to .net objects.
    /// </summary>
    public static class ArgumentParser
    {
        /// <summary>
        /// Parses command lines arguments. Assigning them to static properties.
        /// </summary>
        /// <typeparam name="T">The static class that will have its static properties assigned too.</typeparam>
        /// <param name="arguments">The arguments to be deserialized.</param>
        public static void Parse<T>(string[] arguments) where T : class
        {
            Dictionary<string, Argument> nameToArgumentMap = CreatePropertyMap(typeof(T).GetProperties());

            bool expectingParameter = false;

            Argument currentArgument = new Argument();

            string argumentBuilder = string.Empty;

            foreach(string argument in arguments)
            {
                if(!expectingParameter && argument[0] == '-')
                {
                    if(!string.IsNullOrEmpty(argumentBuilder))
                    {
                        if(currentArgument.ArgumentType is Parameter)
                        {
                            currentArgument.Property.SetValue(null, Convert.ChangeType(argumentBuilder, currentArgument.Property.PropertyType));

                            argumentBuilder = string.Empty;
                        }
                        else
                        {
                            throw new ArgumentException($"You can't assign a parameter to the flag [{currentArgument.ArgumentType.Name}].");
                        }
                    }

                    if(argument[argument.Length - 1] == ':')
                    {
                        string argumentName = argument.Substring(1, argument.Length - 2);

                        if(!nameToArgumentMap.TryGetValue(argumentName, out currentArgument))
                        {
                            throw new ArgumentNullException($"The parameter [-{argumentName}:] cannot be found in [{typeof(T).Name}].");
                        }

                        if(!(currentArgument.ArgumentType is Parameter))
                        {
                            throw new ArgumentException($"The flag [{currentArgument.ArgumentType.Name}] ends with [:]. Only parameters can end with [:].");
                        }

                        expectingParameter = true;
                    }
                    else
                    {
                        string argumentName = argument.Substring(1);

                        if(!nameToArgumentMap.TryGetValue(argumentName, out currentArgument))
                        {
                            throw new ArgumentNullException($"The flag [-{argumentName}] cannot be found in [{typeof(T).Name}].");
                        }

                        if(!(currentArgument.ArgumentType is Flag))
                        {
                            throw new ArgumentException("The parameter [{currentArgument.ArgumentType.Name}] doesn't end with [:].");
                        }

                        currentArgument.Property.SetValue(null, true);
                    }
                }
                else
                {
                    if(!string.IsNullOrEmpty(argumentBuilder))
                    {
                        argumentBuilder += ' ';
                    }

                    argumentBuilder += argument;

                    expectingParameter = false;
                }
            }

            if(!string.IsNullOrEmpty(argumentBuilder))
            {
                if(currentArgument.ArgumentType is Flag)
                {
                    throw new ArgumentException($"You can't assign a parameter to the flag [{currentArgument.ArgumentType.Name}].");
                }

                currentArgument.Property.SetValue(null, Convert.ChangeType(argumentBuilder, currentArgument.Property.PropertyType));
            }
        }

        /// <summary>
        /// Creates a Parameter Property Map based off of the properties in T.
        /// </summary>
        /// <param name="properties"> An array of <see cref="PropertyInfo"> that the map will be created from. </see> </param>
        /// <returns> Returns the Parameter Property map. </returns>
        private static Dictionary<string, Argument> CreatePropertyMap(PropertyInfo[] properties)
        {
            Dictionary<string, Argument> nameToArgumentMap = new Dictionary<string, Argument>();

            foreach(PropertyInfo property in properties)
            {
                if(Attribute.IsDefined(property, typeof(ArgumentBase)))
                {
                    ArgumentBase argument = (ArgumentBase)property.GetCustomAttributes(typeof(ArgumentBase), false).First();

                    if(argument is Flag)
                    {
                        if(property.PropertyType != typeof(bool))
                        {
                            throw new ArgumentException($"The flag [{argument.Name}] is not a boolean type.");
                        }
                    }

                    if(string.IsNullOrEmpty(argument.Name))
                    {
                        argument.Name = property.Name;
                    }

                    nameToArgumentMap.Add(argument.Name, new Argument(argument, property));
                }
            }

            return nameToArgumentMap;
        }
    }
}
