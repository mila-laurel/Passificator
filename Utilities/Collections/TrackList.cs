using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Passificator.Utilities.Collections
{
    internal enum ChangeType
    {
        Added,
        Removed
    }
    
    internal class TrackList<T> : BindingList<T>
    {
        internal class ChangedItem
        {
            public ChangeType ChangeType { get; set; }
            public T Item { get; }

            public ChangedItem(T item, ChangeType changeType)
            {
                ChangeType = changeType;
                Item = item;
            }
        }

        public List<ChangedItem> Changes { get; } = new List<ChangedItem>();

        public TrackList()
        {
            this.ListChanged += OnListChanged;
        }

        public void ClearChanges() => Changes.Clear();

        protected override void RemoveItem(int index)
        {
            var previousChange = Changes.FirstOrDefault(x => x.Item.Equals(this[index]));
            if (previousChange != null)
                previousChange.ChangeType = ChangeType.Removed;
            else
                Changes.Add(new ChangedItem(this[index], ChangeType.Removed));

            base.RemoveItem(index);
        }

        private void OnListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded)
                Changes.Add(new ChangedItem(this[e.NewIndex], ChangeType.Added));
        }
    }
}