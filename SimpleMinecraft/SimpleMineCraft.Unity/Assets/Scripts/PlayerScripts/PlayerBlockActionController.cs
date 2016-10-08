using SimpleMinecraft.Library.SceneElements;
using SimpleMinecraft.Unity.Scripts.SystemScripts;
using UnityEngine;

namespace SimpleMinecraft.Unity.Scripts.PlayerScripts
{
    public class PlayerBlockActionController : MonoBehaviour
    {
        [SerializeField]
        private GameObject blockGameObjectPrefab;
        [SerializeField]
        private GameObject settingBlockGameObject;

        private Block blockPrefab;

        void Start()
        {
            InputManager.Instance.OnMouseButtonUp += OnInstantiateBlockMouseButtonUp;
            InputManager.Instance.OnMouseButtonUp += OnDestroyBlockMouseButtonUp;
            blockPrefab = new CubeBlock(1, new Library.Vector3(), true);

            settingBlockGameObject = Instantiate(settingBlockGameObject);
            settingBlockGameObject.SetActive(false);
        }
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Vector3 position = hitInfo.point;
                Vector3 normal = hitInfo.normal;
                if (hitInfo.collider.GetComponent<IBlockController>() != null)
                {
                    settingBlockGameObject.SetActive(true);
                    Block hitBlock = hitInfo.collider.GetComponent<IBlockController>().Block;
                    Library.Vector3 instantiatePoint = hitBlock.GetInstantiatePoint(Vector3Convertor.Convert(normal));
                    Library.Vector3 newBlockCenter = hitBlock.BlockCenterGenerator(instantiatePoint, Vector3Convertor.Convert(normal), blockPrefab);
                    settingBlockGameObject.transform.position = Vector3Convertor.Convert(newBlockCenter);
                }
                else
                {
                    settingBlockGameObject.SetActive(false);
                }
            }
            else
            {
                settingBlockGameObject.SetActive(false);
            }
        }
        private void OnInstantiateBlockMouseButtonUp(int mouseButton)
        {
            if (mouseButton == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    Vector3 position = hitInfo.point;
                    Vector3 normal = hitInfo.normal;
                    if (hitInfo.collider.GetComponent<IBlockController>() != null)
                    {
                        Block hitBlock = hitInfo.collider.GetComponent<IBlockController>().Block;
                        Library.Vector3 instantiatePoint = hitBlock.GetInstantiatePoint(Vector3Convertor.Convert(normal));
                        Block newBlock = SceneManager.Instance.InstantiateBlock(hitBlock.BlockID, Vector3Convertor.Convert(normal), true, blockPrefab);

                        GameObject blockGameObject = Instantiate(blockGameObjectPrefab);
                        newBlock.BindController(blockGameObject.GetComponent<IBlockController>());
                        blockGameObject.transform.position = Vector3Convertor.Convert(newBlock.CenterPosition);
                    }
                }
            }
        }
        private void OnDestroyBlockMouseButtonUp(int mouseButton)
        {
            if (mouseButton == 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    Vector3 position = hitInfo.point;
                    Vector3 normal = hitInfo.normal;
                    if (hitInfo.collider.GetComponent<IBlockController>() != null)
                    {
                        Block hitBlock = hitInfo.collider.GetComponent<IBlockController>().Block;
                        SceneManager.Instance.DestroyBlock(hitBlock.BlockID);
                    }
                }
            }
        }
    }
}
