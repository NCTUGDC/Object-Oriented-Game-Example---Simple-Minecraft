using System.Collections.Generic;

namespace SimpleMinecraft.Library.ItemElements
{
    public abstract class ItemComponent
    {
        public abstract bool Affect(List<IEffectorTarget> targets);
    }
}
