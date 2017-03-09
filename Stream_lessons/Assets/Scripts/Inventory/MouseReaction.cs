using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseReaction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject DescriptionWindow;
    public Item ItemHere;
    private GameObject descriptWind;


    public void OnPointerEnter(PointerEventData eventData)
    {
            // задержку корутиной
            descriptWind = Instantiate(DescriptionWindow);
            descriptWind.transform.SetParent(transform.GetComponentInParent<Drag>().Canvas);
            descriptWind.transform.position = transform.position + new Vector3(90f, 0, 0);
            descriptWind.transform.GetChild(0).transform.GetComponent<Text>().text = ItemHere.Description;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        Destroy(descriptWind);

    }
}
