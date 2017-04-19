using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class TransformPosition : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.Tab;
    private GameObject current;
    private GameObject[] all;
    private bool isLock = false;

    void Start()
    {
        GetAllGUI();
    }

    void GetAllGUI()
    {
        all = new GameObject[transform.childCount];
        for (int j = 0; j < transform.childCount; j++)
        {
            all[j] = transform.GetChild(j).gameObject;
        }
    }

    void AddTriggerEvent()
    {
        for (int j = 0; j < all.Length; j++)
        {
            EventTrigger et = null;

            GameObject obj = all[j];

            if (obj.GetComponent<EventTrigger>())
            {
                et = obj.GetComponent<EventTrigger>();

                var t = new EventTrigger.TriggerEvent();
                t.AddListener(data => {
                    data.Use();
                    current = obj;
                });
                et.triggers.Add(new EventTrigger.Entry { callback = t, eventID = EventTriggerType.PointerDown });

                t = new EventTrigger.TriggerEvent();
                t.AddListener(data => {
                    var ev = (PointerEventData)data;
                    ev.Use();
                    current.transform.position = ev.position;
                });
                et.triggers.Add(new EventTrigger.Entry { callback = t, eventID = EventTriggerType.Drag });
            }
        }
    }

    void ClearTriggerEvent()
    {
        for (int j = 0; j < all.Length; j++)
        {
            EventTrigger et = null;

            GameObject obj = all[j];

            if (obj.GetComponent<EventTrigger>())
            {
                et = obj.GetComponent<EventTrigger>();
                et.triggers.Clear();
            }
        }
    }

    void Lock()
    {
        AddTriggerEvent();
        isLock = true;
        for (int j = 0; j < all.Length; j++)
        {
            if (all[j].GetComponent<Button>()) all[j].GetComponent<Button>().interactable = false;
            if (all[j].GetComponent<InputField>()) all[j].GetComponent<InputField>().interactable = false;
            if (all[j].GetComponent<Slider>()) all[j].GetComponent<Slider>().interactable = false;
            if (all[j].GetComponent<Scrollbar>()) all[j].GetComponent<Scrollbar>().interactable = false;
            if (all[j].GetComponent<Toggle>()) all[j].GetComponent<Toggle>().interactable = false;
            if (all[j].GetComponent<Selectable>()) all[j].GetComponent<Selectable>().interactable = false;
        }
    }

    void UnLock()
    {
        ClearTriggerEvent();
        isLock = false;
        for (int j = 0; j < all.Length; j++)
        {
            if (all[j].GetComponent<Button>()) all[j].GetComponent<Button>().interactable = true;
            if (all[j].GetComponent<InputField>()) all[j].GetComponent<InputField>().interactable = true;
            if (all[j].GetComponent<Slider>()) all[j].GetComponent<Slider>().interactable = true;
            if (all[j].GetComponent<Scrollbar>()) all[j].GetComponent<Scrollbar>().interactable = true;
            if (all[j].GetComponent<Toggle>()) all[j].GetComponent<Toggle>().interactable = true;
            if (all[j].GetComponent<Selectable>()) all[j].GetComponent<Selectable>().interactable = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode) && !isLock) Lock();
        else if (Input.GetKeyDown(keyCode) && isLock) UnLock();
    }
}
