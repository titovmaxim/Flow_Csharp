using System;
using System.Runtime.CompilerServices;

namespace Unicore.Library.Flows.Chaining
{
    public static partial class FlowChainingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T>(this T1 changeable, Func<T1, T> func)
            where T1 : IFlowChangeable {
            return Flow.FuncC(changeable, func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T>(this IFlowSource<T1> source, Func<T1, T> func) {
            return Flow.Func(source, func);
        }
    }
}
