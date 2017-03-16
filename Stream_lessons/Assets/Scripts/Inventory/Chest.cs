using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public GameObject ChestPanel;
    public GameObject ItemIcon;
    public GameObject Content;


    public List<Item> ItemsInChest; //считывать из какого-то файла

    //глобальная
    public ushort MaxNumOfItems;

    private ushort itemsHere = 0;
    public Scrollbar scroll;
    private GameObject inventory;


    void Start()
    {
        inventory = GameObject.Find("InventoryCanvas");
        scroll = ChestPanel.transform.GetChild(0).GetComponentInChildren<Scrollbar>();
        foreach (var it in ItemsInChest)
        {
            itemsHere++;
            if (it.IsWeaponOnly) itemsHere++;
        }
    }


    void OnMouseUp()
    {
        if (ChestPanel.activeSelf)
        {
            ChestPanel.SetActive(false);
            //удаление объектов или Пул объектов
            //for (int i = 0; i < Content.transform.childCount; i++)
            //{
            //    Destroy(Content.transform.GetChild(i));
            //    i--;
            //}
        }
        else
        {
            ChestPanel.SetActive(true);
            if (!inventory.activeSelf)
            {
                inventory.SetActive(true);
            }
            
            foreach (var it in ItemsInChest)
            {
                GameObject item = Instantiate(ItemIcon);
                item.transform.SetParent(Content.transform);
                item.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.Sprite);
                item.transform.GetChild(0).transform.GetComponentInChildren<Text>().text = it.Description;
            }

        }
    }
}

      
   
