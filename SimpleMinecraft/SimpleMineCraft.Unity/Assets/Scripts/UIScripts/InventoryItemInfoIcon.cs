﻿using SimpleMinecraft.Library.PlayerElements;
using SimpleMinecraft.Unity.Scripts.SystemScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleMinecraft.Unity.Scripts.UIScripts
{
    public class InventoryItemInfoIcon : MonoBehaviour
    {        
        private InventoryItemInfoIconEventTrigger eventTrigger;

        private InventoryItemInfo inventoryItemInfo;
        public InventoryItemInfo InventoryItemInfo
        {
            get { return inventoryItemInfo; }
            set
            {
                inventoryItemInfo = value;
                if (InventoryItemInfo != null && InventoryItemInfo.Item != null)
                {
                    iconText.text = InventoryItemInfo.Item.ItemName;
                    itemCountText.text = InventoryItemInfo.Count.ToString();
                }
                else
                {
                    iconText.text = "";
                    itemCountText.text = "";
                }
            }
        }
        private Text iconText;
        private Text itemCountText;
        private InventoryItemInfoIcon draggingIcon;
        private Vector2 pressPosition;
        void Awake()
        {
            eventTrigger = GetComponent<InventoryItemInfoIconEventTrigger>();
            eventTrigger.OnStartDrag += OnStartDrag;
            eventTrigger.OnStopDrag += OnStopDrag;
            eventTrigger.OnDragging += OnDragging;
            eventTrigger.OnDisplayInventoryItemInfo += OnDisplayInventoryItemInfo;
            eventTrigger.OnDiscardInventoryItemInfo += OnDiscardInventoryItemInfo;

            iconText = transform.Find("IconText").GetComponent<Text>();
            itemCountText = transform.Find("ItemCountText").GetComponent<Text>();
        }

        private void OnStartDrag(PointerEventData eventData)
        {
            if (InventoryItemInfo.Item != null)
            {
                pressPosition = eventData.pressPosition;

                draggingIcon = Instantiate(this);
                draggingIcon.transform.SetParent(GameObject.Find("Canvas").transform);
                draggingIcon.gameObject.layer = 0;
                RectTransform rectTransform = draggingIcon.GetComponent<RectTransform>();
                rectTransform.localScale = Vector3.one;
                rectTransform.position = transform.position;
                draggingIcon.GetComponent<Selectable>().interactable = false;
                draggingIcon.GetComponent<Image>().raycastTarget = false;
            }
        }
        private void OnStopDrag(PointerEventData eventData)
        {
            if (draggingIcon != null)
            {
                Destroy(draggingIcon.gameObject);
                draggingIcon = null;
            }
        }
        private void OnDragging(PointerEventData eventData)
        {
            if (draggingIcon != null)
            {
                Vector3 displacement = eventData.position - pressPosition;
                draggingIcon.GetComponent<RectTransform>().position = transform.position + displacement;
            }
        }
        private void OnDisplayInventoryItemInfo(InventoryItemInfo inventoryItemInfo)
        {
            if (inventoryItemInfo != null && inventoryItemInfo.Item != null)
            {
                PlayerManager.Instance.Inventory.SwapItemInfo(InventoryItemInfo.PositionIndex, inventoryItemInfo.PositionIndex);
            }
        }
        private void OnDiscardInventoryItemInfo()
        {
            if(inventoryItemInfo != null && inventoryItemInfo.Item != null)
            {
                if(PlayerManager.Instance.Inventory.RemoveItem(inventoryItemInfo.PositionIndex, 1))
                {
                    SceneManager.Instance.InstantiateItemEntity(inventoryItemInfo.Item, Vector3Convertor.Convert(PlayerManager.Instance.PlayerFrontPosition));
                }
            }
        }
    }
}
