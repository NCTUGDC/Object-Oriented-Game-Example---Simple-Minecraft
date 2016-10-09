namespace SimpleMinecraft.Library.SceneElements
{
    public class ItemEntity
    {
        private static int itemEntityCounter = 0;

        public int ItemEntityID { get; protected set; }
        public Item Item { get; protected set; }
        public Vector3 Position { get; protected set; }
        private IItemEntityController itemEntityController;

        public ItemEntity(Item item, Vector3 position)
        {
            itemEntityCounter++;
            ItemEntityID = itemEntityCounter;
            Item = item;
            Position = position;
        }
        public void BindController(IItemEntityController itemEntityController)
        {
            this.itemEntityController = itemEntityController;
            itemEntityController.ItemEntity = this;
        }
        public void Destroy()
        {
            itemEntityController.Destroy();
        }
    }
}
