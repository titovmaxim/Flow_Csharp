using System;
using System.Linq;
using Unicore.Library.Dispose;

namespace Unicore.Library.Flows.Bases
{
    /// <summary>
    ///     Base IFlowSubscriber implementation.
    /// </summary>
    /// <remarks>
    ///     The inheritors MUST set OnSubscriptionChange in order to react on subscription changes.
    /// </remarks>
    public abstract class FlowSubscribableBase
    {
        protected void SubscribeTo(params IFlowChangeable[] sources) {
            foreach (var source in sources)
                source.AddSubscription(this, OnChange);
        }

        protected IDisposable DisposableSubscribeTo(params IFlowChangeable[] sources) {
            switch (sources.Length) {
                case 0:
                    return new DisposeAction(() => { });

                case 1:
                    return sources[0].AddDisposableSubscription(this, OnChange);

                default: {
                    var subscriptions = sources.Select(source => source.AddDisposableSubscription(this, OnChange));
                    return new DisposeAction(() => {
                        foreach (var subscription in subscriptions)
                            subscription.Dispose();
                    });
                }
            }
        }

        public abstract void OnChange();
    }
}
