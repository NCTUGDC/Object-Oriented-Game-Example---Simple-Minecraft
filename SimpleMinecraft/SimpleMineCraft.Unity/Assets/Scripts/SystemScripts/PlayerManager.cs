using UnityEngine;
using SimpleMinecraft.Library;
using SimpleMinecraft.Library.PlayerElements;
using SimpleMinecraft.Unity.Scripts.UIScripts;

namespace SimpleMinecraft.Unity.Scripts.SystemScripts
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        private Player player;
        public Inventory Inventory { get { return player.Inventory; } }

        [SerializeField]
        private InventoryPanel inventoryPanelPrefab;
        private Canvas canvas;

        void Awake()
        {
            Instance = this;
            player = new Player();
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }
        void Start()
        {
            InputManager.Instance.OnKeyDown += ToggleInventoryPanel;

            Inventory.LoadItem(new InventoryItemInfo(new Item(1, "測試", ""), 5, 4));
            Inventory.LoadItem(new InventoryItemInfo(new Item(8, "測試2", ""), 8, 12));
        }

        private void ToggleInventoryPanel(KeyCode keycode)
        {
            if (keycode == KeyCode.I)
            {
                if (canvas.transform.FindChild("InventoryPanel") == null)
                {
                    InventoryPanel inventoryPanel = Instantiate(inventoryPanelPrefab);
                    inventoryPanel.name = "InventoryPanel";
                    inventoryPanel.transform.SetParent(canvas.transform);
                    RectTransform rectTransform = inventoryPanel.GetComponent<RectTransform>();
                    rectTransform.localScale = UnityEngine.Vector3.one;
                    rectTransform.localPosition = Vector2.zero;
                }
                else
                {
                    Destroy(canvas.transform.FindChild("InventoryPanel").gameObject);
                }
            }
        }
    }
}
