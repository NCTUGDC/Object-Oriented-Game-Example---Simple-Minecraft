using System;
using System.Collections.Generic;

namespace SimpleMinecraft.Library.PlayerElements
{
    public class HotKeySet
    {
        private static int hotKeySetCounter;

        private Player owner;
        public int HotKeySetID { get; private set; }
        private Dictionary<int, HotKeyInfo> hotKeyInfoDictionary;
        private Dictionary<short, HotKeyInfo> hotKeyInfoByKeyCodeDictionary;

        public IEnumerable<HotKeyInfo> HotKeyInfos { get { return hotKeyInfoDictionary.Values; } }

        private event Action<HotKeyInfo> onHotKeyInfoChange;
        public event Action<HotKeyInfo> OnHotKeyInfoChange { add { onHotKeyInfoChange += value; } remove { onHotKeyInfoChange -= value; } }

        public HotKeySet(Player owner)
        {
            hotKeySetCounter++;
            HotKeySetID = hotKeySetCounter;
            this.owner = owner;

            hotKeyInfoDictionary = new Dictionary<int, HotKeyInfo>();
            hotKeyInfoByKeyCodeDictionary = new Dictionary<short, HotKeyInfo>();
        }

        public bool ContainsHotKeyInfo(int hotKeyInfoID)
        {
            return hotKeyInfoDictionary.ContainsKey(hotKeyInfoID);
        }
        public bool ContainsHotKeyInfo(short keyCode)
        {
            return hotKeyInfoByKeyCodeDictionary.ContainsKey(keyCode);
        }

        public void LoadHotKeyInfos(List<HotKeyInfo> hotkeyInfos)
        {
            foreach (HotKeyInfo info in hotkeyInfos)
            {
                if (!ContainsHotKeyInfo(info.HotKeyInfoID) && !ContainsHotKeyInfo(info.HotKeyCode))
                {
                    hotKeyInfoDictionary.Add(info.HotKeyInfoID, info);
                    hotKeyInfoByKeyCodeDictionary.Add(info.HotKeyCode, info);
                }
            }
        }
        public void SetHotKeyInfo(HotKeyInfo hotKeyInfo)
        {
            if (hotKeyInfo.InventoryItemInfo == null)
            {
                if (ContainsHotKeyInfo(hotKeyInfo.HotKeyCode))
                {
                    hotKeyInfo = hotKeyInfoByKeyCodeDictionary[hotKeyInfo.HotKeyCode];
                    if (ContainsHotKeyInfo(hotKeyInfo.HotKeyInfoID))
                    {
                        hotKeyInfoDictionary.Remove(hotKeyInfo.HotKeyInfoID);
                    }
                    hotKeyInfoByKeyCodeDictionary.Remove(hotKeyInfo.HotKeyCode);

                    onHotKeyInfoChange?.Invoke(new HotKeyInfo(hotKeyInfo.HotKeyCode, null));
                }
            }
            else if (!ContainsHotKeyInfo(hotKeyInfo.HotKeyInfoID) && !ContainsHotKeyInfo(hotKeyInfo.HotKeyCode))
            {
                if (hotKeyInfo != null)
                {
                    hotKeyInfoDictionary.Add(hotKeyInfo.HotKeyInfoID, hotKeyInfo);
                    hotKeyInfoByKeyCodeDictionary.Add(hotKeyInfo.HotKeyCode, hotKeyInfo);
                    onHotKeyInfoChange?.Invoke(hotKeyInfo);
                }
            }
            else
            {
                if (ContainsHotKeyInfo(hotKeyInfo.HotKeyInfoID))
                {
                    hotKeyInfoDictionary[hotKeyInfo.HotKeyInfoID] = hotKeyInfo;
                }
                if (ContainsHotKeyInfo(hotKeyInfo.HotKeyCode))
                {
                    hotKeyInfoByKeyCodeDictionary[hotKeyInfo.HotKeyCode] = hotKeyInfo;
                }
                onHotKeyInfoChange?.Invoke(hotKeyInfo);
            }
        }
    }
}
