using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unicore.Library.Dispose;

namespace Unicore.Library.Flows.Path
{
    internal class FlowListPath<T> : FlowPath<IFlowList<T>>, IFlowList<T>
    {
        internal FlowListPath(IFlowBuilder builder) : base(builder) {
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        // Catch OnChange event from target (lat path element) and DO NOT file NotifyPropertyChanged in that case,
        // Also retransmit correct CollectionChanged event.
        // In case any object higher in path hierarchy changed complete OnChange will be fired.
        protected override IDisposable DisposableSubscribeToTarget(IFlowList<T> target) {
            var subscription = target.AddDisposableSubscription(this, NotifySubscribers); // Ignore firing NotifyPropertyChanged event here
            target.CollectionChanged += RetransmitCollectionChanged;

            return new DisposeAction(() => {
                subscription.Dispose();
                target.CollectionChanged -= RetransmitCollectionChanged;
            });
        }

        private void RetransmitCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            CollectionChanged?.Invoke(sender, e);
        }

        IEnumerator IEnumerable.GetEnumerator() => Target.GetEnumerator();

        public IEnumerator<T> GetEnumerator() => Target.GetEnumerator();

        public int Count => Target.Count;

        public int IndexOf(object item) => Target.IndexOf(item);

        public bool Contains(object item) => Target.Contains(item);

        public void Set(IEnumerable<T> list) => Target.Set(list);

        public void Add(T item) => Target.Add(item);

        public void Insert(int index, T item) => Target.Insert(index, item);

        public bool Remove(T item) => Target.Remove(item);

        public void RemoveAt(int index) => Target.RemoveAt(index);

        public void Clear() => Target.Clear();

        public T this[int index] {
            get => Target[index];
            set => Target[index] = value;
        }

        public T[] ToArray() => Target.ToArray();
    }
}
