using SimpleMinecraft.Library.SceneElements;
using UnityEngine;

namespace SimpleMinecraft.Unity.Scripts.SceneElementScripts
{
    class ItemEntityController : MonoBehaviour, IItemEntityController
    {
        private ItemEntity itemEntity;
        private float randomValue;

        public ItemEntity ItemEntity
        {
            get { return itemEntity; }
            set
            {
                itemEntity = value;
                gameObject.name = "ItemEntity" + itemEntity.ItemEntityID;

                transform.localPosition = Vector3Convertor.Convert(itemEntity.Position);
                randomValue = Random.Range(0, 360);
                transform.Rotate(randomValue * Vector3.up);
            }
        }

        void Update()
        {
            transform.Rotate(Time.deltaTime * Vector3.up * 25);
            transform.localPosition = Vector3Convertor.Convert(itemEntity.Position) + (Vector3.up * Mathf.Sin(Time.time + randomValue) * 0.05f);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
