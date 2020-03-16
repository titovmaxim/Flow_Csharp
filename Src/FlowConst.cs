using System.Runtime.CompilerServices;
using Unicore.Library.Dispose;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     Creator helpers.
    /// </summary>
    public static partial class Flow
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Const<T>(T value, IDisposer? disposer = null) {
            return Var(value, disposer);
        }
    }
}
