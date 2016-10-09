using SimpleMinecraft.Library.SceneElements;
using System.Collections.Generic;
using System;

namespace SimpleMinecraft.Library
{
    public class Scene
    {
        private Dictionary<int, Block> blockDictionary;
        private Dictionary<int, ItemEntity> itemEntityDictionary;

        public Vector3 OriginPoint { get; private set; }
        public float ResetPositionY { get; private set; }

        private event Action<ItemEntity, bool> onItemEntityChange;
        public event Action<ItemEntity, bool> OnItemEntityChange { add { onItemEntityChange += value; } remove { onItemEntityChange -= value; } }

        public Scene(Vector3 originPoint, float resetPositionY)
        {
            blockDictionary = new Dictionary<int, Block>();
            itemEntityDictionary = new Dictionary<int, ItemEntity>();

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
                Block block = blockDictionary[blockID];
                block.DestroyBlock();
                blockDictionary.Remove(blockID);
                InstantiateItemEntity(block.Item, block.CenterPosition);
            }
        }
        public bool ContainsItemEntity(int itemEntityID)
        {
            return itemEntityDictionary.ContainsKey(itemEntityID);
        }
        public void InstantiateItemEntity(Item item, Vector3 position)
        {
            ItemEntity itemEntity = new ItemEntity(item, position);
            itemEntityDictionary.Add(itemEntity.ItemEntityID, itemEntity);

            onItemEntityChange?.Invoke(itemEntity, true);
        }
        public void DestroyItemEntity(ItemEntity itemEntity)
        {
            if(ContainsItemEntity(itemEntity.ItemEntityID))
            {
                itemEntityDictionary.Remove(itemEntity.ItemEntityID);

                onItemEntityChange?.Invoke(itemEntity, false);
            }
        }
    }
}
