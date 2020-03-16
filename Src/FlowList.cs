using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     IFlowList implementation.
    /// </summary>
    /// <typeparam name="T">Data type.</typeparam>
    internal class FlowList<T> : FlowChangeableBase, IFlowList<T>
    {
        private List<T> _list;

        public FlowList(IEnumerable<T>? list = null) {
            _list = list == null
                ? new List<T>()
                : new List<T>(list);
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public bool IsReadOnly { get; } = false;

        public int Count => _list.Count;

        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        public int IndexOf(object item) {
            return item is T typed
                ? _list.IndexOf(typed)
                : -1;
        }

        public bool Contains(object item) {
            return item is T typed
                ? _list.Contains(typed)
                : false;
        }

        public void Set(IEnumerable<T> list) {
            _list = new List<T>(list);

            NotifySubscribers();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Add(T item) {
            _list.Add(item);

            NotifySubscribers();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, _list.Count - 1));
        }

        public void Insert(int index, T item) {
            _list.Insert(index, item);

            NotifySubscribers();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public bool Remove(T item) {
            var index = _list.IndexOf(item);
            if (index < 0)
                return false;

            _list.RemoveAt(index);

            NotifySubscribers();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            return true;
        }

        public void RemoveAt(int index) {
            var item = _list[index];
            _list.RemoveAt(index);

            NotifySubscribers();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        public void Clear() {
            if (_list.Count > 0) {
                _list.Clear();

                NotifySubscribers();
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public T this[int index] {
            get => _list[index];
            set {
                _list[index] = value;

                NotifySubscribers();
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, index));
            }
        }

        public T[] ToArray() => _list.ToArray();
    }


    /// <summary>
    ///     Creator helpers.
    /// </summary>
    public static partial class Flow
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowList<T> List<T>(IEnumerable<T>? list = null) {
            return new FlowList<T>(list);
        }
    }
}
