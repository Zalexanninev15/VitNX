using System;
using System.Collections.Generic;
using System.Linq;

namespace VitNX.UI.ControlsV1.BasedOnDarkUI.Collections
{
    public class ObservableList<T> : List<T>, IDisposable
    {
        private bool _disposed;

        public event EventHandler<ObservableListModified<T>> ItemsAdded;

        public event EventHandler<ObservableListModified<T>> ItemsRemoved;

        ~ObservableList()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (ItemsAdded != null)
                    ItemsAdded = null;
                if (ItemsRemoved != null)
                    ItemsRemoved = null;
                _disposed = true;
            }
        }

        public new void Add(T item)
        {
            base.Add(item);
            if (ItemsAdded != null)
                ItemsAdded(this, new ObservableListModified<T>(new List<T> { item }));
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            var list = collection.ToList();
            base.AddRange(list);
            if (ItemsAdded != null)
                ItemsAdded(this, new ObservableListModified<T>(list));
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            if (ItemsRemoved != null)
                ItemsRemoved(this, new ObservableListModified<T>(new List<T> { item }));
        }

        public new void Clear()
        {
            ObservableListModified<T> removed = new ObservableListModified<T>(this.ToList());
            base.Clear();
            if (removed.Items.Count() > 0 && ItemsRemoved != null)
                ItemsRemoved(this, removed);
        }
    }
}