using IngenieriaBosco.Core.Resources;
using System;
using System.Collections.ObjectModel;

namespace IngenieriaBosco.Core.Models.Generics
{
    public class GridListModel<T> : INotify
    {
        #region Propiedades

        private T? selectedItem;
        private ObservableCollection<T> collection;
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
                if(SetProperty(ref selectedItem, value) && selectedItem != null && OnSelectionChanged != null)
                {
                    OnSelectionChanged!.Invoke(selectedItem);
                }
            }
        }

        #endregion

        public GridListModel()
        {
            collection = new ObservableCollection<T>();
        }

        #region Metodos
        public void Insert(T item)
        {
            Collection.Add(item);
            OnPropertyChanged(nameof(Collection));
        }
        public void Edit(T oldi, T newi)
        {
            Collection[Collection.IndexOf(oldi)] = newi;
            OnPropertyChanged(nameof(Collection));
        }
        public void Delete(T item)
        {
            Collection.Remove(item);
            OnPropertyChanged(nameof(Collection));
        }

        public Action<T>? OnSelectionChanged;
        #endregion
    }
}
