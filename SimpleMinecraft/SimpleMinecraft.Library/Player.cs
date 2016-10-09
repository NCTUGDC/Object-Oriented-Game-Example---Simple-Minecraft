using SimpleMinecraft.Library.PlayerElements;
using System;

namespace SimpleMinecraft.Library
{
    public class Player
    {
        public Inventory Inventory { get; private set; }
        public HotKeySet HotKeySet { get; private set; }

        private InventoryItemInfo holdingItemInfo;
        public InventoryItemInfo HoldingItemInfo
        {
            get { return holdingItemInfo; }
            set
            {
                holdingItemInfo = value;
                onHoldingItemInfoChange?.Invoke(holdingItemInfo);
            }
        }

        private event Action<InventoryItemInfo> onHoldingItemInfoChange;
        public event Action<InventoryItemInfo> OnHoldingItemInfoChange { add { onHoldingItemInfoChange += value; } remove { onHoldingItemInfoChange -= value; } }

        public Player()
        {
            Inventory = new Inventory(Inventory.DefaultCapacity, Inventory.DefaultHotKeyCapacity, this);
            HotKeySet = new HotKeySet(this);
        }
    }
}
