using DeltaWare.SDK.Core.Transformation.Transformers;
using System;
using System.Collections.Concurrent;

namespace DeltaWare.SDK.Core.Transformation
{
    public class TransformerCollection
    {
        private readonly ConcurrentDictionary<Type, ITransformer> _nullableTransformers = new();
        private readonly ConcurrentDictionary<Type, ITransformer> _transformers = new();

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
                AddTransformer(transformer);
            }
        }

        public void AddTransformer(ITransformer transformer)
        {
            if (transformer is INullableTransformer)
            {
                _nullableTransformers.TryAdd(transformer.Type, transformer);
            }
            else
            {
                _transformers.TryAdd(transformer.Type, transformer);
            }
        }

        public ITransformer GetTransformer(Type type)
        {
            if (_transformers.TryGetValue(type, out ITransformer transformer))
            {
                return transformer;
            }

            if (_nullableTransformers.TryGetValue(type, out transformer)) { return transformer; }

            if (type.BaseType != null)
            {
                if (_transformers.TryGetValue(type.BaseType, out transformer)) { return transformer; }

                if (_nullableTransformers.TryGetValue(type.BaseType, out transformer))
                {
                    return transformer;
                }
            }

            type = Nullable.GetUnderlyingType(type);

            return _nullableTransformers[type];
        }

        public T GetTransformer<T>() where T : ITransformer
        {
            return (T)GetTransformer(typeof(T));
        }

        public bool HasTransformer(Type type)
        {
            if (_transformers.ContainsKey(type))
            {
                return true;
            }

            if (_nullableTransformers.ContainsKey(type)) { return true; }

            if (type.BaseType != null)
            {
                if (_transformers.ContainsKey(type.BaseType)) { return true; }

                if (_nullableTransformers.ContainsKey(type.BaseType)) { return true; }
            }

            type = Nullable.GetUnderlyingType(type);

            if (type == null) { return false; }

            return _nullableTransformers.ContainsKey(type);
        }

        public bool HasTransformer<T>()
        {
            return HasTransformer(typeof(T));
        }

        public bool TryGetTransformer(Type type, out ITransformer transformer)
        {
            if (_transformers.TryGetValue(type, out transformer))
            {
                return true;
            }

            if (_nullableTransformers.TryGetValue(type, out transformer)) { return true; }

            if (type.BaseType != null)
            {
                if (_transformers.TryGetValue(type.BaseType, out transformer)) { return true; }

                if (_nullableTransformers.TryGetValue(type.BaseType, out transformer))
                {
                    return true;
                }
            }

            type = Nullable.GetUnderlyingType(type);

            if (type == null)
            {
                return false;
            }

            return _nullableTransformers.TryGetValue(type, out transformer);
        }

        public bool TryGetTransformer<T>(out T transformer)
        {
            bool hasTransformer = TryGetTransformer(typeof(T), out ITransformer foundTransformer);

            transformer = (T)foundTransformer;

            return hasTransformer;
        }
    }
}