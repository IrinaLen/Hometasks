using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuButtons : MonoBehaviour
{
    public GameObject MenuPlane;

    /// <summary>
    /// 0 - basic menu
    /// 1 - settings
    /// </summary>

    public void OnExitButton()
    {
        //autosave?
        Application.Quit();
    }

    public void SaveGame()
    {
        
    }

    public void ChangeSettings()
    {
        
    }

    public void ReturnToGame()
    {

    }
}
