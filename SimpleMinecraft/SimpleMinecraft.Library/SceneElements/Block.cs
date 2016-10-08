namespace SimpleMinecraft.Library.SceneElements
{
    public abstract class Block
    {
        static int blockCounter = 0;

        public int BlockID { get; private set; }
        public float PositionX { get; protected set; }
        public float PositionY { get; protected set; }
        public float PositionZ { get; protected set; }
        public bool IsBreakable { get; private set; }
        private IBlockController blockController;
        
        protected Block(float positionX, float positionY, float positionZ, bool isBreakable, IBlockController blockController)
        {
            blockCounter++;
            BlockID = blockCounter;

            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;

            IsBreakable = isBreakable;

            this.blockController = blockController;
        }

        public void DestroyBlock()
        {
            if(IsBreakable)
            {
                blockController?.Destroy();
            }
        }
    }
}
