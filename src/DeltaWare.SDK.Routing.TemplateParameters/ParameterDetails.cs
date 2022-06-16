namespace DeltaWare.SDK.Routing.TemplateParameters
{
    public struct ParameterDetails
    {
        public bool Optional { get; set; }

        public string Key { get; set; }

        public ParameterDetails(string key, bool optional)
        {
            Key = key;
            Optional = optional;
        }
    }
}
