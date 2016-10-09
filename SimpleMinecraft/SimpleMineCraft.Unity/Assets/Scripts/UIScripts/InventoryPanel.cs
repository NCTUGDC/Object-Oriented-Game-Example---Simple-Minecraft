using UnityEngine;
using SimpleMinecraft.Unity.Scripts.SystemScripts;
using SimpleMinecraft.Library.PlayerElements;

namespace SimpleMinecraft.Unity.Scripts.UIScripts
{
    public class InventoryPanel : MonoBehaviour
    {
        private InventoryItemInfoIcon[] inventoryItemInfoIcons;
        public int columnCount = 10;

        [SerializeField]
        private InventoryItemInfoIcon inventoryItemInfoIconPrefab;

        void Awake()
        {
            inventoryItemInfoIcons = new InventoryItemInfoIcon[PlayerManager.Instance.Inventory.Capacity];
            PlayerManager.Instance.Inventory.OnItemChange += OnItemChange;
        }
        void Start()
        {
            ShowInventory();
        }

        void OnDestroy()
        {
            PlayerManager.Instance.Inventory.OnItemChange -= OnItemChange;
        }

        private void ShowInventory()
        {
            Inventory inventory = PlayerManager.Instance.Inventory;

            for (int i = 0; i < inventory.Capacity - inventory.HotKeyCapacity; i++)
            {
                inventoryItemInfoIcons[i] = Instantiate(inventoryItemInfoIconPrefab);
                RectTransform blockRectTransform = inventoryItemInfoIcons[i].GetComponent<RectTransform>();
                blockRectTransform.transform.SetParent(transform);
                blockRectTransform.localScale = Vector3.one;
                blockRectTransform.anchorMin = new Vector2(0, 1);
                blockRectTransform.anchorMax = new Vector2(0, 1);
                blockRectTransform.pivot = new Vector2(0.5f, 0.5f);
                float x = 30 + 55 * (i % columnCount);
                float y = 30 + 55 * (i / columnCount);
                blockRectTransform.anchoredPosition = new Vector2(x, -y);
                inventoryItemInfoIcons[i].InventoryItemInfo = new InventoryItemInfo(null, 0, i);
            }
            for (int i = inventory.Capacity - inventory.HotKeyCapacity; i < inventory.Capacity; i++)
            {
                inventoryItemInfoIcons[i] = Instantiate(inventoryItemInfoIconPrefab);
                RectTransform blockRectTransform = inventoryItemInfoIcons[i].GetComponent<RectTransform>();
                blockRectTransform.transform.SetParent(transform);
                blockRectTransform.localScale = Vector3.one;
                blockRectTransform.anchorMin = new Vector2(0, 1);
                blockRectTransform.anchorMax = new Vector2(0, 1);
                blockRectTransform.pivot = new Vector2(0.5f, 0.5f);
                float x = 30 + 55 * (i % columnCount);
                float y = 50 + 55 * (i / columnCount);
                blockRectTransform.anchoredPosition = new Vector2(x, -y);
                inventoryItemInfoIcons[i].InventoryItemInfo = new InventoryItemInfo(null, 0, i);
            }
            foreach (var info in inventory.ItemInfos)
            {
                inventoryItemInfoIcons[info.PositionIndex].InventoryItemInfo = info;
            }
        }
        private void OnItemChange(InventoryItemInfo info)
        {
            if (PlayerManager.Instance.Inventory.IsPositionIndexInRange(info.PositionIndex))
            {
                inventoryItemInfoIcons[info.PositionIndex].InventoryItemInfo = info;
            }
        }
    }
}
