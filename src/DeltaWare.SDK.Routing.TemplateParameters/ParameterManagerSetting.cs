namespace DeltaWare.SDK.Routing.TemplateParameters
{
    public class ParameterManagerSetting : IParameterManagerSetting
    {
        public char ParameterStartDelimiter { get; set; }
        public char ParameterEndDelimiter { get; set; }
        public char ParameterOptionalKey { get; set; }
        public char SectionSeparator { get; set; }

        public static IParameterManagerSetting Default => new ParameterManagerSetting
        {
            ParameterStartDelimiter = '{',
            ParameterEndDelimiter = '}',
            ParameterOptionalKey = '?',
            SectionSeparator = '/'
        };
    }
}
