using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestDrop : MonoBehaviour, IDropHandler
{
    public Chest ChestHere;
    //public Inventory InventoryHere;

    public void OnDrop(PointerEventData eventData)
    {
        Drag drag = eventData.pointerDrag.GetComponent<Drag>();
        ushort dragSize = (ushort)(eventData.pointerDrag.GetComponent<MouseReaction>().ItemHere.IsWeaponOnly ? 2 : 1);
        if (drag != null)
        {
            if (ChestHere.FreePlace >= dragSize)
            {
                GameObject item = Instantiate(ChestHere.ItemIcon);
                item.transform.SetParent(ChestHere.Content.transform);
                item.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                item.transform.GetChild(0).transform.GetComponentInChildren<Text>().text = eventData.pointerDrag.GetComponent<MouseReaction>().ItemHere.Description;
                item.GetComponent<MouseReaction>().enabled = false;
                item.GetComponent<MouseReaction>().ItemHere = eventData.pointerDrag.GetComponent<MouseReaction>().ItemHere;
                ChestHere.ItemsInChest.Add(eventData.pointerDrag.GetComponent<MouseReaction>().ItemHere);
                ChestHere.FreePlace -= dragSize;
                Destroy(eventData.pointerDrag);

            }
            else
            {
                //sound for error
            }
        }
    }
}
