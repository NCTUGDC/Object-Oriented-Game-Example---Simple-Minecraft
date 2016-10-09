using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMinecraft.Library.PlayerElements
{
    public class Inventory
    {
        public static int DefaultCapacity { get { return 50; } }
        public static int DefaultHotKeyCapacity { get { return 10; } }
        private static int inventoryCounter = 0;

        public int InventoryID { get; private set; }
        public int Capacity { get; private set; }
        public int HotKeyCapacity { get; private set; }
        public Player Owner { get; private set; }
        public int EmptyBlockCount { get { return itemInfos.Count(x => x.Item == null); } }
        protected InventoryItemInfo[] itemInfos;
        protected Dictionary<int, InventoryItemInfo> itemInfoDictionary;

        public IEnumerable<InventoryItemInfo> ItemInfos { get { return itemInfos.Where(x => x.Item != null); } }
        public IEnumerable<InventoryItemInfo> HotKeyItemInfos { get { return itemInfos.Skip(Capacity - HotKeyCapacity).Where(x => x.Item != null); } }

        private event Action<InventoryItemInfo> onItemChange;
        public event Action<InventoryItemInfo> OnItemChange { add { onItemChange += value; } remove { onItemChange -= value; } }

        public Inventory(int capacity, int hotKeyCapacity, Player owner)
        {
            inventoryCounter++;
            InventoryID = inventoryCounter;
            Capacity = capacity;
            HotKeyCapacity = hotKeyCapacity;
            Owner = owner;

            itemInfos = new InventoryItemInfo[Capacity];
            for (int i = 0; i < Capacity; i++)
            {
                itemInfos[i] = new InventoryItemInfo(null, 0, i);
            }
            itemInfoDictionary = new Dictionary<int, InventoryItemInfo>();
        }
        public bool ContainsInventoryItemInfo(int inventoryItemInfoID)
        {
            return itemInfoDictionary.ContainsKey(inventoryItemInfoID);
        }
        public InventoryItemInfo FindInventoryItemInfo(int inventoryItemInfoID)
        {
            if (ContainsInventoryItemInfo(inventoryItemInfoID))
            {
                return itemInfoDictionary[inventoryItemInfoID];
            }
            else
            {
                return null;
            }
        }
        public bool IsPositionIndexInRange(int positionIndex)
        {
            return positionIndex >= 0 && positionIndex < Capacity;
        }

        public void LoadItem(InventoryItemInfo info)
        {
            if (IsPositionIndexInRange(info.PositionIndex))
            {
                if (itemInfoDictionary.ContainsKey(info.InventoryItemInfoID))
                {
                    itemInfoDictionary[info.InventoryItemInfoID].Item = info.Item;
                    itemInfoDictionary[info.InventoryItemInfoID].Count = info.Count;
                    itemInfoDictionary[info.InventoryItemInfoID].PositionIndex = info.PositionIndex;
                }
                else
                {
                    itemInfos[info.PositionIndex] = info;
                    itemInfoDictionary.Add(info.InventoryItemInfoID, info);
                }
                onItemChange?.Invoke(info);
            }
        }
        public int ItemCount(int itemID)
        {
            return ItemInfos.Where(x => x.Item.ItemID == itemID).Sum(x => x.Count);
        }
        public bool CanAddItem(Item item, int count)
        {
            if (EmptyBlockCount > 0)
            {
                return true;
            }
            else if (ItemCount(item.ItemID) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddItem(Item item, int count, out InventoryItemInfo newInfo)
        {
            if (CanAddItem(item, count))
            {
                if (ItemCount(item.ItemID) == 0)
                {
                    InventoryItemInfo info = itemInfos.First(x => x.Item == null);
                    info.Item = item;
                    info.Count = count;
                    newInfo = info;
                    itemInfoDictionary.Add(info.InventoryItemInfoID, info);
                }
                else
                {
                    InventoryItemInfo existItems = ItemInfos.First(x => x.Item.ItemID == item.ItemID);
                    existItems.Count += count;
                    newInfo = existItems;
                }
                onItemChange?.Invoke(newInfo);
                return true;
            }
            else
            {
                newInfo = null;
                return false;
            }
        }
        public bool RemoveItem(int positionIndex, int count)
        {
            if (IsPositionIndexInRange(positionIndex) && itemInfos[positionIndex].Count >= count)
            {
                itemInfos[positionIndex].Count -= count;
                if (itemInfos[positionIndex].Count == 0)
                {
                    if (itemInfoDictionary.ContainsKey(itemInfos[positionIndex].InventoryItemInfoID))
                    {
                        itemInfoDictionary.Remove(itemInfos[positionIndex].InventoryItemInfoID);
                    }
                    itemInfos[positionIndex] = new InventoryItemInfo(null, 0, positionIndex);
                }
                onItemChange?.Invoke(itemInfos[positionIndex]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SwapItemInfo(int originPosition, int newPosition)
        {
            itemInfos[originPosition].PositionIndex = newPosition;
            itemInfos[newPosition].PositionIndex = originPosition;

            InventoryItemInfo selectedInfo = itemInfos[originPosition];
            itemInfos[originPosition] = itemInfos[newPosition];
            itemInfos[newPosition] = selectedInfo;
            onItemChange?.Invoke(itemInfos[originPosition]);
            onItemChange?.Invoke(itemInfos[newPosition]);
        }
        public bool UseItem(int positionIndex)
        {
            if (IsPositionIndexInRange(positionIndex) && itemInfos[positionIndex].Count > 0)
            {
                List<IEffectorTarget> effectorTargets = new List<IEffectorTarget>() { Owner };
                if (itemInfos[positionIndex].Item.Affect(effectorTargets))
                {
                    return RemoveItem(positionIndex, 1);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
