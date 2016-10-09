using SimpleMinecraft.Library.SceneElements;
using SimpleMinecraft.Library.ItemElements;
using SimpleMinecraft.Unity.Scripts.SystemScripts;
using UnityEngine;
using System.Linq;

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
            PlayerManager.Instance.Player.OnHoldingItemInfoChange += (info) =>
            {
                if(info != null && info.Item != null && info.Item.Components.Any(x => x is BlockMaterial))
                {
                    BlockMaterial blockMaterial = info.Item.Components.First(x => x is BlockMaterial) as BlockMaterial;
                    blockPrefab = blockMaterial.BlockTemplate;
                }
                else
                {
                    blockPrefab = null;
                }
            };

            settingBlockGameObject = Instantiate(settingBlockGameObject);
            settingBlockGameObject.SetActive(false);
        }
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (blockPrefab != null && Physics.Raycast(ray, out hitInfo))
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
            if (blockPrefab != null && mouseButton == 1)
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

                        PlayerManager.Instance.Inventory.RemoveItem(PlayerManager.Instance.Player.HoldingItemInfo.PositionIndex, 1);
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
