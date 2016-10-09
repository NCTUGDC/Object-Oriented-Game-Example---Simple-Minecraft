namespace SimpleMinecraft.Library.PlayerElements
{
    public class HotKeyInfo
    {
        private static int hotKeyInfoCounter = 0;

        public int HotKeyInfoID { get; protected set; }
        public short HotKeyCode { get; protected set; }
        public InventoryItemInfo InventoryItemInfo { get; protected set; }

        public HotKeyInfo(short hotKeyCode, InventoryItemInfo inventoryItemInfo)
        {
            hotKeyInfoCounter++;
            HotKeyInfoID = hotKeyInfoCounter;
            HotKeyCode = hotKeyCode;
            InventoryItemInfo = inventoryItemInfo;
        }
    }
}
