using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public GameObject ChestPanel;
    public GameObject ItemIcon;
    public GameObject Content;
    public GameObject InventoryCanvas;
    public GameObject OtherCanvas;
    public List<Item> ItemsInChest; //считывать из какого-то файла
    public ushort MaxNumOfItems;

    public ushort FreePlace;
    private Scrollbar scroll;

    void Start()
    {
        FreePlace = MaxNumOfItems;
        scroll = ChestPanel.transform.GetChild(0).GetComponentInChildren<Scrollbar>();
        foreach (var it in ItemsInChest)
        {
            FreePlace--;
            if (it.IsWeaponOnly) FreePlace--;
        }
    }


    void OnMouseUp()
    {
        OtherCanvas.SetActive(false);
        if (ChestPanel.activeSelf)
        {
            ChestPanel.SetActive(false);
            InventoryCanvas.SetActive(false);
            for(int i = 0; i < Content.transform.childCount; i++)
            {
               Debug.Log(i);
               Destroy(Content.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            ChestPanel.SetActive(true);
            InventoryCanvas.SetActive(true);
            foreach (var it in ItemsInChest)
            {
                GameObject item = Instantiate(ItemIcon);
                item.transform.SetParent(Content.transform);
                item.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.Sprite);
                item.transform.GetChild(0).transform.GetComponentInChildren<Text>().text = it.Description;
                item.GetComponent<MouseReaction>().enabled = false;
            }
        }
    }
}

      
   
