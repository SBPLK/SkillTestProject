using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]private int btnIndex;
    private CursorPhantom phantomManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject phantomManagerObject = GameObject.Find("CursorPhantomManager");
        if (phantomManagerObject != null)
        {
            phantomManager = phantomManagerObject.GetComponent<CursorPhantom>();

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin");
        phantomManager.ShowPhantom(btnIndex);
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End");
        //phantomManager.HidePhamtom();

    }
}
