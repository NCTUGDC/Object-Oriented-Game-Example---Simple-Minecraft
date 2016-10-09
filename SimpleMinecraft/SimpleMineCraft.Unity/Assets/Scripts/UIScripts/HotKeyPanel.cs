using SimpleMinecraft.Library.PlayerElements;
using SimpleMinecraft.Unity.Scripts.SystemScripts;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMinecraft.Unity.Scripts.UIScripts
{
    public class HotKeyPanel : MonoBehaviour
    {
        private HotKeySet hotKeySet;

        public struct HotKeyDisplayInfo
        {
            public static readonly HotKeyDisplayInfo[] HotKeyMappingTable;
            static HotKeyDisplayInfo()
            {
                HotKeyMappingTable = new HotKeyDisplayInfo[]
                {
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha1, labelName = "1" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha2, labelName = "2" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha3, labelName = "3" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha4, labelName = "4" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha5, labelName = "5" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha6, labelName = "6" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha7, labelName = "7" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha8, labelName = "8" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha9, labelName = "9" },
                    new HotKeyDisplayInfo { keyCode = KeyCode.Alpha0, labelName = "0" }
                };
            }

            public KeyCode keyCode;
            public string labelName;
        }
        [SerializeField]
        private HotKeyInfoIcon hotKeyInfoIconPrefab;

        private Dictionary<KeyCode, HotKeyInfoIcon> hotKeyInfoIconDictionary;

        public HotKeyPanel()
        {
            hotKeyInfoIconDictionary = new Dictionary<KeyCode, HotKeyInfoIcon>();
        }
        
        void Start()
        {
            hotKeySet = PlayerManager.Instance.HotKeySet;
            hotKeySet.OnHotKeyInfoChange += OnSetHotKey;
            Inventory inventory = PlayerManager.Instance.Inventory;
            inventory.OnItemChange += (info) =>
            {
                int hotKeyIndex = info.PositionIndex - (inventory.Capacity - inventory.HotKeyCapacity);
                if (hotKeyIndex >=0 && hotKeyIndex < inventory.HotKeyCapacity)
                {
                    hotKeySet.SetHotKeyInfo(new HotKeyInfo((short)HotKeyDisplayInfo.HotKeyMappingTable[hotKeyIndex].keyCode, info));
                }
            };

            for (int i = 0; i < 10; i++)
            {
                HotKeyInfoIcon icon = Instantiate(hotKeyInfoIconPrefab);
                icon.transform.SetParent(transform);
                RectTransform iconTransform = icon.GetComponent<RectTransform>();
                iconTransform.localScale = Vector3.one;
                iconTransform.anchorMin = new Vector2(0, 0.5f);
                iconTransform.anchorMax = new Vector2(0, 0.5f);
                iconTransform.pivot = new Vector2(0.5f, 0.5f);
                float x = 30 + 55 * (i % 10);
                iconTransform.anchoredPosition = new Vector2(x, 0);
                icon.Initial(HotKeyDisplayInfo.HotKeyMappingTable[i].labelName);
                hotKeyInfoIconDictionary.Add(HotKeyDisplayInfo.HotKeyMappingTable[i].keyCode, icon);
            }
            foreach (var info in hotKeySet.HotKeyInfos)
            {
                if (hotKeyInfoIconDictionary.ContainsKey((KeyCode)info.HotKeyCode))
                {
                    hotKeyInfoIconDictionary[(KeyCode)info.HotKeyCode].HotKeyInfo = info;
                }
            }
        }
        private void OnSetHotKey(HotKeyInfo info)
        {
            if (hotKeyInfoIconDictionary.ContainsKey((KeyCode)info.HotKeyCode))
            {
                HotKeyInfoIcon icon = hotKeyInfoIconDictionary[(KeyCode)info.HotKeyCode];
                icon.HotKeyInfo = info;
            }
        }
    }
}
