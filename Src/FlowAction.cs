using System;
using System.Runtime.CompilerServices;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     FlowAction executes an action on change of any of sources.
    ///     The class instance is bound to the owner, which determines its lifetime.
    /// </summary>
    internal class FlowAction : FlowSubscribableBase
    {
        public FlowAction(object owner, Action action, params IFlowChangeable[] sources) {
            Action = action;
            SubscribeTo(sources);

            // Bound to the owner in order to prevent GC (the object will be guaranteed to outlive the owner)
            WeakHolder.Add(owner, this);
        }

        public Action Action { get; }

        public override void OnChange() {
            Action();
        }

        // Used instance.OnChange instead of instance.Action to ensure the livetime of the object
        // while the reference to the action is hold
        public static implicit operator Action(FlowAction instance) => instance.OnChange;
    }


    /// <summary>
    ///     Creator helpers.
    /// </summary>
    public static partial class Flow
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(object owner, IFlowChangeable changeable, Action action) {
            return new FlowAction(owner, () => {
                // Capture to ensure lifetime
                var d1 = changeable;
                action();
            }, changeable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(object owner, IFlowChangeable changeable1, IFlowChangeable changeable2, Action action) {
            return new FlowAction(owner, () => {
                // Capture to ensure lifetime
                var d1 = changeable1;
                var d2 = changeable2;
                action();
            }, changeable1, changeable2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(object owner, IFlowChangeable changeable1, IFlowChangeable changeable2, IFlowChangeable changeable3, Action action) {
            return new FlowAction(owner, () => {
                // Capture to ensure lifetime
                var d1 = changeable1;
                var d2 = changeable2;
                var d3 = changeable3;
                action();
            }, changeable1, changeable2, changeable3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(object owner, IFlowChangeable changeable1, IFlowChangeable changeable2, IFlowChangeable changeable3, IFlowChangeable changeable4, Action action) {
            return new FlowAction(owner, () => {
                // Capture to ensure lifetime
                var d1 = changeable1;
                var d2 = changeable2;
                var d3 = changeable3;
                var d4 = changeable4;
                action();
            }, changeable1, changeable2, changeable3, changeable4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(object owner, IFlowChangeable changeable1, IFlowChangeable changeable2, IFlowChangeable changeable3, IFlowChangeable changeable4, IFlowChangeable changeable5, Action action) {
            return new FlowAction(owner, () => {
                // Capture to ensure lifetime
                var d1 = changeable1;
                var d2 = changeable2;
                var d3 = changeable3;
                var d4 = changeable4;
                var d5 = changeable5;
                action();
            }, changeable1, changeable2, changeable3, changeable4, changeable5);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(object owner, IFlowChangeable changeable1, IFlowChangeable changeable2, IFlowChangeable changeable3, IFlowChangeable changeable4, IFlowChangeable changeable5, IFlowChangeable changeable6, Action action) {
            return new FlowAction(owner, () => {
                // Capture to ensure lifetime
                var d1 = changeable1;
                var d2 = changeable2;
                var d3 = changeable3;
                var d4 = changeable4;
                var d5 = changeable5;
                var d6 = changeable6;
                action();
            }, changeable1, changeable2, changeable3, changeable4, changeable5, changeable6);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(object owner, IFlowChangeable changeable1, IFlowChangeable changeable2, IFlowChangeable changeable3, IFlowChangeable changeable4, IFlowChangeable changeable5, IFlowChangeable changeable6, IFlowChangeable changeable7, Action action) {
            return new FlowAction(owner, () => {
                // Capture to ensure lifetime
                var d1 = changeable1;
                var d2 = changeable2;
                var d3 = changeable3;
                var d4 = changeable4;
                var d5 = changeable5;
                var d6 = changeable6;
                var d7 = changeable7;
                action();
            }, changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action(object owner, IFlowChangeable changeable1, IFlowChangeable changeable2, IFlowChangeable changeable3, IFlowChangeable changeable4, IFlowChangeable changeable5, IFlowChangeable changeable6, IFlowChangeable changeable7, IFlowChangeable changeable8, Action action) {
            return new FlowAction(owner, () => {
                // Capture to ensure lifetime
                var d1 = changeable1;
                var d2 = changeable2;
                var d3 = changeable3;
                var d4 = changeable4;
                var d5 = changeable5;
                var d6 = changeable6;
                var d7 = changeable7;
                var d8 = changeable8;
                action();
            }, changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7, changeable8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1>(object owner, T1 changeable, Action<T1> action)
            where T1 : IFlowChangeable {
            return new FlowAction(owner, () => action(changeable), changeable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1, T2>(object owner, T1 changeable1, T2 changeable2, Action<T1, T2> action)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable {
            return new FlowAction(owner, () => action(changeable1, changeable2), changeable1, changeable2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1, T2, T3>(object owner, T1 changeable1, T2 changeable2, T3 changeable3, Action<T1, T2, T3> action)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable {
            return new FlowAction(owner, () => action(changeable1, changeable2, changeable3), changeable1, changeable2, changeable3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1, T2, T3, T4>(object owner, T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, Action<T1, T2, T3, T4> action)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable {
            return new FlowAction(owner, () => action(changeable1, changeable2, changeable3, changeable4), changeable1, changeable2, changeable3, changeable4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1, T2, T3, T4, T5>(object owner, T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, T5 changeable5, Action<T1, T2, T3, T4, T5> action)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable
            where T5 : IFlowChangeable {
            return new FlowAction(owner, () => action(changeable1, changeable2, changeable3, changeable4, changeable5), changeable1, changeable2, changeable3, changeable4, changeable5);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1, T2, T3, T4, T5, T6>(object owner, T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, T5 changeable5, T6 changeable6, Action<T1, T2, T3, T4, T5, T6> action)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable
            where T5 : IFlowChangeable
            where T6 : IFlowChangeable {
            return new FlowAction(owner, () => action(changeable1, changeable2, changeable3, changeable4, changeable5, changeable6), changeable1, changeable2, changeable3, changeable4, changeable5, changeable6);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1, T2, T3, T4, T5, T6, T7>(object owner, T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, T5 changeable5, T6 changeable6, T7 changeable7, Action<T1, T2, T3, T4, T5, T6, T7> action)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable
            where T5 : IFlowChangeable
            where T6 : IFlowChangeable
            where T7 : IFlowChangeable {
            return new FlowAction(owner, () => action(changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7), changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action ActionC<T1, T2, T3, T4, T5, T6, T7, T8>(object owner, T1 changeable1, T2 changeable2, T3 changeable3, T4 changeable4, T5 changeable5, T6 changeable6, T7 changeable7, T8 changeable8, Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
            where T1 : IFlowChangeable
            where T2 : IFlowChangeable
            where T3 : IFlowChangeable
            where T4 : IFlowChangeable
            where T5 : IFlowChangeable
            where T6 : IFlowChangeable
            where T7 : IFlowChangeable
            where T8 : IFlowChangeable {
            return new FlowAction(owner, () => action(changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7, changeable8), changeable1, changeable2, changeable3, changeable4, changeable5, changeable6, changeable7, changeable8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1>(object owner, IFlowSource<T1> source, Action<T1> action) {
            return new FlowAction(owner, () => action(source.Value), source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1, T2>(object owner, IFlowSource<T1> source1, IFlowSource<T2> source2, Action<T1, T2> action) {
            return new FlowAction(owner, () => action(source1.Value, source2.Value), source1, source2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1, T2, T3>(object owner, IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, Action<T1, T2, T3> action) {
            return new FlowAction(owner, () => action(source1.Value, source2.Value, source3.Value), source1, source2, source3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1, T2, T3, T4>(object owner, IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, Action<T1, T2, T3, T4> action) {
            return new FlowAction(owner, () => action(source1.Value, source2.Value, source3.Value, source4.Value), source1, source2, source3, source4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1, T2, T3, T4, T5>(object owner, IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, IFlowSource<T5> source5, Action<T1, T2, T3, T4, T5> action) {
            return new FlowAction(owner, () => action(source1.Value, source2.Value, source3.Value, source4.Value, source5.Value), source1, source2, source3, source4, source5);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1, T2, T3, T4, T5, T6>(object owner, IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, IFlowSource<T5> source5, IFlowSource<T6> source6, Action<T1, T2, T3, T4, T5, T6> action) {
            return new FlowAction(owner, () => action(source1.Value, source2.Value, source3.Value, source4.Value, source5.Value, source6.Value), source1, source2, source3, source4, source5, source6);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1, T2, T3, T4, T5, T6, T7>(object owner, IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, IFlowSource<T5> source5, IFlowSource<T6> source6, IFlowSource<T7> source7, Action<T1, T2, T3, T4, T5, T6, T7> action) {
            return new FlowAction(owner, () => action(source1.Value, source2.Value, source3.Value, source4.Value, source5.Value, source6.Value, source7.Value), source1, source2, source3, source4, source5, source6, source7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action Action<T1, T2, T3, T4, T5, T6, T7, T8>(object owner, IFlowSource<T1> source1, IFlowSource<T2> source2, IFlowSource<T3> source3, IFlowSource<T4> source4, IFlowSource<T5> source5, IFlowSource<T6> source6, IFlowSource<T7> source7, IFlowSource<T8> source8, Action<T1, T2, T3, T4, T5, T6, T7, T8> action) {
            return new FlowAction(owner, () => action(source1.Value, source2.Value, source3.Value, source4.Value, source5.Value, source6.Value, source7.Value, source8.Value), source1, source2, source3, source4, source5, source6, source7, source8);
        }
    }
}
