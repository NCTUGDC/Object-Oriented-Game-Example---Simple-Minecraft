using SimpleMinecraft.Library.PlayerElements;
using SimpleMinecraft.Library.SceneElements;
using SimpleMinecraft.Unity.Scripts.SceneElementScripts;
using SimpleMinecraft.Unity.Scripts.SystemScripts;
using UnityEngine;

namespace SimpleMinecraft.Unity.Scripts.PlayerScripts
{
    public class PlayerCollectItemEntityController : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "ItemEntity" && other.GetComponent<ItemEntityController>() != null)
            {
                Debug.Log("col");
                ItemEntity itemEntity = other.GetComponent<ItemEntityController>().ItemEntity;
                SceneManager.Instance.DestroyItemEntity(itemEntity);
                InventoryItemInfo info;
                PlayerManager.Instance.Inventory.AddItem(itemEntity.Item, 1, out info);
            }
        }
    }
}
