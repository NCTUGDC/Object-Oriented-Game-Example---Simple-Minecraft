namespace SimpleMinecraft.Library.PlayerElements
{
    public class InventoryItemInfo
    {
        private static int inventoryItemInfoCounter = 0;
        public int InventoryItemInfoID { get; set; }
        public Item Item { get; set; }
        public int Count { get; set; }
        public int PositionIndex { get; set; }

        public InventoryItemInfo(Item item, int count, int positionIndex)
        {
            inventoryItemInfoCounter++;
            InventoryItemInfoID = inventoryItemInfoCounter;
            Item = item;
            Count = count;
            PositionIndex = positionIndex;
        }
    }
}
