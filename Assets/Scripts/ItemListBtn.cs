using System.Runtime.CompilerServices;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemListBtn : MonoBehaviour
{
    private bool showList;
    private Button listBtn;

    [SerializeField] private GameObject RaycastReciever;

    [SerializeField] private GameObject[] Buttons = new GameObject[5];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listBtn = transform.GetComponentInParent<Button>();
        Debug.Log(listBtn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ListBtnOnClick()
    {
        if (listBtn != null)
        {
            RectTransform RaycastRecieverRect = RaycastReciever.GetComponent<RectTransform>();
            showList = !showList;
            if (showList)
            {
                RaycastRecieverRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100.0f * Buttons.Length + 30.0f);
            }
            else
            {
                RaycastRecieverRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30.0f);
            }
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].SetActive(showList);
            }
        }
    }
}
