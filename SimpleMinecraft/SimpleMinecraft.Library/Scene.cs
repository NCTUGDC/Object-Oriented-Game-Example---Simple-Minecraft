using SimpleMinecraft.Library.SceneElements;
using System.Collections.Generic;

namespace SimpleMinecraft.Library
{
    public class Scene
    {
        private Dictionary<int, Block> blockDictionary;
        public Vector3 OriginPoint { get; private set; }
        public float ResetPositionY { get; private set; }

        public Scene(Vector3 originPoint, float resetPositionY)
        {
            blockDictionary = new Dictionary<int, Block>();
            OriginPoint = originPoint;
            ResetPositionY = resetPositionY;
        }

        public bool ContainsBlock(int blockID)
        {
            return blockDictionary.ContainsKey(blockID);
        }
        public void LoadBlock(Block block)
        {
            if(!ContainsBlock(block.BlockID))
            {
                blockDictionary.Add(block.BlockID, block);
            }
        }
        public Block InstantiateBlock(int attachedBlockID, Vector3 normal, bool isBreakable, Block blockPrefab)
        {
            if(ContainsBlock(attachedBlockID))
            {
                Block attachedBlock = blockDictionary[attachedBlockID];
                Vector3 instantiatePoint = attachedBlock.GetInstantiatePoint(normal);
                Block newBlock = blockPrefab.BlockGenerator(instantiatePoint, normal, isBreakable, blockPrefab);
                LoadBlock(newBlock);
                return newBlock;
            }
            else
            {
                return null;
            }
        }
        public void DestroyBlock(int blockID)
        {
            if(ContainsBlock(blockID) && blockDictionary[blockID].IsBreakable)
            {
                blockDictionary[blockID].DestroyBlock();
                blockDictionary.Remove(blockID);
            }
        }
    }
}
