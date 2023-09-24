using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mainMenu;
    public GameObject menu;
    public GameObject dungeons;
    public GameObject testMenu;
    public GameObject guild;
    public GameObject events;
    public Image background;
    private void Start()
    {
        instance = this;
        MainMenu();
    }

    public void MainMenu()
    {
        Utility.instance.TurnOff(background.gameObject);
        Utility.instance.TurnOff(guild);
        Utility.instance.TurnOn(mainMenu);
        Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOff(menu);
        Utility.instance.TurnOff(events);
    }

    public void Menu()
    {
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOff(guild);
        Utility.instance.TurnOff(mainMenu);
        Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOn(menu);
        Utility.instance.TurnOff(events);
        background.sprite = SpriteList.instance.menuBackGround;
        global::Menu.instance.UpdateButtons();
    }
    public void Event()
    {
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOff(guild);
        Utility.instance.TurnOff(mainMenu);
        if (dungeons != null) Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOn(menu);
        Utility.instance.TurnOn(events);
        Debug.Log("On");
        background.sprite = SpriteList.instance.menuBackGround;
        global::Menu.instance.UpdateButtons();
    }
    public void ViewGuild() 
    {
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOff(menu);
        Utility.instance.TurnOff(mainMenu);
        if (dungeons != null) Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOn(guild);        
    }
    public void PVEMenu()
    {
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOn(menu);
        Utility.instance.TurnOff(mainMenu);
        if (dungeons != null) Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOff(guild);
        background.sprite = SpriteList.instance.menuBackGround;
        global::Menu.instance.UpdateButtons();
    }
    public void PVELoadStart()
    {
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOff(menu);
        Utility.instance.TurnOff(mainMenu);
        if (dungeons != null) Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOn(guild);
    }

    public void PVEStart()
    {
        Utility.instance.TurnOff(menu);
        Utility.instance.TurnOff(guild);
        Utility.instance.TurnOff(mainMenu);
        Utility.instance.TurnOff(background.gameObject);
        Utility.instance.TurnOn(dungeons.gameObject);
    }

    public void NightOff()
    {
        TimeManagement.instance.NewDay(1);
    }
}
