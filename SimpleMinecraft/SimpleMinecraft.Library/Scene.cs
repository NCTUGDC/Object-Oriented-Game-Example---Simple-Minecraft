using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleMinecraft.Library.SceneElements;

namespace SimpleMinecraft.Library
{
    public class Scene
    {
        private Dictionary<int, Block> blockDictionary;

        public Scene()
        {
            blockDictionary = new Dictionary<int, Block>();
        }

        public bool ContainsBlock(int blockID)
        {
            return blockDictionary.ContainsKey(blockID);
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
