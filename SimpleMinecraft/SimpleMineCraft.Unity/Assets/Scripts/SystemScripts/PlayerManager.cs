using UnityEngine;
using SimpleMinecraft.Library;
using SimpleMinecraft.Library.ItemElements;
using SimpleMinecraft.Library.PlayerElements;
using SimpleMinecraft.Unity.Scripts.UIScripts;

namespace SimpleMinecraft.Unity.Scripts.SystemScripts
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public Player Player { get; private set; }
        public Inventory Inventory { get { return Player.Inventory; } }
        public HotKeySet HotKeySet { get { return Player.HotKeySet; } }

        private Canvas canvas;
        [SerializeField]
        private InventoryPanel inventoryPanelPrefab;

        void Awake()
        {
            Instance = this;
            Player = new Player();
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }
        void Start()
        {
            InputManager.Instance.OnKeyDown += ToggleInventoryPanel;

            Item testBlock = new Item(1, "測試方塊", "");
            testBlock.AddComponent(new CubeBlockMaterial(1));

            Inventory.LoadItem(new InventoryItemInfo(testBlock, 50, 4));
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
