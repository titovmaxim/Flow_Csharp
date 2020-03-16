namespace Unicore.Library.Flows.Path
{
    internal class FlowSourcePath<T> : FlowPath<IFlowSource<T>>, IFlowSource<T>
    {
        internal FlowSourcePath(IFlowBuilder builder) : base(builder) {
        }

        public T Value => Target.Value;

        public object? this[string name] => Target[name];
    }
}
