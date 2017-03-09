using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : Item
{
    public bool IsWeapon;

    public InventoryItem()
    {
        FunctionsList.Add("explore");
        FunctionsList.Add("Remove");
    }
    public void Remove(InventoryItem item, GameObject obj, List<InventoryItem> itemList)
    {
        GameObject newObj = Instantiate<GameObject>(Resources.Load<GameObject>(item.Prefab));
        newObj.transform.position = transform.position + transform.forward + transform.up; // как выкидывает
        Destroy(obj);
        itemList.Remove(item);
    }
    
}

   