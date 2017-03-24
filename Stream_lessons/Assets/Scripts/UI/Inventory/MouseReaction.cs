using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseReaction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject DescriptionWindow;
    public Item ItemHere;
    public GameObject ActionButton;

    private GameObject descriptWind;
    private Transform canvas;

    void Start()
    {
        canvas = GameObject.Find("InventoryCanvas").transform;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
            // задержку корутиной
            descriptWind = Instantiate(DescriptionWindow);
            descriptWind.transform.SetParent(canvas);
            descriptWind.transform.position = transform.position + ItemHere.MoveDescriptWindow;
            descriptWind.transform.GetChild(0).transform.GetComponent<Text>().text = ItemHere.Description;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(descriptWind);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 position = Input.mousePosition;
        List<GameObject> buttons  = new List<GameObject>();
        GameObject obj;
        foreach (var act in ItemHere.FunctionsList)
        {
            obj = Instantiate(ActionButton);
            obj.transform.SetParent(canvas);
            obj.transform.position = position + new Vector3(0, ActionButton.GetComponent<RectTransform>().rect.height, 0);
            obj.transform.GetChild(0).transform.GetComponent<Text>().text = act;
            buttons.Add(obj);
        }
    }
}
