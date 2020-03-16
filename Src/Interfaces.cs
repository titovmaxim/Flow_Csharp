using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     Base interface of Flow library.
    ///     Its only goal is to allow subscriptions for its changes.
    ///     Implements INotifyPropertyChange interface for WPF compatibility.
    /// </summary>
    public interface IFlowChangeable : INotifyPropertyChanged
    {
        /// <summary>
        ///     Subscribe the <see cref="action" /> to the changes of current object.
        ///     The subscription is weak and the <see cref="owner" /> object determines its lifetime.
        ///     Only one subscription for each separate owner is allowed.
        /// </summary>
        /// <param name="owner">Object which determines subscription lifetime.</param>
        /// <param name="action">Action to be executed on current object change.</param>
        /// <exception cref="ArgumentException">If <see cref="owner" /> already has subscription to current object.</exception>
        void AddSubscription(object owner, Action action);

        /// <summary>
        ///     Subscribe the <see cref="action" /> to the changes of current object
        ///     with possibility to unsubscribe with IDisposable returned.
        ///     The subscription is weak and the <see cref="owner" /> object determines its lifetime.
        ///     Only one subscription for each separate owner is allowed.
        /// </summary>
        /// <param name="owner">Object which determines subscription lifetime.</param>
        /// <param name="action">Action to be executed on current object change.</param>
        /// <exception cref="ArgumentException">If <see cref="owner" /> already has subscription to current object.</exception>
        /// <returns>IDisposable object to cancel subscription.</returns>
        IDisposable AddDisposableSubscription(object owner, Action action);

        /// <summary>
        ///     Rises on change of every subscription.
        ///     Can also me manually used to simulate external change.
        /// </summary>
        public void OnChange();
    }


    /// <summary>
    ///     Flow source interface.
    ///     Implement IFlowChangeable.
    ///     Implements INotifyPropertyChanged and accessor by indexer[] for WPF compatibility.
    ///     The [name] in indexer is the field or property in T which extracted from Value by reflection.
    /// </summary>
    public interface IFlowSource<out T> : IFlowChangeable
    {
        /// <summary>
        ///     Get value.
        /// </summary>
        /// <remarks>
        ///     Thread-safe respecting to OnChange().
        ///     Not thread-safe respecting to itself.
        /// </remarks>
        T Value { get; }

        /// <summary>
        ///     Access for WPF bindings by field/property name.
        /// </summary>
        /// <param name="name">Name of field/property in type T.</param>
        object? this[string name] { get; }
    }


    /// <summary>
    ///     Flow value interface.
    ///     Works as IFlowSource but also allows modification of the value.
    ///     Also supports setting individual fields and property of T in Value by indexer[] for WPF compatibility.
    /// </summary>
    public interface IFlowMutable<T> : IFlowSource<T>
    {
        /// <summary>
        ///     Get or set value.
        /// </summary>
        /// <remarks>
        ///     Thread-safe respecting to OnChange().
        ///     Not thread-safe respecting to itself.
        /// </remarks>
        new T Value { get; set; }

        /// <summary>
        ///     Access for WPF bindings by field/property name.
        /// </summary>
        /// <param name="name">Name of field/property in type T.</param>
        new object? this[string name] { get; set; }
    }


    /// <summary>
    ///     Readonly list of items (depending on constructor arguments).
    ///     Implements: IList{T}, IFlowChangeable, INotifyPropertyChanged, INotifyCollectionChanged.
    ///     Not thread safe.
    /// </summary>
    public interface IFlowReadOnlyList<out T> : IFlowChangeable, IEnumerable<T>, INotifyCollectionChanged
    {
        int Count { get; }
        int IndexOf(object item); // Not strongly typed in order to preserve interface covariant <out T>
        bool Contains(object item); // Not strongly typed in order to preserve interface covariant <out T>
        T this[int index] { get; }
        T[] ToArray();
    }


    /// <summary>
    ///     Modifiable list of items (depending on constructor arguments).
    ///     Implements: IList{T}, IFlowChangeable, INotifyPropertyChanged (usually used only by Path implementations),
    ///     INotifyCollectionChanged.
    ///     Not thread safe.
    /// </summary>
    public interface IFlowList<T> : IFlowReadOnlyList<T>
    {
        void Set(IEnumerable<T> list);
        void Clear();
        void Add(T item);
        void Insert(int index, T item);
        bool Remove(T item);
        void RemoveAt(int index);
        new T this[int index] { get; set; }
    }


    /// <summary>
    ///     Dynamic connector to IFlowSource.
    ///     Usually used to connect and disconnect to different sources.
    ///     IDisposable interface does exactly the same as Disconnect.
    /// </summary>
    public interface IFlowConnector<T> : IFlowSource<T>, IDisposable
    {
        /// <summary>
        ///     Disconnects connector. Equal to ConnectTo(null).
        /// </summary>
        void Disconnect();

        /// <summary>
        ///     Connect to IFlowSource.
        /// </summary>
        /// <param name="source">Object to connect to.</param>
        void ConnectTo(IFlowSource<T>? source);

        /// <summary>
        ///     Check if connector is connected.
        /// </summary>
        bool IsConnected { get; }
    }


    /// <summary>
    ///     Retransmit the changes of Target.
    ///     The target object (inheritor of IFlowChangeable) is accessible via Target property.
    /// </summary>
    /// <typeparam name="TClass">Inheritor of IFlowChangeable.</typeparam>
    public interface IFlowPath<out TClass> : IFlowChangeable
        where TClass : class, IFlowChangeable
    {
        TClass Target { get; }
    }
}
