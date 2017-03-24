using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name;
    public uint ID;
    public string Description;
    public string Sprite;
    public string Prefab;
    public bool IsWeaponOnly;
    public List<string> FunctionsList = new List<string>(); //  не стринг а спец класс Action()
    public Vector3 MoveDescriptWindow = new Vector3 (90f, 0, 0);


    public void GetName()
    {
        //
    }

    public void Explore(GameObject obj)
    {
       //sound
    }

    
}
