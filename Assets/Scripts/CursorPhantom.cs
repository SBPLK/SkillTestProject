using UnityEngine;
using UnityEngine.InputSystem;

public class CursorPhantom : MonoBehaviour
{
    [System.Serializable]
    private struct BlockData
    {
        // public GameObject BlockPrefab;
        public Mesh BlockMesh;
        public float BlockWidth;
        public float BlockHalfHeight;
    }
    [SerializeField]private BlockData[] m_Blocks = new BlockData[5];

    public GameObject BlockPrefab;
    public GameObject PhantomPrefab;
    public bool hasPhantom;
    private GameObject Phantom;
    private int PhantomIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPhantom && Phantom != null)
        { 
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Debug.Log("Left mouse button clicked (New Input System)!");
                float width = m_Blocks[PhantomIndex].BlockWidth;
                float height = m_Blocks[PhantomIndex].BlockHalfHeight;

            }
        }
    }

    private void FixedUpdate()
    {
        
        if (hasPhantom && Phantom != null) {
            Debug.Log(GetMousePos());
            Phantom.transform.position = GetMousePos();
            
        }
        

    }

    private Vector3 GetMousePos() 
    {
        Plane targetPlane = new Plane(Vector3.up, Vector3.zero);

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
        Phantom = Instantiate(BlockPrefab);

        Phantom.SetActive(true);
        hasPhantom = true;
        
    }

    public void HidePhamtom()
    {
        hasPhantom = false;
        if (Phantom == null) { return; }
        Phantom.SetActive(false);
        Phantom = null;
    }

    private void SpawnBlock(int index)
    { 
    
    }
}
