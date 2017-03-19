using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject WeaponSlot;
    public GameObject InventorySlots;
    public GameObject InventoryCanvas;
    public GameObject Container;
    public GameObject WeaponContainer;
    public GameObject JewelleryContainer;

    private int numberOfItems;
    private List<InventoryItem> itemList;//нужен ли...
    private Weapon weaponInSlot;
    private bool isWeaponHere;


    void Start ()
	{
	    itemList = new List<InventoryItem>();
	    numberOfItems = InventorySlots.transform.childCount;
	    isWeaponHere = false;
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
                    if (item.IsWeaponOnly)
                    {
                        if (weaponInSlot != null)
                        {
                            //diolog window...
                        }
                        else
                        {
                            weaponInSlot = hit.collider.GetComponent<Weapon>();
                            isWeaponHere = true;
                            GameObject img = Instantiate(WeaponContainer);
                            img.transform.SetParent(WeaponSlot.transform);
                            img.GetComponent<Image>().sprite = Resources.Load<Sprite>(weaponInSlot.Sprite);
                            img.GetComponent<MouseReaction>().ItemHere = weaponInSlot;
                            Destroy(hit.collider.gameObject);
                        }
                        
                    }
                    else
                    {
                        if (itemList.Count < numberOfItems)
                        {
                            InventoryItem it = hit.collider.GetComponent<InventoryItem>();
                            itemList.Add(it);
                            //добавить в первую своюодную
                            for (int i = 0; i < InventorySlots.transform.childCount; i++)
                            {
                                if (InventorySlots.transform.GetChild(i).childCount > 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    GameObject img = Instantiate(Container);
                                    img.transform.SetParent(InventorySlots.transform.GetChild(i).transform);
                                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.Sprite);
                                    img.GetComponent<MouseReaction>().ItemHere = it;
                                    break;
                                }
                            }
                            Destroy(hit.collider.gameObject);
                        }
                        else
                        {
                            //error sound
                        }
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            InventoryCanvas.SetActive(!InventoryCanvas.activeSelf);
           

        }
    }
}
