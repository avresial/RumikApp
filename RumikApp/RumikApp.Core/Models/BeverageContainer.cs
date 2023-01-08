using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RumikApp.Core.Models
{
    public class BeverageContainer
    {
        private List<Beverage> bverages = new List<Beverage>();

        public event EventHandler BveragesUpdated;
        public ReadOnlyCollection<Beverage> Bverages { get; private set; }

        public BeverageContainer()
        {
            this.Bverages = bverages.AsReadOnly();
        }

        public void Add(Beverage beverage) 
        {
            bverages.Add(beverage);
            BveragesUpdated?.Invoke(this, null);

            this.Bverages = bverages.AsReadOnly();
        }

        public void Clear()
        {
            bverages.Clear();
            BveragesUpdated?.Invoke(this, null);

            this.Bverages = bverages.AsReadOnly();
        }

    }
}
