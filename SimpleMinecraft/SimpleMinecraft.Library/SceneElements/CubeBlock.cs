namespace SimpleMinecraft.Library.SceneElements
{
    public class CubeBlock : Block
    {
        public float SideLength { get; private set; }

        public override InstantiateBlockCenterHandler BlockCenterGenerator
        {
            get
            {
                return (instantiatePoint, direction, blockPrefab) =>
                {
                    float sideLength = (float)(blockPrefab as CubeBlock).blockInformation[(byte)CubeBlockInformationCode.SideLength];
                    return instantiatePoint + sideLength / 2f * direction.Normalized;
                };
            }
        }
        public override InstantiateBlockHandler BlockGenerator
        {
            get
            {
                return (instantiatePoint, direction, isBreakable, blockPrefab) =>
                {
                    float sideLength = (float)(blockPrefab as CubeBlock).blockInformation[(byte)CubeBlockInformationCode.SideLength];
                    return new CubeBlock(sideLength, BlockCenterGenerator(instantiatePoint, direction, blockPrefab), isBreakable, Item);
                };
            }
        }

        public CubeBlock(float sideLength, Vector3 position, bool isBreakable, Item item) : base(position, isBreakable, item)
        {
            SideLength = sideLength;
            blockInformation.Add((byte)CubeBlockInformationCode.SideLength, SideLength);
        }

        public override Vector3 GetInstantiatePoint(Vector3 normal)
        {
            return CenterPosition + SideLength / 2f * normal.Normalized;
        }
    }
}
