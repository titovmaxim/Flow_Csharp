using System;

namespace Unicore.Library.Flows.Path
{
    /// <summary>
    ///     IFlowPathBuilder class to build flow path.
    ///     The possible methods like .Path and .Build are implemented as extensions.
    /// </summary>
    /// <typeparam name="TClass">Type of last flow path element (inheritor of IFlowChangeable).</typeparam>
    public interface IFlowPathBuilder<out TClass>
        where TClass : class, IFlowChangeable
    {
        IFlowPath<TClass> BuildPath();
    }


    // Real builder data.
    // Is not used as a parent for IFlowPathBuilder<T> in order to hide Root and Transitions from end user.
    internal interface IFlowBuilder
    {
        IFlowChangeable Root { get; }
        Func<object, IFlowChangeable>[] Transitions { get; }
    }


    // The only implementation of the two interfaces above.
    // Used only from Extensions.
    internal class FlowBuilderBuilder<TClass> : IFlowPathBuilder<TClass>, IFlowBuilder
        where TClass : class, IFlowChangeable
    {
        public IFlowChangeable Root { get; }
        public Func<object, IFlowChangeable>[] Transitions { get; }

        internal FlowBuilderBuilder(TClass root) {
            Root = root;
            Transitions = new Func<object, IFlowChangeable>[0];
        }

        internal FlowBuilderBuilder(IFlowBuilder builder, Func<object, IFlowChangeable> transition) {
            Root = builder.Root;

            var N = builder.Transitions.Length;
            Transitions = new Func<object, IFlowChangeable>[N + 1];
            Array.Copy(builder.Transitions, Transitions, N);
            Transitions[N] = transition;
        }

        public IFlowPath<TClass> BuildPath() {
            return new FlowPath<TClass>(this);
        }
    }
}
