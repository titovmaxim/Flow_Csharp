using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unicore.Library.Dispose;

namespace Unicore.Library.Flows.Bases
{
    /// <summary>
    ///     IFlowChangeable base implementation without INotifyPropertyChanged support.
    /// </summary>
    public abstract class FlowChangeableBase : FlowSubscribableBase, IFlowChangeable
    {
        private readonly ConditionalWeakTable<object, Action> _subscriptions = new ConditionalWeakTable<object, Action>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public override void OnChange() {
            NotifySubscribers();
            PropertyChanged?.Invoke(this, Helpers.AllPropertiesChanged);
        }

        protected void NotifySubscribers() {
            foreach (var (_, action) in _subscriptions)
                action();
        }

        public void AddSubscription(object owner, Action action) {
            // Throws on duplicate
            _subscriptions.Add(owner, action);
        }

        public IDisposable AddDisposableSubscription(object owner, Action action) {
            AddSubscription(owner, action);
            return new DisposeAction(() => { _subscriptions.Remove(owner); });
        }
    }
}
