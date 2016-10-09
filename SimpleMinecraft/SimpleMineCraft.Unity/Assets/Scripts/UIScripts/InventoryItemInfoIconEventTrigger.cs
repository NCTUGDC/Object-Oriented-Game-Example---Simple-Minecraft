using SimpleMinecraft.Library.PlayerElements;
using System;
using UnityEngine.EventSystems;

namespace SimpleMinecraft.Unity.Scripts.UIScripts
{
    public class InventoryItemInfoIconEventTrigger : EventTrigger
    {
        private event Action<PointerEventData> onStartDrag;
        public event Action<PointerEventData> OnStartDrag { add { onStartDrag += value; } remove { onStartDrag -= value; } }

        private event Action<PointerEventData> onStopDrag;
        public event Action<PointerEventData> OnStopDrag { add { onStopDrag += value; } remove { onStopDrag -= value; } }

        private event Action<PointerEventData> onDragging;
        public event Action<PointerEventData> OnDragging { add { onDragging += value; } remove { onDragging -= value; } }

        private event Action<InventoryItemInfo> onDisplayInventoryItemInfo;
        public event Action<InventoryItemInfo> OnDisplayInventoryItemInfo { add { onDisplayInventoryItemInfo += value; } remove { onDisplayInventoryItemInfo -= value; } }

        private event Action onDiscardInventoryItemInfo;
        public event Action OnDiscardInventoryItemInfo { add { onDiscardInventoryItemInfo += value; } remove { onDiscardInventoryItemInfo -= value; } }

        protected InventoryItemInfoIcon iconForDisplay;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            if (onStartDrag != null)
                onStartDrag.Invoke(eventData);
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (onStopDrag != null)
                onStopDrag.Invoke(eventData);
            EventSystem.current.SetSelectedGameObject(null);
            if(iconForDisplay == null)
            {
                if (onDiscardInventoryItemInfo != null)
                {
                    onDiscardInventoryItemInfo.Invoke();
                }
            }
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            if (onDragging != null)
                onDragging.Invoke(eventData);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (eventData.selectedObject != null && eventData.selectedObject.GetComponent<InventoryItemInfoIcon>() != null)
            {
                iconForDisplay = eventData.selectedObject.GetComponent<InventoryItemInfoIcon>();
            }
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            iconForDisplay = null;
        }
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (iconForDisplay != null)
            {
                if (onDisplayInventoryItemInfo != null)
                {
                    onDisplayInventoryItemInfo.Invoke(iconForDisplay.InventoryItemInfo);
                }
            }
        }
    }
}
