using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject WeaponSlot;//private or public
    public GameObject InventorySlots;
    public GameObject PesonPanel;
    public GameObject Container;
    public GameObject WeaponContainer;
    public GameObject JewelleryContainer;

    private int numberOfItems;
    private List<InventoryItem> itemList;
    private Weapon weaponInSlot;


    void Start ()
	{
	    itemList = new List<InventoryItem>();
	    numberOfItems = InventorySlots.transform.childCount;
	}
	
	void Update ()
    {
        if (Input.GetMouseButtonUp(0)) // пока может взять больше, чем надо инвентаря
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Item item = hit.collider.GetComponent<Item>();
                if (item != null)
                {
                    if (item.IsWeapolOnly)
                    {
                        weaponInSlot = hit.collider.GetComponent<Weapon>();
                        Destroy(hit.collider.gameObject);
                    }
                    else
                    {
                        if (itemList.Count < numberOfItems)
                        {
                            itemList.Add(hit.collider.GetComponent<InventoryItem>());
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            if (InventorySlots.activeSelf)
            {
                PesonPanel.SetActive(false);
                // зачем это?
                if (WeaponSlot.transform.childCount > 0)
                {
                    Destroy(WeaponSlot.transform.GetChild(0).gameObject);
                }
                InventorySlots.SetActive(false);
                for (int i = 0; i < InventorySlots.transform.childCount; i++)
                {
                    if (InventorySlots.transform.GetChild(i).transform.childCount > 0)
                    {
                        Destroy(InventorySlots.transform.GetChild(i).transform.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                PesonPanel.SetActive(true);
                if (WeaponSlot.transform.childCount == 0 && weaponInSlot != null)
                {
                    GameObject img = Instantiate(WeaponContainer);
                    img.transform.SetParent(WeaponSlot.transform);
                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>(weaponInSlot.Sprite);
                    img.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = weaponInSlot.Description;

                }

                InventorySlots.SetActive(true);
                int count = itemList.Count;
                for (int i = 0; i < count; i++)
                {
                    InventoryItem it = itemList[i];
                    if (InventorySlots.transform.childCount >= i)
                    {
                        GameObject img = Instantiate(Container);
                        img.transform.SetParent(InventorySlots.transform.GetChild(i).transform);
                        img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.Sprite);
                        img.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = itemList[i].Description;
                    }
                    else
                    {
                        break;
                    }
                }
               
            }
        }
    }
}
