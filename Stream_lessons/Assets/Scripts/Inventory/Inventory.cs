using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject WeaponSlot;//private or public
    public GameObject InventorySlots;
    public GameObject InventoryCanvas;
    public GameObject Container;
    public GameObject WeaponContainer;
    public GameObject JewelleryContainer;

    private int numberOfItems;
    private List<InventoryItem> itemList;
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
                            if (InventoryCanvas.activeSelf)
                            {
                                GameObject img = Instantiate(WeaponContainer);
                                img.transform.SetParent(WeaponSlot.transform);
                                img.GetComponent<Image>().sprite = Resources.Load<Sprite>(weaponInSlot.Sprite);
                                img.GetComponent<MouseReaction>().ItemHere = weaponInSlot;
                            }
                            Destroy(hit.collider.gameObject);
                           
                        }
                        
                    }
                    else
                    {
                        if (itemList.Count < numberOfItems)
                        {
                            var it = hit.collider.GetComponent<InventoryItem>();
                            itemList.Add(it);
                            for (int i = 0; i < InventorySlots.transform.childCount; i++)
                            {
                                if (InventorySlots.transform.GetChild(i).transform.childCount == 0)
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
            if (InventoryCanvas.activeSelf)
            {
                InventoryCanvas.SetActive(false);

                if (WeaponSlot.transform.childCount > 0)
                {
                    Destroy(WeaponSlot.transform.GetChild(0).gameObject);
                }

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
                InventoryCanvas.SetActive(true);

                if (isWeaponHere)
                {
                    GameObject img = Instantiate(WeaponContainer);
                    img.transform.SetParent(WeaponSlot.transform);
                    img.GetComponent<Image>().sprite = Resources.Load<Sprite>(weaponInSlot.Sprite);
                    img.GetComponent<MouseReaction>().ItemHere = weaponInSlot;
                }
                int count = itemList.Count;
                for (int i = 0; i < count; i++)
                {
                    InventoryItem it = itemList[i];
                    if (InventorySlots.transform.childCount >= i) // потом не понадобится
                    {
                        GameObject img = Instantiate(Container);
                        img.transform.SetParent(InventorySlots.transform.GetChild(i).transform);
                        img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.Sprite);
                        img.GetComponent<MouseReaction>().ItemHere = it;
                    }
                    else
                    {
                        break;
                    }
                }
               
            }
        }
    }



    private void PictureUpdate()
    {
        
        int count = itemList.Count;
        for (int i = 0; i < count; i++)
        {
            InventoryItem it = itemList[i];
            if (InventorySlots.transform.childCount >= i) // потом не понадобится
            {
                GameObject img = Instantiate(Container);
                img.transform.SetParent(InventorySlots.transform.GetChild(i).transform);
                img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.Sprite);
                img.GetComponent<MouseReaction>().ItemHere = it;
            }
            else
            {
                break;
            }
        }
    }
}



