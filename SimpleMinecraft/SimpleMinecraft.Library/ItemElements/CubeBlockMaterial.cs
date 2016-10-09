using SimpleMinecraft.Library.SceneElements;

namespace SimpleMinecraft.Library.ItemElements
{
    public class CubeBlockMaterial : BlockMaterial
    {
        public float SideLength { get; private set; }
        public override Block BlockTemplate
        {
            get
            {
                return new CubeBlock(SideLength, new Vector3(), true);
            }
        }
        public CubeBlockMaterial(float sideLength)
        {
            SideLength = sideLength;
        }
    }
}
