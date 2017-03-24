using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestDrop : MonoBehaviour, IDropHandler
{
    public Chest ChestHere;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<Scrollbar>())
        {
            return;
        }

        Drag drag = eventData.pointerDrag.GetComponent<Drag>();
        MouseReaction reaction = eventData.pointerDrag.GetComponent<MouseReaction>();
        reaction.enabled = false;
        ushort dragSize = (ushort)(reaction.ItemHere.IsWeaponOnly ? 2 : 1);

        if (drag != null)
        {
            if (ChestHere.FreePlace >= dragSize)
            {
                GameObject item = Instantiate(ChestHere.ItemIcon);
                item.transform.SetParent(ChestHere.Content.transform);
                item.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                item.transform.GetChild(0).transform.GetComponentInChildren<Text>().text = reaction.ItemHere.Description;
                item.GetComponent<MouseReaction>().enabled = false;
                item.GetComponent<MouseReaction>().ItemHere = reaction.ItemHere;
                if (!drag.fromChest)
                {
                    ChestHere.ItemsInChest.Add(reaction.ItemHere);
                }
                ChestHere.FreePlace -= dragSize;
                Destroy(eventData.pointerDrag);
                ChestHere.Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, ChestHere.SizeOfIcon * ChestHere.ItemsInChest.Count);

            }
            else
            {
                //sound for error
            }
        }
    }
}
