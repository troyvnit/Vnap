using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.Views.ExtendedControls
{
    public class ExtendedListView : ListView
    {
        private bool _isLoadingMore;
        private int _currentItems;

        #region Bindable Properties

        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create("ItemClickCommand",
            typeof(ICommand), typeof(ExtendedListView));

        public static BindableProperty LoadMoreCommandProperty = BindableProperty.Create("LoadMoreCommand",
            typeof(ICommand), typeof(ExtendedListView));

        public static BindableProperty AllowSelectItemProperty = BindableProperty.Create("AllowSelectItem",
            typeof(bool), typeof(ExtendedListView), false);

        public static BindableProperty ScrollToBottomProperty = BindableProperty.Create("ScrollToBottom",
            typeof(bool), typeof(ExtendedListView), false);

        #endregion

        #region Properties

        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public bool AllowSelectItem
        {
            get { return (bool)GetValue(AllowSelectItemProperty); }
            set { SetValue(AllowSelectItemProperty, value); }
        }

        public bool ScrollToBottom
        {
            get { return (bool)GetValue(ScrollToBottomProperty); }
            set { SetValue(ScrollToBottomProperty, value); }
        }

        #endregion

        public ExtendedListView()
        {
            RegisterEvents();
        }

        public ExtendedListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            ItemTapped -= OnItemTapped;
            ItemAppearing -= OnItemAppearing;

            ItemTapped += OnItemTapped;
            ItemAppearing += OnItemAppearing;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && ItemClickCommand != null && ItemClickCommand.CanExecute(e.Item))
            {
                ItemClickCommand.Execute(e.Item);
            }

            if (!AllowSelectItem)
            {
                SelectedItem = null;
            }
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;

            if (_isLoadingMore || items == null || items.Count == 0)
                return;

            if (ScrollToBottom && _currentItems > 0 && items.Count != _currentItems)
            {
                ScrollTo(items[items.Count - 1], ScrollToPosition.MakeVisible, true);
            }

            _currentItems = items.Count;

            // Hit the bottom
            if (e.Item == items[items.Count - 1] && !(LoadMoreCommand == null || !LoadMoreCommand.CanExecute(e)))
            {
                _isLoadingMore = true;

                LoadMoreCommand.Execute(null);

                _isLoadingMore = false;
            }
        }
    }

    /// <summary> 
    /// Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {

        /// <summary> 
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class. 
        /// </summary> 
        public ObservableRangeCollection()
            : base()
        {
        }

        /// <summary> 
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class that contains elements copied from the specified collection. 
        /// </summary> 
        /// <param name="collection">collection: The collection from which the elements are copied.</param> 
        /// <exception cref="System.ArgumentNullException">The collection parameter cannot be null.</exception> 
        public ObservableRangeCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        /// <summary> 
        /// Adds the elements of the specified collection to the end of the ObservableCollection(Of T). 
        /// </summary> 
        public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            CheckReentrancy();

            if (notificationMode == NotifyCollectionChangedAction.Reset)
            {
                foreach (var i in collection)
                {
                    Items.Add(i);
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                return;
            }

            int startIndex = Count;
            var changedItems = collection is List<T> ? (List<T>)collection : new List<T>(collection);
            foreach (var i in changedItems)
            {
                Items.Add(i);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));
        }

        /// <summary> 
        /// Removes the first occurence of each item in the specified collection from ObservableCollection(Of T). 
        /// </summary> 
        public void RemoveRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            foreach (var i in collection)
                Items.Remove(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary> 
        /// Clears the current collection and replaces it with the specified item. 
        /// </summary> 
        public void Replace(T item)
        {
            ReplaceRange(new T[] { item });
        }

        /// <summary> 
        /// Clears the current collection and replaces it with the specified collection. 
        /// </summary> 
        public void ReplaceRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            Items.Clear();
            AddRange(collection, NotifyCollectionChangedAction.Reset);
        }
    }
}
