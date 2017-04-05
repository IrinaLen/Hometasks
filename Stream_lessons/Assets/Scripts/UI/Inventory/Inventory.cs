using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject WeaponSlot;
    public GameObject InventorySlots; // может стоит переписать?
    public GameObject InventoryCanvas;
    public GameObject Container;
    public GameObject WeaponContainer;
    public GameObject JewelleryContainer;
    public GameObject ActionButton;

    private GameObject menu;
    private int numberOfItems;
    private List<InventoryItem> itemList; //нужен ли...
    private Weapon weaponInSlot;
    private bool isWeaponHere;
    private float maxDistance = 4f; // const
    private List<GameObject> buttons = new List<GameObject>();
    private List<string> functionsList = new List<string>();// возможно придется удалить

    void Start()
    {
        menu = GameObject.Find("MenuCanvas");
        itemList = new List<InventoryItem>();
        numberOfItems = InventorySlots.transform.childCount;
        isWeaponHere = false;
        functionsList.Add("Take"); // относится к списку
        functionsList.Add("Say");
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1)) // пока может взять больше, чем надо инвентаря
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                Item item = hit.collider.GetComponent<Item>();
                if (item != null)
                {
                    //отрисока кнопок при щелчке по объекту... почему не работает?

                    DrawWindows(item);
                   // AddToInventory(hit,item);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            InventoryCanvas.SetActive(!InventoryCanvas.activeSelf);
        }
    }

    private void DrawWindows(Item item)
    {
        foreach (GameObject b in buttons)
        {
            Destroy(b);
        }
        buttons.Clear();

        Vector3 position = Input.mousePosition + new Vector3(ActionButton.GetComponent<RectTransform>().rect.width / 2, 0);
        GameObject obj;
        foreach (var act in functionsList)
        {
            obj = Instantiate(ActionButton);
            obj.transform.SetParent(menu.transform);
            position -= new Vector3(0, ActionButton.GetComponent<RectTransform>().rect.height, 0);
            obj.transform.position = position;
            obj.transform.GetChild(0).transform.GetComponent<Text>().text = act;
            buttons.Add(obj);
        }
    }

    private void AddToInventory(RaycastHit hit, Item item)
    {
        foreach (GameObject b in buttons)
        {
            Destroy(b);
        }
        buttons.Clear();

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
                itemList.Add(it); //изменить на id
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

