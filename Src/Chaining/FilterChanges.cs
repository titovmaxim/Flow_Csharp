using Unicore.Library.Dispose;

namespace Unicore.Library.Flows.Chaining
{
    public static partial class FlowChainingExtensions
    {
        public static IFlowSource<T> FilterChanges<T>(this IFlowSource<T> source, IDisposer? disposer = null) {
            var filtered = new FlowVar<T>(source.Value, disposer);
            // FlowVar ignores assignments when new value is equal to old one (so - we filter the changes)
            source.AddSubscription(filtered, () => { filtered.Value = source.Value; });
            return filtered;
        }
    }
}
