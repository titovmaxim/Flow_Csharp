using System;
using System.Runtime.CompilerServices;
using Unicore.Library.Arc;
using Unicore.Library.Dispose;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows.Chaining
{
    /// <summary>
    ///     Cache source IFlowSource.
    ///     Source IFlowSource.Value is cached (and is not recalculated) until OnChange event occurs.
    ///     If T is <see cref="IArc" /> it will capture it in cache and release by disposer.
    /// </summary>
    internal class FlowCache<T> : FlowSourceBase<T>
    {
        private readonly object _lock = new object();
        private int _changeCount;
        private bool _isCached;
        private T _cachedValue = default!;

        /// <summary>
        ///     Create a FlowCache.
        /// </summary>
        /// <param name="source">Source to cache.</param>
        /// <param name="disposer">Disposer in case T is IArc (required).</param>
        /// <exception cref="Exception">
        ///     Throws Exception in case T is IArc and no disposer is supplied
        ///     or T is not IArc and disposer is supplied.
        /// </exception>
        public FlowCache(IFlowSource<T> source, IDisposer? disposer = null) {
            SubscribeTo(source);

            GetValue = () => {
                int lastChangeCount;

                lock (_lock) {
                    if (_isCached) {
                        (_cachedValue as IArc)?.Acquire();
                        return _cachedValue;
                    }

                    lastChangeCount = _changeCount;
                }

                // Calc may be lock, so is not locked to allow OnChange fire on another thread
                var newValue = source.Value;

                lock (_lock) {
                    // Can be changed during Calc() by OnChange method, we have to return the value even if the value will not be used as cached later
                    if (lastChangeCount != _changeCount)
                        return newValue;

                    // Dispose previous value is necessary, this can not be done in OnChange methods, since some object need to be disposed in the same thread (like OpenGL)
                    (_cachedValue as IArc)?.Dispose();
                    _cachedValue = newValue;
                    _isCached = true;

                    (newValue as IArc)?.Acquire();
                    return newValue;
                }
            };

            if (Helpers.IsInheritedFrom<T, IArc>()) {
                if (disposer == null)
                    throw new Exception($"IArc type {typeof(T)} requires disposer");

                disposer.Add(() => {
                    (_cachedValue as IArc)?.Dispose();
                    _cachedValue = default!;
                });
            }
            else if (disposer != null) {
                throw new Exception($"Disposer cannot be used for type {typeof(T)}");
            }
        }

        public override void OnChange() {
            lock (_lock) {
                _changeCount++;

                // Release cachedValue reference, in case of IArc we will wait for new .Value call and dispose it there, on the same thread
                if (!(_cachedValue is IArc))
                    _cachedValue = default!;

                _isCached = false;
            }

            base.OnChange();
        }
    }


    /// <summary>
    ///     Creator helpers.
    /// </summary>
    public static partial class FlowChainingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Cache<T>(this IFlowSource<T> source, IDisposer? disposer = null) {
            return new FlowCache<T>(source, disposer);
        }
    }
}
