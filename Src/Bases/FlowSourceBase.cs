using System;
using Unicore.Library.Arc;
using Unicore.Library.Reflection;

namespace Unicore.Library.Flows.Bases
{
    /// <summary>
    ///     Base IFlowSource implementation.
    /// </summary>
    /// <remarks>
    ///     The inheritors MUST set GetValue in constructor which produce the Value of the source.
    /// </remarks>
    public abstract partial class FlowSourceBase<T> : FlowChangeableBase, IFlowSource<T>
    {
        protected Func<T> GetValue = null!;

        public T Value => GetValue();

        /// Dummy set is a mandatory for WPF bindings
        public virtual object? this[string name] {
            get {
                var value = Value;
                using var _ = value as IArc; // To dispose IArc object if necessary
                return ReflectionAccessor<T>.Get(name).GetFieldOrProperty(value);
            }

            set => throw new NotSupportedException();
        }

        public override string ToString() {
            return Value?.ToString() ?? string.Empty;
        }
    }
}
