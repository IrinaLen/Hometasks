using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReaction : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject OtherPanels;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (InventoryPanel.activeSelf)
            {
                if (InventoryPanel.transform.GetChild(2).gameObject.activeSelf)
                {
                    Transform content = InventoryPanel.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0);
                    InventoryPanel.transform.GetChild(2).gameObject.SetActive(false);
                    for (int i = 0; i < content.childCount; i++)
                    {
                        Destroy(content.GetChild(i).gameObject);
                    }
                }
                else
                {
                    InventoryPanel.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < OtherPanels.transform.childCount; i++)
                {
                    if (OtherPanels.transform.GetChild(i).gameObject.activeSelf)
                    {
                        OtherPanels.transform.GetChild(i).gameObject.SetActive(false);
                        return;
                    }
                }
                OtherPanels.SetActive(true);
                OtherPanels.transform.GetChild(5).gameObject.SetActive(true);
            }
        }
    }
    /// <summary>
    ///0 -  лор
    ///1 - карта
    ///2 - состояние
    ///3 - крафт
    ///--инвентврь
    ///4 - дневние
    ///5 - меню
    /// </summary>
   
    private void OpenPlane(GameObject obj)
    {
        OtherPanels.SetActive(true);
        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }
        else
        {
            InventoryPanel.SetActive(false);
            for (int i = 0; i < OtherPanels.transform.childCount; i++)
            {
                OtherPanels.transform.GetChild(i).gameObject.SetActive(false);
            }
            obj.SetActive(true);
        }
        
    }

    public void OpenLor()
    {
        GameObject child = OtherPanels.transform.GetChild(0).gameObject;
        OpenPlane(child);
       
    }

    public void OpenMap()
    {
        GameObject child = OtherPanels.transform.GetChild(1).gameObject;
        OpenPlane(child);
    }

    public void OpenStatus()
    {
        GameObject child = OtherPanels.transform.GetChild(2).gameObject;
        OpenPlane(child);
    }

    public void OpenCraft()
    {
        GameObject child = OtherPanels.transform.GetChild(3).gameObject;
        OpenPlane(child);
    }

    public void OpenDiary()
    {
        GameObject child = OtherPanels.transform.GetChild(4).gameObject;
        OpenPlane(child);
    }

    public void OpenMenu()
    {
        GameObject child = OtherPanels.transform.GetChild(5).gameObject;
        OpenPlane(child);
    }

    public void OpenInventory()
    {
        OpenPlane(InventoryPanel);
        InventoryPanel.transform.GetChild(2).gameObject.SetActive(false);
        Transform content = InventoryPanel.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0);
        for (int i = 0; i < content.childCount; i++)
        {
            Debug.Log(i);
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
