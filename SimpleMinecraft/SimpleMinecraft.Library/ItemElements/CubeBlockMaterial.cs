﻿using SimpleMinecraft.Library.SceneElements;

namespace SimpleMinecraft.Library.ItemElements
{
    public class CubeBlockMaterial : BlockMaterial
    {
        public float SideLength { get; private set; }
        public override Block GetBlockTemplate(Item item)
        {
            return new CubeBlock(SideLength, new Vector3(), true, item);
        }
        public CubeBlockMaterial(float sideLength)
        {
            SideLength = sideLength;
        }
    }
}
