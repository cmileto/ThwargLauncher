﻿using System;
using System.Collections.Generic;
using System.Text;

using Decal.Adapter;
using Decal.Adapter.Wrappers;

namespace MagFilter
{
    class ThwargInventory
    {
        private Dictionary<int, string> _items = new Dictionary<int, string>();
        private bool disposed;

        public ThwargInventory()
        {
            CoreManager.Current.ItemSelected += Current_ItemSelected;
        }


        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!disposed)
            {
                if (disposing)
                {
                    CoreManager.Current.ItemSelected -= Current_ItemSelected;
                }
                // Indicate that the instance has been disposed.
                disposed = true;
            }
        }

        private void Current_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            if (e.ItemGuid == 0) { return; }
            if (!_items.ContainsKey(e.ItemGuid))
            {
                log.WriteDebug("Item selected {0} - sending request", e.ItemGuid);
                CoreManager.Current.Actions.RequestId(e.ItemGuid);
            }
            else
            {
                log.WriteDebug("Item selected {0} - no request", e.ItemGuid);
            }
        }
        public void HandleInventoryCommand()
        {
            foreach (WorldObject wo in CoreManager.Current.WorldFilter.GetInventory())
            {
                if (!wo.HasIdData)
                {
                    log.WriteDebug("Lack id data for {0}", wo.Id);
                }
                else
                {
                    log.WriteDebug("Id {0}, ObjectClass {1} Name {2}", wo.Id, wo.Name, wo.ObjectClass);
                }
            }
        }
    }
}
