using System;
using System.Runtime.CompilerServices;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     FlowProperty works very similar to C# Property { get; set; }.
    ///     Usually a baking field is used to store the value.
    ///     Call to OnChange() method is used to fire it in other circumstances.
    /// </summary>
    internal class FlowProperty<T> : FlowMutableBase<T>
    {
        public FlowProperty(Func<T> get, Action<T> set) {
            GetValue = get;
            SetValue = set;
        }
    }


    /// <summary>
    ///     Creator helpers.
    /// </summary>
    public static partial class Flow
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowMutable<T> Property<T>(Func<T> get, Action<T> set) {
            return new FlowProperty<T>(get, set);
        }
    }
}
