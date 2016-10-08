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

        void Awake()
        {
            Instance = this;
            scene = new Scene();

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
