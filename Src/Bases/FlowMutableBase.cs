using System;
using Unicore.Library.Arc;
using Unicore.Library.Reflection;

namespace Unicore.Library.Flows.Bases
{
    /// <summary>
    ///     Base IFlowVar implementation.
    /// </summary>
    /// <remarks>
    ///     The inheritors MUST set SetValue in constructor which produce the Value of the source.
    /// </remarks>
    public abstract class FlowMutableBase<T> : FlowSourceBase<T>, IFlowMutable<T>
    {
        protected Action<T> SetValue = null!;

        public new T Value {
            get => GetValue();
            set => SetValue(value);
        }

        public override object? this[string name] {
            get => base[name];

            set {
                var oldValue = Value;

                var accessor = ReflectionAccessor<T>.Get(name);

                var oldItem = accessor.GetFieldOrProperty(oldValue);
                if (Equals(oldItem, value))
                    return;

                if (typeof(T).IsValueType) {
                    Value = accessor.SetFieldOrProperty(oldValue, value);
                }
                else {
                    accessor.SetFieldOrProperty(oldValue, value);
                    (oldValue as IArc)?.Dispose();
                    OnChange();
                }
            }
        }
    }
}
