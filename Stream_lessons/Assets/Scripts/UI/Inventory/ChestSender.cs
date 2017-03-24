using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSender : MonoBehaviour
{
    public GameObject ChestPanel;
    public GameObject InventryPanel;
    public GameObject WeaponPanel;

    void OnMouseUp()
    {
        ChestPanel.GetComponent<ChestDrop>().ChestHere = GetComponent<Chest>();
        for (int i = 0; i < InventryPanel.transform.childCount; i++)
        {
            InventryPanel.transform.GetChild(i).GetComponent<InventoryDrop>().ChestHere = GetComponent<Chest>();
        }
        WeaponPanel.GetComponent<WeaponDrop>().ChestHere = GetComponent<Chest>();
    }
}
