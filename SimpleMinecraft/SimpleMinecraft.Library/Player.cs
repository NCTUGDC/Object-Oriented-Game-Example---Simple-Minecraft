using SimpleMinecraft.Library.PlayerElements;

namespace SimpleMinecraft.Library
{
    public class Player : IEffectorTarget
    {
        public Inventory Inventory { get; private set; }
        public HotKeySet HotKeySet { get; private set; }

        public Player()
        {
            Inventory = new Inventory(Inventory.DefaultCapacity, Inventory.DefaultHotKeyCapacity, this);
            HotKeySet = new HotKeySet(this);
        }
    }
}
