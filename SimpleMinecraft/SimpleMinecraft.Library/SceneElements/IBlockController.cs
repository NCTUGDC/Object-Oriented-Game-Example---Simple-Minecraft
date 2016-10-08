namespace SimpleMinecraft.Library.SceneElements
{
    public interface IBlockController
    {
        Block Block { get; set; }
        void Destroy();
    }
}
