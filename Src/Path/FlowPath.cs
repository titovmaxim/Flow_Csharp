using System;
using System.Linq;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows.Path
{
    /// <summary>
    ///     IFlowPath implementation.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    internal class FlowPath<TClass> : FlowChangeableBase, IFlowPath<TClass>
        where TClass : class, IFlowChangeable
    {
        private readonly object _lock = new object();
        private readonly Func<object, IFlowChangeable>[] _transitions;
        private readonly IDisposable?[] _subscriptions;

        internal FlowPath(IFlowBuilder builder) {
            _transitions = builder.Transitions;
            _subscriptions = Enumerable.Repeat<IDisposable?>(null, _transitions.Length + 1).ToArray();

            UpdateTracking(builder.Root, 0);
        }

        private volatile TClass _target = null!; // Set and updated at the end of UpdateTracking method
        public TClass Target => _target;

        private void UpdateTracking(IFlowChangeable node, int i) {
            lock (_lock) {
                _subscriptions[i]?.Dispose();

                if (i == _transitions.Length) {
                    _target = (TClass) node;
                    _subscriptions[i] = DisposableSubscribeToTarget(_target);
                    return;
                }

                _subscriptions[i] = node.AddDisposableSubscription(this, () => {
                    UpdateTracking(_transitions[i](node), i + 1);
                    OnChange();
                });

                UpdateTracking(_transitions[i](node), i + 1);
            }
        }

        protected virtual IDisposable DisposableSubscribeToTarget(TClass target) {
            return DisposableSubscribeTo(target);
        }
    }
}
