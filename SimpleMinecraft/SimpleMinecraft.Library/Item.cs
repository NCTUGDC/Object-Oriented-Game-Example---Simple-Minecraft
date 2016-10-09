using SimpleMinecraft.Library.ItemElements;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMinecraft.Library
{
    public class Item
    {
        public int ItemID { get; protected set; }
        public string ItemName { get; protected set; }
        public string Description { get; protected set; }
        protected List<ItemComponent> components;
        public IEnumerable<ItemComponent> Components { get { return components; } }

        public Item(int itemID, string itemName, string description)
        {
            ItemID = itemID;
            ItemName = itemName;
            Description = description;
            components = new List<ItemComponent>();
        }
        public void AddComponent(ItemComponent component)
        {
            components.Add(component);
        }
        public void RemoveComponent(ItemComponent component)
        {
            components.Remove(component);
        }
        public void LoadComponents(IEnumerable<ItemComponent> components)
        {
            this.components.AddRange(components);
        }
        public void ClearComponents()
        {
            components.Clear();
        }
    }
}
