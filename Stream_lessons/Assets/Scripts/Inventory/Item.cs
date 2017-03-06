using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item: MonoBehaviour
{
    public string Name;
    public uint ID;
    public string Description;
    public string Sprite;
    public string Prefab;
    public bool IsWeapolOnly;

    public void Explore(GameObject obj)
    {
       //sound
    }



    void OnMouseEnter()
    {

        
    
    }

    void OnMouseExit()
    {
        
    }
}
