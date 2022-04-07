using DeltaWare.SDK.Core.Caching;
using DeltaWare.SDK.Serialization.Csv.Header;
using DeltaWare.SDK.Serialization.Types;

namespace DeltaWare.SDK.Serialization.Csv
{
    public partial class CsvSerializer : ICsvSerializer
    {
        private readonly IAttributeCache _attributeCache;
        private readonly HeaderHandler _headerHandler;
        private readonly IObjectSerializer _objectSerializer;

        public CsvSerializer(IObjectSerializer objectSerializer = null)
        {
            _attributeCache = new AttributeCache();
            _objectSerializer = objectSerializer ?? new ObjectSerializer(_attributeCache);

            _headerHandler = new HeaderHandler(_attributeCache, _objectSerializer);
        }
    }
}