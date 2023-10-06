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
    public GameObject encounter;
    public GameObject preEncounter;
    public GameObject combat;
    public GameObject combatUI;
    public GameObject rewards;
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
        Utility.instance.TurnOff(encounter);
        Utility.instance.TurnOff(testMenu);
    }

    public void Menu()
    {
        SoundManager.instance.PlayMusic(SoundList.instance.CurrentMenuTheme());
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOff(guild);
        Utility.instance.TurnOff(mainMenu);
        Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOn(menu);
        Utility.instance.TurnOff(events);
        background.sprite = SpriteList.instance.menuBackGround;
        global::Menu.instance.UpdateButtons();
        Utility.instance.TurnOff(encounter);
        Utility.instance.TurnOff(testMenu);
    }
    public void Event()
    {
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOff(guild);
        Utility.instance.TurnOff(mainMenu);
        if (dungeons != null) Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOn(menu);
        Utility.instance.TurnOn(events);
        background.sprite = SpriteList.instance.menuBackGround;
        global::Menu.instance.UpdateButtons();
        Utility.instance.TurnOff(encounter);
        Utility.instance.TurnOff(testMenu);
    }
    public void ViewGuild() 
    {
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOff(menu);
        Utility.instance.TurnOff(mainMenu);
        if (dungeons != null) Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOn(guild);
        Utility.instance.TurnOff(encounter);
        Utility.instance.TurnOff(testMenu);
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
        Utility.instance.TurnOff(encounter);
        Utility.instance.TurnOff(testMenu);
    }
    public void PVELoadStart()
    {
        Utility.instance.TurnOn(background.gameObject);
        Utility.instance.TurnOff(menu);
        Utility.instance.TurnOff(mainMenu);
        if (dungeons != null) Utility.instance.TurnOff(dungeons);
        Utility.instance.TurnOn(guild);
        Utility.instance.TurnOff(encounter);
        Utility.instance.TurnOff(testMenu);
    }

    public void PVEStart()
    {
        Utility.instance.TurnOff(menu);
        Utility.instance.TurnOff(guild);
        Utility.instance.TurnOff(mainMenu);
        Utility.instance.TurnOff(background.gameObject);
        Utility.instance.TurnOn(dungeons.gameObject);
        Utility.instance.TurnOn(encounter);
        Utility.instance.TurnOff(testMenu);
    }
    public void PreEncounter()
    {
        Utility.instance.TurnOn(preEncounter);
        Utility.instance.TurnOff(combat);
        Utility.instance.TurnOff(rewards);
        Utility.instance.TurnOff(combatUI);
    }
    public void Combat()
    {
        Utility.instance.TurnOff(preEncounter);
        Utility.instance.TurnOn(combat);
        Utility.instance.TurnOff(rewards);
        Utility.instance.TurnOn(combatUI);
    }
    public void Rewards()
    {
        Utility.instance.TurnOff(preEncounter);
        Utility.instance.TurnOff(combat);
        Utility.instance.TurnOn(rewards);
        Utility.instance.TurnOff(combatUI);
    }

    public void NightOff()
    {
        TimeManagement.instance.NewDay(1);
    }
}
