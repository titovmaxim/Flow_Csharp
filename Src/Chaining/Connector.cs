using System.Runtime.CompilerServices;

namespace Unicore.Library.Flows.Chaining
{
    public static partial class FlowChainingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowConnector<T> Connector<T>(this IFlowSource<T> source, T @default = default) {
            var connector = new FlowConnector<T>(@default);
            connector.ConnectTo(source);
            return connector;
        }
    }
}
