using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<InventoryItem> itemList;
    private GameObject weapon;//private or public
    public GameObject Invent;
    public GameObject PesonPanel;
    public GameObject Container;
    private int numberOfItems;


	void Start ()
	{
	    itemList = new List<InventoryItem>();
	    numberOfItems = Invent.transform.childCount;
	}
	
	void Update ()
    {
        if (Input.GetMouseButtonUp(0)) // Take()
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
                        
                    }
                    else
                    {
                        itemList.Add(hit.collider.GetComponent<InventoryItem>());
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            if (Invent.activeSelf)
            {
                PesonPanel.SetActive(false);
                Invent.SetActive(false);
                for (int i = 0; i < Invent.transform.childCount; i++)
                {
                    if (Invent.transform.GetChild(i).transform.childCount > 0)
                    {
                        Destroy(Invent.transform.GetChild(i).transform.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                PesonPanel.SetActive(true);
                Invent.SetActive(true);
                int count = itemList.Count;
                for (int i = 0; i < count; i++)
                {
                    InventoryItem it = itemList[i];
                    if (Invent.transform.childCount >= i)
                    {
                        GameObject img = Instantiate(Container);
                        img.transform.SetParent(Invent.transform.GetChild(i).transform);
                        img.GetComponent<Image>().sprite = Resources.Load<Sprite>(it.Sprite);
                        img.AddComponent<Button>().onClick.AddListener(()=>Remove(it, img));
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    void Remove(InventoryItem item, GameObject obj)
    {
        GameObject newObj = Instantiate<GameObject>(Resources.Load<GameObject>(item.Prefab));
        newObj.transform.position = transform.position + transform.forward + transform.up; // как выкидывает
        Destroy(obj);
        itemList.Remove(item);
    }


}
