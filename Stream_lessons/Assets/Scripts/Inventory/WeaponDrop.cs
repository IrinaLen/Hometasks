using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponDrop : MonoBehaviour, IDropHandler
{
    public Chest ChestHere;

    public void OnDrop(PointerEventData eventData)
    {
        Drag drag = eventData.pointerDrag.GetComponent<Drag>();
        MouseReaction reaction = eventData.pointerDrag.GetComponent<MouseReaction>();
        bool isWeapon = eventData.pointerDrag.GetComponent<MouseReaction>().ItemHere.IsWeaponOnly;
        if (drag != null && isWeapon)
        {
            if (drag.fromChest)
            {
                if (transform.childCount == 0)
                {
                    drag.fromChest = false;
                    drag.transform.SetParent(transform);
                    ChestHere.ItemsInChest.Remove(reaction.ItemHere);

                }
                else
                {
                    reaction.enabled = false;
                    //ERROR 
                }
            }
            else
            {
                if (transform.childCount > 0)
                {
                    transform.GetChild(0).transform.SetParent(drag.Old);
                }
                drag.transform.SetParent(transform);
            }
        }
    }
}
