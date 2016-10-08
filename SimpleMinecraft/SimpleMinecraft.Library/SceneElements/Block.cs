using System.Collections.Generic;

namespace SimpleMinecraft.Library.SceneElements
{
    public abstract class Block
    {
        static int blockCounter = 0;

        public int BlockID { get; private set; }
        public Vector3 CenterPosition { get; protected set; }
        public bool IsBreakable { get; private set; }
        protected Dictionary<byte, object> blockInformation;
        private IBlockController blockController;

        public delegate Vector3 InstantiateBlockCenterHandler(Vector3 instantiatePoint, Vector3 direction, Block blockPrefab);
        public delegate Block InstantiateBlockHandler(Vector3 instantiatePoint, Vector3 direction, bool isBreakable, Block blockPrefab);

        public abstract InstantiateBlockCenterHandler BlockCenterGenerator { get; }
        public abstract InstantiateBlockHandler BlockGenerator { get; }

        protected Block(Vector3 centerPosition, bool isBreakable)
        {
            blockCounter++;
            BlockID = blockCounter;
            CenterPosition = centerPosition;
            IsBreakable = isBreakable;
            blockInformation = new Dictionary<byte, object>();
        }
        public void BindController(IBlockController blockController)
        {
            this.blockController = blockController;
            blockController.Block = this;
        }
        public void DestroyBlock()
        {
            if(IsBreakable)
            {
                blockController?.Destroy();
            }
        }
        public abstract Vector3 GetInstantiatePoint(Vector3 normal);
    }
}
