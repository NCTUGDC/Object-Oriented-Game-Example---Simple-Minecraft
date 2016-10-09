using SimpleMinecraft.Library;
using SimpleMinecraft.Library.ItemElements;
using SimpleMinecraft.Library.SceneElements;
using SimpleMinecraft.Unity.Scripts.SceneElementScripts;
using UnityEngine;

namespace SimpleMinecraft.Unity.Scripts.SystemScripts
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        [SerializeField]
        private Transform sceneElementsTransform;
        [SerializeField]
        private CubeBlockController originBlockController;
        [SerializeField]
        private ItemEntityController itemEntityGameObjectPrefab;

        private Scene scene;
        public UnityEngine.Vector3 OriginPoint { get { return Vector3Convertor.Convert(scene.OriginPoint); } }
        public float ResetPositionY { get { return scene.ResetPositionY; } }
        public Item SupportItem { get; private set; }

        void Awake()
        {
            Instance = this;
            scene = new Scene(new Library.Vector3 { x = 0, y = 1.68f, z = 0 }, -10);
            scene.OnItemEntityChange += OnItemEntityChange;

            SupportItem = new Item(1, "測試方塊", "");
            SupportItem.AddComponent(new CubeBlockMaterial(1));

            Block originBlock = new CubeBlock(1, new Library.Vector3 { x = 0, y = -0.5f, z = 0 }, false, SupportItem);
            originBlock.BindController(originBlockController);

            scene.LoadBlock(originBlock);
        }

        public Block InstantiateBlock(int attachedBlockID, Library.Vector3 normal, bool isBreakable, Block blockPrefab)
        {
            return scene.InstantiateBlock(attachedBlockID, normal, isBreakable, blockPrefab);
        }
        public void DestroyBlock(int blockID)
        {
            scene.DestroyBlock(blockID);
        }
        public void InstantiateItemEntity(Item item, Library.Vector3 position)
        {
            scene.InstantiateItemEntity(item, position);
        }
        public void DestroyItemEntity(ItemEntity itemEntity)
        {
            scene.DestroyItemEntity(itemEntity);
        }
        public void OnItemEntityChange(ItemEntity itemEntity, bool isCreate)
        {
            if(isCreate)
            {
                ItemEntityController controller = Instantiate(itemEntityGameObjectPrefab);
                controller.transform.SetParent(sceneElementsTransform);
                controller.transform.localPosition = Vector3Convertor.Convert(itemEntity.Position);
                itemEntity.BindController(controller);
            }
            else
            {
                itemEntity.Destroy();
            }
        }
    }
}
