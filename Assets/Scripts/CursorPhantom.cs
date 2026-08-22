using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorPhantom : MonoBehaviour
{
    [System.Serializable]
    private struct BlockData
    {
        // public GameObject BlockPrefab;
        public Mesh BlockMesh;
        public float BlockWidth;
        public float BlockHalfHeight;
        public Color Color;
    }
    [SerializeField] private GameObject BlockEventBtn;

    [SerializeField] private BlockData[] m_Blocks = new BlockData[5];

    public GameObject BlockPrefab;
    public GameObject PhantomPrefab;
    public bool hasPhantom;
    public GameObject Phantom;
    public GameObject LocatorPhantom;
    private int PhantomIndex;
    [SerializeField] private GameObject lineObject;

    public LayerMask clickLayerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = new Vector3();
        if (hasPhantom && Phantom != null)
        {
            Phantom.transform.position = GetMousePos();
            lineObject.transform.position = GetMousePos();

            float width = m_Blocks[PhantomIndex].BlockWidth;
            float height = m_Blocks[PhantomIndex].BlockHalfHeight;

            RaycastHit hit;

            Ray midRay = new Ray(Phantom.transform.position, Vector3.down);
            if (Physics.Raycast(midRay, out hit))
            {
                // 4. Get the exact 3D world coordinates of that intersection point
                worldPosition = Phantom.transform.position - new Vector3(0, hit.distance, 0) + new Vector3(0, height, 0);
                LocatorPhantom.transform.position = worldPosition;
                lineObject.transform.localScale = new Vector3(lineObject.transform.localScale.x, Mathf.Max((hit.distance - height * 2), 0), lineObject.transform.localScale.z);

            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            // Debug.Log(worldPosition);
            if (hasPhantom && Phantom != null)
            {
                if (worldPosition.y > 0)
                {
                    SpawnBlock(PhantomIndex, worldPosition);
                }
                HidePhamtom();
            }

        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {

            int layerMask = ~clickLayerMask;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                BlockEvent BlockBtn = BlockEventBtn.GetComponent<BlockEvent>();
                // 4. Get the exact 3D world coordinates of that intersection point
                if (hit.rigidbody != null && BlockBtn.GetBlock() == null) 
                {
                
                    BlockEventBtn.SetActive(true);
                    BlockBtn.SetBlock(hit.transform.gameObject);
                    BlockEventBtn.GetComponent<RectTransform>().SetPositionAndRotation(Mouse.current.position.ReadValue(), new Quaternion(0, 0, 0, 1));
                
                }
                else
                {
                    //BlockEventBtn.SetActive(false);
                    //BlockEventBtn.GetComponent<BlockEvent>().SetBlock(null);
                }

            }
        }
    }


    private Vector3 GetMousePos() 
    {
        Plane targetPlane = new Plane(Vector3.back, new Vector3(0.19f, 0,-1.8f));

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // 3. Determine where the ray intersects the plane
        if (targetPlane.Raycast(ray, out float enterDistance))
        {
            // 4. Get the exact 3D world coordinates of that intersection point
            Vector3 worldPosition = ray.GetPoint(enterDistance);

            
            return worldPosition;
        }

        return new Vector3(0, 0, 0);
    }

    public void ShowPhantom(int index)
    { 
        if (BlockPrefab == null) { return; }
        // Phantom = Instantiate(PhantomPrefab);
        // LocatorPhantom = Instantiate(PhantomPrefab);
        PhantomIndex = index;
        Phantom.GetComponent<MeshFilter>().mesh = m_Blocks[PhantomIndex].BlockMesh;
        LocatorPhantom.GetComponent<MeshFilter>().mesh = m_Blocks[PhantomIndex].BlockMesh;

        Phantom.SetActive(true);
        LocatorPhantom.SetActive(true);
        hasPhantom = true;

        lineObject.SetActive(true);
        
    }

    public void HidePhamtom()
    {
        hasPhantom = false;
        PhantomIndex = 0;
        if (LocatorPhantom != null) 
        { 
            LocatorPhantom.SetActive(false);
        }
        if (Phantom == null) { return; }
        Phantom.SetActive(false);

        lineObject.SetActive(false);
    }

    private void SpawnBlock(int index, Vector3 Pos)
    { 
        if (index < m_Blocks.Length)
        {
            GameObject SpawnBlock = Instantiate(BlockPrefab, Pos, new Quaternion(-0.707106888f, 0, 0, 0.707106709f));
            SpawnBlock.GetComponent<MeshCollider>().sharedMesh = m_Blocks[index].BlockMesh;
            SpawnBlock.GetComponent<MeshFilter>().mesh = m_Blocks[index].BlockMesh;
            SpawnBlock.GetComponent<Renderer>().material.color = m_Blocks[index].Color;
        }
    }
}
