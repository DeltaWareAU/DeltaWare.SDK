// ReSharper disable once CheckNamespace
namespace System
{
    public static class ArrayExtensions
    {
        public static IArrayItemAddition<T> Resize<T>(this T[] source, int newSize)
            => new ArrayResizeHandler<T>(source, newSize);

        private class ArrayResizeHandler<T> : IArrayItemAddition<T>
        {
            private readonly T[] _source;
            private readonly int _newSize;

            private Action<T> _onRemoval;
            private Func<T>? _onAddition;
            private Func<int, T>? _onAdditionWithIndex;

            public ArrayResizeHandler(T[] source, int newSize)
            {
                _source = source;
                _newSize = newSize;
            }

            public T[] GetArray()
            {
                if (_newSize == _source.Length)
                {
                    return _source;
                }

                T[] newArray = new T[_newSize];

                if (newArray.Length > _source.Length)
                {
                    for (int i = 0; i < _source.Length; i++)
                    {
                        newArray[i] = _source[i];
                    }

                    for (int i = _source.Length; i < newArray.Length; i++)
                    {
                        if (_onAddition != null)
                        {
                            newArray[i] = _onAddition.Invoke();
                        }
                        else if (_onAdditionWithIndex != null)
                        {
                            newArray[i] = _onAdditionWithIndex.Invoke(i);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }

                    return newArray;
                }

                for (int i = 0; i < newArray.Length; i++)
                {
                    newArray[i] = _source[i];
                }

                for (int i = newArray.Length; i < _source.Length; i++)
                {
                    _onRemoval.Invoke(_source[i]);
                }

                return newArray;
            }

            public IArrayResizeFinalizer<T> OnRemoval(Action<T> action)
            {
                _onRemoval = action;

                return this;
            }

            public IArrayItemRemoval<T> OnAddition(Func<T> action)
            {
                _onAddition = action;

                return this;
            }

            public IArrayItemRemoval<T> OnAddition(Func<int, T> action)
            {
                _onAdditionWithIndex = action;

                return this;
            }
        }
    }

    public interface IArrayResizeFinalizer<out T>
    {
        T[] GetArray();
    }

    public interface IArrayItemAddition<T> : IArrayItemRemoval<T>
    {
        IArrayItemRemoval<T> OnAddition(Func<T> action);
        IArrayItemRemoval<T> OnAddition(Func<int, T> action);
    }

    public interface IArrayItemRemoval<out T> : IArrayResizeFinalizer<T>
    {
        IArrayResizeFinalizer<T> OnRemoval(Action<T> action);
    }
}
