using System;
using System.Runtime.CompilerServices;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     Creates new IFlowSource which reacts on changes of any of its input IFlowChangeable arguments
    ///     and evaluate the result function.
    ///     In some meaning it is similar to .Map(x => func(x)) or .Select(x => func(x)).
    ///     It transforms the input IFlowChangeable arguments to a new value dynamically, monitoring changes in inputs.
    /// </summary>
    internal class FlowFunc<T> : FlowSourceBase<T>
    {
        public FlowFunc(Func<T> func, params IFlowChangeable[] sources) {
            SubscribeTo(sources);
            GetValue = func;
        }
    }


    /// <summary>
    ///     Creator helpers.
    /// </summary>
    public static partial class Flow
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T>(Func<T> func) {
            return new FlowFunc<T>(func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T>(T1 changeable, Func<T1, T> func)
            where T1 : IFlowChangeable {
            return new FlowFunc<T>(() => func(changeable), changeable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T2, T>(T1 changeable1, T2 changeable2, Func<T1, T2, T> func)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable {
            return new FlowFunc<T>(() => func(changeable1, changeable2), changeable1, changeable2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T2, T3, T>(T1 changeable1, T2 changeable2, T3 changeable3, Func<T1, T2, T3, T> func)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable {
            return new FlowFunc<T>(() => func(changeable1, changeable2, changeable3), changeable1, changeable2, changeable3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T2, T3, T4, T>(T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, Func<T1, T2, T3, T4, T> func)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable {
            return new FlowFunc<T>(() => func(changeable1, changeable2, changeable3, changeable4), changeable1, changeable2, changeable3, changeable4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T2, T3, T4, T5, T>(T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, T5 changeable5, Func<T1, T2, T3, T4, T5, T> func)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable
            where T5 : IFlowChangeable {
            return new FlowFunc<T>(() => func(changeable1, changeable2, changeable3, changeable4, changeable5), changeable1, changeable2, changeable3, changeable4, changeable5);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T2, T3, T4, T5, T6, T>(T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, T5 changeable5, T6 changeable6, Func<T1, T2, T3, T4, T5, T6, T> func)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable
            where T5 : IFlowChangeable
            where T6 : IFlowChangeable {
            return new FlowFunc<T>(() => func(changeable1, changeable2, changeable3, changeable4, changeable5, changeable6), changeable1, changeable2, changeable3, changeable4, changeable5, changeable6);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T2, T3, T4, T5, T6, T7, T>(T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, T5 changeable5, T6 changeable6, T7 changeable7, Func<T1, T2, T3, T4, T5, T6, T7, T> func)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable
            where T5 : IFlowChangeable
            where T6 : IFlowChangeable
            where T7 : IFlowChangeable {
            return new FlowFunc<T>(() => func(changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7), changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> FuncC<T1, T2, T3, T4, T5, T6, T7, T8, T>(T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, T5 changeable5, T6 changeable6, T7 changeable7, T8 changeable8, Func<T1, T2, T3, T4, T5, T6, T7, T8, T> func)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable
            where T5 : IFlowChangeable
            where T6 : IFlowChangeable
            where T7 : IFlowChangeable
            where T8 : IFlowChangeable {
            return new FlowFunc<T>(() => func(changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7, changeable8), changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7, changeable8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T>(IFlowSource<T1> source, Func<T1, T> func) {
            return new FlowFunc<T>(() => func(source.Value), source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T2, T>(IFlowSource<T1> source1, IFlowSource<T2> source2, Func<T1, T2, T> func) {
            return new FlowFunc<T>(() => func(source1.Value, source2.Value), source1, source2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T2, T3, T>(IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, Func<T1, T2, T3, T> func) {
            return new FlowFunc<T>(() => func(source1.Value, source2.Value, source3.Value), source1, source2, source3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T2, T3, T4, T>(IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, Func<T1, T2, T3, T4, T> func) {
            return new FlowFunc<T>(() => func(source1.Value, source2.Value, source3.Value, source4.Value), source1, source2, source3, source4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T2, T3, T4, T5, T>(IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, IFlowSource<T5> source5, Func<T1, T2, T3, T4, T5, T> func) {
            return new FlowFunc<T>(() => func(source1.Value, source2.Value, source3.Value, source4.Value, source5.Value), source1, source2, source3, source4, source5);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T2, T3, T4, T5, T6, T>(IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, IFlowSource<T5> source5, IFlowSource<T6> source6, Func<T1, T2, T3, T4, T5, T6, T> func) {
            return new FlowFunc<T>(() => func(source1.Value, source2.Value, source3.Value, source4.Value, source5.Value, source6.Value), source1, source2, source3, source4, source5, source6);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T2, T3, T4, T5, T6, T7, T>(IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, IFlowSource<T5> source5, IFlowSource<T6> source6, IFlowSource<T7> source7, Func<T1, T2, T3, T4, T5, T6, T7, T> func) {
            return new FlowFunc<T>(() => func(source1.Value, source2.Value, source3.Value, source4.Value, source5.Value, source6.Value, source7.Value), source1, source2, source3, source4, source5, source6, source7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Func<T1, T2, T3, T4, T5, T6, T7, T8, T>(IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, IFlowSource<T5> source5, IFlowSource<T6> source6, IFlowSource<T7> source7, IFlowSource<T8> source8, Func<T1, T2, T3, T4, T5, T6, T7, T8, T> func) {
            return new FlowFunc<T>(() => func(source1.Value, source2.Value, source3.Value, source4.Value, source5.Value, source6.Value, source7.Value, source8.Value), source1, source2, source3, source4, source5, source6, source7, source8);
        }
    }
}
