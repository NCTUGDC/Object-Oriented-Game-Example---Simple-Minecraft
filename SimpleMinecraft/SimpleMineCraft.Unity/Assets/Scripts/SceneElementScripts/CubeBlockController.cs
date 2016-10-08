using SimpleMinecraft.Library.SceneElements;
using UnityEngine;

namespace SimpleMinecraft.Unity.Scripts.SceneElementScripts
{
    public class CubeBlockController : MonoBehaviour, IBlockController
    {
        private Block block;
        public Block Block { get { return block; } set { block = value; } }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
