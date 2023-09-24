using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public List<Event> triggeredEvents;
    public List<TMP_Text> eventText;
    public List<Button> eventButton;
    private void Awake()
    {
        instance = this;
    }
    public void Events()
    {
        Menu.instance.Event();
        Menu.instance.events = true;
        Load();
    }
    public void Load()
    {        
        if (triggeredEvents.Count > 0)
        {
            triggeredEvents[0].Go();            
        }
        else
        {
            Menu.instance.events = false;
            UIManager.instance.Menu();
        }
    }
    public void OneButton(string x)
    {
        Utility.instance.TurnOff(eventButton[0].gameObject);
        Utility.instance.TurnOn(eventButton[1].gameObject);
        Utility.instance.TurnOff(eventButton[2].gameObject);
        eventButton[1].GetComponentInChildren<TMP_Text>().text = x;
    }
    public void TwoButton(string x, string y)
    {
        Utility.instance.TurnOn(eventButton[0].gameObject);
        Utility.instance.TurnOn(eventButton[2].gameObject);
        eventButton[0].GetComponentInChildren<TMP_Text>().text = x;
        Utility.instance.TurnOff(eventButton[1].gameObject);
        eventButton[2].GetComponentInChildren<TMP_Text>().text = y;
    }
    public void ThreeButton(string x, string y, string z)
    {
        Utility.instance.TurnOn(eventButton[0].gameObject);
        Utility.instance.TurnOn(eventButton[1].gameObject);
        Utility.instance.TurnOn(eventButton[2].gameObject);      
        eventButton[0].GetComponentInChildren<TMP_Text>().text = x;
        eventButton[1].GetComponentInChildren<TMP_Text>().text = y;
        eventButton[2].GetComponentInChildren<TMP_Text>().text = z;
    }
    public void ButtonOne()
    {
        triggeredEvents[0].ButtonOne();
    }

    public void ButtonTwo()
    {
        triggeredEvents[0].ButtonTwo();
    }
    public void ButtonThree()
    {
        triggeredEvents[0].ButtonThree();
    }

    public void ButtonsOff()
    {
        foreach (Button b in eventButton) Utility.instance.TurnOff(b.gameObject);
    }
}
