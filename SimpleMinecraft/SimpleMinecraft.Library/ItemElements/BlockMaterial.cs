using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMinecraft.Library.SceneElements;

namespace SimpleMinecraft.Library.ItemElements
{
    public abstract class BlockMaterial : ItemComponent
    {
        public abstract Block BlockTemplate { get; } 
    }
}
