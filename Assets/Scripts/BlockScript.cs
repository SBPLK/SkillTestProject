using UnityEngine;
using UnityEngine.UI;

public class BlockClickEvent : MonoBehaviour
{
    [SerializeField]private GameObject BlockEventBtn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        BlockEventBtn.SetActive(true);


        Debug.Log(11111);
    }
}
