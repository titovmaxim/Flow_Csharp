using System;
using System.Runtime.CompilerServices;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     IFlowConnector implementation.
    /// </summary>
    internal class FlowConnector<T> : FlowSourceBase<T>, IFlowConnector<T>
    {
        private readonly object _lock = new object();
        private volatile IFlowSource<T>? _source;
        private IDisposable? _subscription;

        public FlowConnector(T defaultValue) {
            GetValue = () => {
                var source = _source;
                return source == null
                    ? defaultValue
                    : source.Value;
            };
        }

        public void Dispose() {
            Disconnect();
        }

        public void Disconnect() {
            ConnectTo(null);
        }

        public void ConnectTo(IFlowSource<T>? source) {
            lock (_lock) {
                if (Equals(_source, source))
                    return;

                _subscription?.Dispose();
                _subscription = null;

                _source = source;

                if (_source != null)
                    _subscription = DisposableSubscribeTo(_source);

                OnChange();
            }
        }

        public bool IsConnected => _source != null;
    }


    /// <summary>
    ///     Creator helpers.
    /// </summary>
    public static partial class Flow
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowConnector<T> Connector<T>(T defaultValue) {
            return new FlowConnector<T>(defaultValue);
        }
    }
}
