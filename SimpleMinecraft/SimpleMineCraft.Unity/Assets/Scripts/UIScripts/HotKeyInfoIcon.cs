using SimpleMinecraft.Library.PlayerElements;
using SimpleMinecraft.Unity.Scripts.SystemScripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleMinecraft.Unity.Scripts.UIScripts
{
    public class HotKeyInfoIcon : MonoBehaviour
    {
        private HotKeyInfo hotKeyInfo;
        public HotKeyInfo HotKeyInfo
        {
            private get { return hotKeyInfo; }
            set
            {
                hotKeyInfo = value;
                if (hotKeyInfo.InventoryItemInfo != null && hotKeyInfo.InventoryItemInfo.Item != null)
                {
                    iconText.text = hotKeyInfo.InventoryItemInfo.Item.ItemName;
                    itemCountText.text = hotKeyInfo.InventoryItemInfo.Count.ToString();
                }
                else
                {
                    iconText.text = "";
                    itemCountText.text = "";
                }
            }
        }
        private Text hotKeyCodeText;
        private Text iconText;
        private Text itemCountText;

        void Awake()
        {
            hotKeyCodeText = transform.Find("HotKeyCodeLabelText").GetComponent<Text>();
            iconText = transform.Find("IconText").GetComponent<Text>();
            itemCountText = transform.Find("ItemCountText").GetComponent<Text>();

            InputManager.Instance.OnKeyDown += UseHotKey;
        }
        void OnDestroy()
        {
            InputManager.Instance.OnKeyDown -= UseHotKey;
        }
        public void Initial(string hotKeyCodeLabel)
        {
            hotKeyCodeText.text = hotKeyCodeLabel;
            iconText.text = "";
            itemCountText.text = "";
        }
        private void UseHotKey(KeyCode keyCode)
        {
            if (HotKeyInfo != null && (short)keyCode == HotKeyInfo.HotKeyCode)
            {
                PlayerManager.Instance.Player.HoldingItemInfo = HotKeyInfo.InventoryItemInfo;
            }
        }
    }
}
