using System;
using System.Runtime.CompilerServices;

namespace Unicore.Library.Flows.Path
{
    public static class FlowPathExtensions
    {
        /// <summary>
        ///     Start building a flow path from IFlowChangeable with first IFlowChangeable->IFlowChangeable transition.
        /// </summary>
        /// <param name="root">Root IFlowChangeable object.</param>
        /// <param name="transition">IFlowChangeable->IFlowChangeable transition.</param>
        /// <typeparam name="TClass">Inheritor of IFlowChangeable.</typeparam>
        /// <typeparam name="TNext">Return type, inheritor of IFlowChangeable.</typeparam>
        /// <returns>IFlowPathBuilder for further transitions or build actions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowPathBuilder<TNext> Path<TClass, TNext>(this TClass root, Func<TClass, TNext> transition)
            where TClass : class, IFlowChangeable
            where TNext : class, IFlowChangeable {
            return new FlowBuilderBuilder<TClass>(root).Path(transition);
        }

        /// <summary>
        ///     Add new flow path changeable->changeable transition.
        /// </summary>
        /// <param name="path">Original FlowPathBuilder to add transition to.</param>
        /// <param name="transition">IFlowChangeable->IFlowChangeable transition.</param>
        /// <typeparam name="TClass">Inheritor of IFlowChangeable.</typeparam>
        /// <typeparam name="TNext">Return type, inheritor of IFlowChangeable.</typeparam>
        /// <returns>IFlowPathBuilder for further transitions or build actions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowPathBuilder<TNext> Path<TClass, TNext>(this IFlowPathBuilder<TClass> path, Func<TClass, TNext> transition)
            where TClass : class, IFlowChangeable
            where TNext : class, IFlowChangeable {
            return new FlowBuilderBuilder<TNext>((IFlowBuilder) path, x => transition((TClass) x));
        }

        /// <summary>
        ///     Start building a flow path from IFlowSource{T} with first T->IFlowChangeable transition.
        ///     The transition function is applied directly to IFlowSource{T}.Value.
        /// </summary>
        /// <param name="root">Root IFlowSource{T} object.</param>
        /// <param name="transition">T->IFlowChangeable transition.</param>
        /// <typeparam name="T">Type in IFlowSource{T}.</typeparam>
        /// <typeparam name="TNext">Return type, inheritor of IFlowChangeable.</typeparam>
        /// <returns>IFlowPathBuilder for further transitions or build actions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowPathBuilder<TNext> Path<T, TNext>(this IFlowSource<T> root, Func<T, TNext> transition)
            where TNext : class, IFlowChangeable {
            return new FlowBuilderBuilder<IFlowSource<T>>(root).Path(transition);
        }

        /// <summary>
        ///     Add new flow path T->changeable transition.
        ///     The transition function is applied directly to IFlowSource{T}.Value.
        /// </summary>
        /// <param name="builder">Original FlowPathBuilder to add transition to.</param>
        /// <param name="transition">T->IFlowChangeable transition.</param>
        /// <typeparam name="T">Type in IFlowSource{T}.</typeparam>
        /// <typeparam name="TNext">Return type, inheritor of IFlowChangeable.</typeparam>
        /// <returns>IFlowPathBuilder for further transitions or build actions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowPathBuilder<TNext> Path<T, TNext>(this IFlowPathBuilder<IFlowSource<T>> builder, Func<T, TNext> transition)
            where TNext : class, IFlowChangeable {
            return new FlowBuilderBuilder<TNext>((IFlowBuilder) builder, x => transition(((IFlowSource<T>) x).Value));
        }

        /// <summary>
        ///     Build IFlowSourcePath which proxy all typical IFlowSource functions to flow path Target.
        /// </summary>
        /// <param name="builder">Flow path to build.</param>
        /// <typeparam name="T">Type in IFlowSource{T}.</typeparam>
        /// <returns>IFlowSourcePath object built.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowSource<T> Build<T>(this IFlowPathBuilder<IFlowSource<T>> builder) {
            return new FlowSourcePath<T>((IFlowBuilder) builder);
        }

        /// <summary>
        ///     Build FlowValuePath which proxy all typical IFlowValue functions to flow path Target.
        /// </summary>
        /// <param name="builder">Flow path to build.</param>
        /// <typeparam name="T">Type in IFlowValue{T}.</typeparam>
        /// <returns>FlowValuePath object built.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowMutable<T> Build<T>(this IFlowPathBuilder<IFlowMutable<T>> builder) {
            return new FlowVarPath<T>((IFlowBuilder) builder);
        }

        /// <summary>
        ///     Build IFlowReadOnlyListPath which proxy all typical IFlowReadOnlyList functions to flow path Target.
        /// </summary>
        /// <param name="builder">Flow path to build.</param>
        /// <typeparam name="T">Type in IFlowReadOnlyList{T}.</typeparam>
        /// <returns>IFlowReadOnlyListPath object built.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowReadOnlyList<T> Build<T>(this IFlowPathBuilder<IFlowReadOnlyList<T>> builder) {
            return new FlowReadOnlyListPath<T>((IFlowBuilder) builder);
        }

        /// <summary>
        ///     Build IFlowListPath which proxy all typical IFlowList functions to flow path Target.
        /// </summary>
        /// <param name="builder">Flow path to build.</param>
        /// <typeparam name="T">Type in IFlowList{T}.</typeparam>
        /// <returns>IFlowListPath object built.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowList<T> Build<T>(this IFlowPathBuilder<IFlowList<T>> builder) {
            return new FlowListPath<T>((IFlowBuilder) builder);
        }
    }
}
