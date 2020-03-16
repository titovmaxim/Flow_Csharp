using System;
using System.Runtime.CompilerServices;

namespace Unicore.Library.Flows.Chaining
{
    public static partial class FlowChainingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(this IFlowChangeable changeable, object owner, Action action) {
            return Flow.Action(owner, changeable, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1>(this T1 changeable, object owner, Action<T1> action)
            where T1 : IFlowChangeable {
            return Flow.ActionC(owner, changeable, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1>(this IFlowSource<T1> source, object owner, Action<T1> action) {
            return Flow.Action(owner, source, action);
        }
    }
}
