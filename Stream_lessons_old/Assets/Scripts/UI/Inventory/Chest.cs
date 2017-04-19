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
    public int SizeOfIcon = 62;


    void Start()
    {
        FreePlace = MaxNumOfItems;
        foreach (var it in ItemsInChest)
        {
            FreePlace--;
            if (it.IsWeaponOnly) FreePlace--;
        }
    }


    void OnMouseUp()// сделать по правой кнопке
    {
        OtherCanvas.SetActive(false);
        if (ChestPanel.activeSelf)
        {
            ChestPanel.SetActive(false);
            InventoryCanvas.SetActive(false);
            Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f,0f);
            for(int i = 0; i < Content.transform.childCount; i++)
            {
               Destroy(Content.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            ChestPanel.SetActive(true);
            InventoryCanvas.SetActive(true);
            Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, SizeOfIcon * ItemsInChest.Count);

            foreach (var it in ItemsInChest)
            {
                GameObject item = Instantiate(ItemIcon);
                item.transform.SetParent(Content.transform);
                item.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.Sprite);
                item.transform.GetChild(0).transform.GetComponentInChildren<Text>().text = it.Description;
                item.GetComponent<MouseReaction>().enabled = false;
                item.GetComponent<MouseReaction>().ItemHere = it;
            }
        }
    }
}

      
   
