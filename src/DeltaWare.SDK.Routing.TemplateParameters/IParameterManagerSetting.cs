namespace DeltaWare.SDK.Routing.TemplateParameters
{
    public interface IParameterManagerSetting
    {
        char ParameterStartDelimiter { get; }
        char ParameterEndDelimiter { get; }
        char ParameterOptionalKey { get; }
        char SectionSeparator { get; }
    }
}
