using System;
using System.Runtime.CompilerServices;
using Unicore.Library.Arc;
using Unicore.Library.Dispose;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     IFlowValue which stores a value, like a var or field in C#.
    ///     If T is <see cref="IArc" /> it will capture it in cache and release by disposer.
    /// </summary>
    internal class FlowVar<T> : FlowMutableBase<T>
    {
        private readonly object _lock = new object();
        private T _value;

        /// <summary>
        ///     Creates FlowVar.
        /// </summary>
        /// <param name="initialValue">Initial value.</param>
        /// <param name="disposer">Disposer in case T is IArc (required).</param>
        /// <exception cref="Exception">
        ///     Throws Exception in case T is IArc and no disposer is supplied
        ///     or T is not IArc and disposer is supplied.
        /// </exception>
        public FlowVar(T initialValue, IDisposer? disposer = null) {
            _value = initialValue;

            GetValue = () => {
                lock (_lock) {
                    (_value as IArc)?.Acquire();
                    return _value;
                }
            };

            SetValue = newValue => {
                lock (_lock) {
                    if (Equals(_value, newValue)) {
                        (newValue as IArc)?.Dispose();
                        return;
                    }

                    (_value as IArc)?.Dispose();
                    _value = newValue;
                }

                OnChange();
            };

            if (Helpers.IsInheritedFrom<T, IArc>()) {
                if (disposer == null)
                    throw new Exception($"IArc type {typeof(T)} requires disposer");

                disposer.Add(() => {
                    (_value as IArc)?.Dispose();
                    _value = default!;
                });
            }
            else if (disposer != null) {
                throw new Exception($"Disposer cannot be used for type {typeof(T)}");
            }
        }
    }


    /// <summary>
    ///     Creator helpers.
    /// </summary>
    public static partial class Flow
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowMutable<T> Var<T>(T value, IDisposer? disposer = null) {
            return new FlowVar<T>(value, disposer);
        }
    }
}
