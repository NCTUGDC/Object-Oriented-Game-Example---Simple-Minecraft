using SimpleMinecraft.Library;
using SimpleMinecraft.Library.SceneElements;
using SimpleMinecraft.Unity.Scripts.SceneElementScripts;
using UnityEngine;

namespace SimpleMinecraft.Unity.Scripts.SystemScripts
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }
        [SerializeField]
        private CubeBlockController originBlockController;
        private Scene scene;
        public UnityEngine.Vector3 OriginPoint { get { return Vector3Convertor.Convert(scene.OriginPoint); } }
        public float ResetPositionY { get { return scene.ResetPositionY; } }

        void Awake()
        {
            Instance = this;
            scene = new Scene(new Library.Vector3 { x = 0, y = 1.68f, z = 0 }, -10);

            Block originBlock = new CubeBlock(1, new Library.Vector3 { x = 0, y = -0.5f, z = 0 }, false);
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
    }
}
