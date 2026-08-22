using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BlockEvent : MonoBehaviour
{
    private GameObject Block;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isHovered = EventSystem.current.IsPointerOverGameObject() &&
                         EventSystem.current.currentSelectedGameObject == transform.gameObject;
        if (Mouse.current.leftButton.wasPressedThisFrame && !isHovered)
        {
            transform.gameObject.SetActive(false);
            Block = null;
        }
    }

    public void SetBlock(GameObject block) 
    {
        Block = block;
    }

    public GameObject GetBlock()
    {
        return Block;
    }

    public void DestroyTargetBlock() 
    { 
        if (Block != null) { Destroy(Block); }
        transform.gameObject.SetActive(false);
    }
}
