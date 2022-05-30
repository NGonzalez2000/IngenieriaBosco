using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace IngenieriaBosco.Core.Models.Generics
{
    public class GridListModel<T> : INotify
    {

        private T? selectedItem;
        private ObservableCollection<T> collection;
        private ICollectionView collectionView;
        public ObservableCollection<T> Collection
        {
            get { return collection; }
            set { collection = value; }
        }
        public T? SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if(SetProperty(ref selectedItem, value) && OnSelectionChanged != null)
                {
                    OnSelectionChanged!.Invoke(selectedItem);
                }
            }
        }

        public GridListModel()
        {

            collection = new ObservableCollection<T>();
            collectionView = CollectionViewSource.GetDefaultView(collection);
        }
        public GridListModel(IEnumerable<T>? _collection) : this()
        {
            if(_collection != null) collection = new(_collection);
        }

        public void FilterExecute(Predicate<object> predicate)
        {
            collectionView = CollectionViewSource.GetDefaultView(collection);
            collectionView.Filter = predicate;
            collectionView.Refresh();
        }
        public void SortExecute(SortDescription sortDescription)
        {
            collectionView = CollectionViewSource.GetDefaultView(collection);
            collectionView.SortDescriptions.Clear();
            collectionView.SortDescriptions.Add(sortDescription);
            collectionView.Refresh();
        }
        public void Insert(T item)
            => Collection.Add(item);
        public void Edit(T oldi, T newi)
        {
            int indx = Collection.IndexOf(oldi);
            Collection[indx] = newi;
            SelectedItem = Collection[indx];
        } 
        public void Delete(T item)
            => Collection.Remove(item);

        public Action<T?>? OnSelectionChanged;
    }
}
