﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace VitNX.Collections
{
    public class ObservableList<T> : List<T>, IDisposable
    {
        #region Field Region

        private bool _disposed;

        #endregion Field Region

        #region Event Region

        public event EventHandler<ObservableListModified<T>> ItemsAdded;

        public event EventHandler<ObservableListModified<T>> ItemsRemoved;

        #endregion Event Region

        #region Destructor Region

        ~ObservableList()
        {
            Dispose(false);
        }

        #endregion Destructor Region

        #region Dispose Region

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

        #endregion Dispose Region

        #region Method Region

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
            ObservableListModified<T> removed = new ObservableListModified<T>(this.ToList<T>());
            base.Clear();

            if (removed.Items.Count() > 0 && ItemsRemoved != null)
                ItemsRemoved(this, removed);
        }

        #endregion Method Region
    }
}