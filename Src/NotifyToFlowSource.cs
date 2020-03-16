using System;
using System.ComponentModel;
using Unicore.Library.Flows.Bases;

namespace Unicore.Library.Flows
{
    /// <summary>
    ///     Создает IFlowSource из объекта, поддерживающего INotifyPropertyChanged.
    ///     Обеспечивает простую связку между подходами INotifyPropertyChanged и Flow.
    /// </summary>
    /// <typeparam name="TNotifier">Тип отслеживаемого объекта с INotifyPropertyChanged.</typeparam>
    /// <typeparam name="T">Тип данных для IFlowSource</typeparam>
    public class NotifyToFlowSource<TNotifier, T> : FlowSourceBase<T>, IDisposable
        where TNotifier : INotifyPropertyChanged
    {
        private readonly TNotifier _notifier;
        private readonly string _paramName;

        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="notifier">
        ///     Отслеживаемый объекта с INotifyPropertyChanged.
        /// </param>
        /// <param name="valueExtractor">
        ///     Функция, извлекающая нужное значение из объекта.
        /// </param>
        /// <param name="paramName">
        ///     Имя параметра для отслеживания. Если пустая строка - отслеживается любой PropertyChanged.
        /// </param>
        public NotifyToFlowSource(TNotifier notifier, Func<TNotifier, T> valueExtractor, string paramName = "") {
            _notifier = notifier;
            _paramName = paramName;
            _notifier.PropertyChanged += OnPropertyChanged;

            GetValue = () => valueExtractor(notifier);
        }

        /// <summary>
        ///     Аналогичный конструктор, но не требующий функции-экстрактора.
        ///     Работает через рефлексию.
        /// </summary>
        public NotifyToFlowSource(TNotifier notifier, string paramName) : this(notifier, CreateReflectionPropertyExtractor(paramName), paramName) {
        }

        public void Dispose() {
            _notifier.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object _, PropertyChangedEventArgs args) {
            if (_paramName.Length == 0 || _paramName == args.PropertyName)
                OnChange();
        }

        private static Func<TNotifier, T> CreateReflectionPropertyExtractor(string paramName) {
            var pi = typeof(TNotifier).GetProperty(paramName);
            if (pi == null)
                throw new ArgumentException($"Property {paramName} is not found in class {typeof(TNotifier)}");
            if (pi.PropertyType != typeof(T))
                throw new ArgumentException($"Property {paramName} has type {pi.PropertyType}, but {typeof(T)} is required");

            return obj => (T) pi.GetValue(obj);
        }
    }


    public class NotifyToFlowSource<T> : NotifyToFlowSource<T, T>
        where T : INotifyPropertyChanged
    {
        public NotifyToFlowSource(T notifier) : base(notifier, x => x) {
        }
    }
}
