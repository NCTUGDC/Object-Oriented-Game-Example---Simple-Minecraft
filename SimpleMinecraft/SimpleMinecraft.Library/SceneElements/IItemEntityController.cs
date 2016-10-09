namespace SimpleMinecraft.Library.SceneElements
{
    public interface IItemEntityController
    {
        ItemEntity ItemEntity { get; set; }
        void Destroy();
    }
}
