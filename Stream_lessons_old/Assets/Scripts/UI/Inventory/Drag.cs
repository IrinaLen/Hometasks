
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public Transform Canvas;
    public Transform Old;

    public bool fromChest = false;

    void Start()
    {
        Canvas = GameObject.Find("InventoryCanvas").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Old = transform.parent;
        transform.SetParent(Canvas);
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == Canvas)
        {
            if (fromChest)
            {
                transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            transform.SetParent(Old);
        }
    }
}
