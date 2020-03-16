namespace Unicore.Library.Flows.Path
{
    internal class FlowVarPath<T> : FlowPath<IFlowMutable<T>>, IFlowMutable<T>
    {
        internal FlowVarPath(IFlowBuilder builder) : base(builder) {
        }

        public T Value {
            get => Target.Value;
            set => Target.Value = value;
        }

        public object? this[string name] {
            get => Target[name];
            set => Target[name] = value;
        }
    }
}
