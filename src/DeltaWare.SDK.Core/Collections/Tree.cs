using System;
using System.Collections.Generic;
using System.Linq;

namespace DeltaWare.SDK.Core.Collections
{
    /// <inheritdoc/>
    public class Tree<T> : ITreeNode<T>
    {
        private readonly List<ITreeNode<T>> _childNodes = new List<ITreeNode<T>>();

        /// <summary>
        /// Creates a new instance of <see cref="Tree{T}"/>
        /// </summary>
        /// <param name="value">Sets the value.</param>
        /// <remarks>The Parent Node will be set to <see langword="null"/>.</remarks>
        public Tree(T value)
        {
            Value = value;
        }

        private Tree(ITreeNode<T> parentNode, T value) : this(value)
        {
            ParentNode = parentNode;
        }

        /// <inheritdoc/>
        public ITreeNode<T> ParentNode { get; }

        /// <inheritdoc/>
        public IReadOnlyList<ITreeNode<T>> ChildNodes => _childNodes;

        /// <inheritdoc/>
        public T Value { get; }

        /// <inheritdoc/>
        public ITreeNode<T> AddChild(T value)
        {
            ITreeNode<T> childNode = new Tree<T>(this, value);

            _childNodes.Add(childNode);

            return childNode;
        }

        /// <inheritdoc/>
        public bool RemoveChild(ITreeNode<T> node)
        {
            return _childNodes.Remove(node);
        }

        /// <inheritdoc/>
        public void Traverse(Action<T> action)
        {
            action(Value);

            foreach (ITreeNode<T> childNode in _childNodes)
            {
                childNode.Traverse(action);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Concat(_childNodes.SelectMany(x => x.Flatten()));
        }
    }
}
