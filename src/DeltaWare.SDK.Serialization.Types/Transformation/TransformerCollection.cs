using DeltaWare.SDK.Serialization.Types.Transformation.Transformers;
using System;
using System.Collections.Generic;

namespace DeltaWare.SDK.Serialization.Types.Transformation
{
    public class TransformerCollection : Dictionary<Type, ITransformer>
    {
        public static TransformerCollection DefaultCollection { get; } = new
        (
            new StringTransformer(),
            new CharTransformer(),
            new BoolTransformer(),
            new ShortTransformer(),
            new IntTransformer(),
            new LongTransformer(),
            new FloatTransformer(),
            new DecimalTransformer(),
            new DateTimeTransformer(),
            new TimeSpanTransformer(),
            new EnumTransformer()
        );

        public TransformerCollection(params ITransformer[] transformers)
        {
            foreach (ITransformer transformer in transformers)
            {
                Add(transformer);
            }
        }

        public void Add(ITransformer transformer)
        {
            Add(transformer.Type, transformer);
        }
    }
}