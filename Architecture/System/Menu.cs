using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    public Text pveTitle;
    public List<Button> buttons;
    public Button helpButton;
    public bool events;
   
    private void Awake()
    {
        instance = this;
    }
    public void UpdateButtons()
    {
        Utility.instance.TurnOn(helpButton.gameObject);
        foreach (Button b in buttons) Utility.instance.TurnOn(b.gameObject);
        buttons[0].GetComponentInChildren<Text>().text = "Manage Guild";
        buttons[1].GetComponentInChildren<Text>().text = "Guild Upgrades";
        buttons[2].GetComponentInChildren<Text>().text = "Assign Tasks";
        buttons[3].GetComponentInChildren<Text>().text = "Schedule Raid";
        buttons[4].GetComponentInChildren<Text>().text = "Time";
        buttons[5].GetComponentInChildren<Text>().text = "Test";
    }

    public void BackToMenu()
    {
        Utility.instance.TurnOff(helpButton.gameObject);
        UIManager.instance.background.sprite = SpriteList.instance.menuBackGround;
        UpdateButtons();
    }

    public void GuildActions()
    {
        if (!events)
        {
            Guild.instance.manageState = Guild.instance.roster.Count > 0 ? ManageState.View : ManageState.Recruit;
            GuildManagement();
        }
        UpdateButtons();
    }

    public void GuildUpgrades()
    {
        if (!events)
        {
            Guild.instance.manageState =  ManageState.Upgrades;
            GuildManagement();
        }
        UpdateButtons();
    }    

    public void Assign()
    {
        if (!events)
        {
            Guild.instance.manageState = ManageState.Assign;
            GuildManagement();
        }
        UpdateButtons();
    }
    public void Raid()
    {
        if (!events)
        {
            Guild.instance.manageState = ManageState.Schedule;
            Utility.instance.TurnOn(UIManager.instance.guild);
            Guild.instance.ScheduleRaid();
        }
        UpdateButtons();
    }
    public void Time()
    {
        if (!events)
        {
            TimeManagement.instance.NewDay(1);
        }
        UpdateButtons();
    }
    public void Test()
    {
        if (!events) global::Test.instance.Begin();
        UpdateButtons();
    }
    public void Help()
    {
        if (!events)
        {
            Event();
            events = true;
            foreach (Event e in EventList.instance.events) if (e.id == 2) EventManager.instance.triggeredEvents.Add(e);
            EventManager.instance.Load();
        }
    }

    public void Event()
    {
        
        Utility.instance.TurnOn(UIManager.instance.background.gameObject);
        Utility.instance.TurnOff(UIManager.instance.guild);
        Utility.instance.TurnOff(UIManager.instance.dungeons);
        Utility.instance.TurnOff(UIManager.instance.mainMenu);
        Utility.instance.TurnOn(UIManager.instance.menu);
        Utility.instance.TurnOn(UIManager.instance.events);
        UIManager.instance.background.sprite = SpriteList.instance.menuBackGround;
        instance.UpdateButtons();
    }

    public void GuildManagement()
    {
        Utility.instance.TurnOn(UIManager.instance.background.gameObject);
        Utility.instance.TurnOff(UIManager.instance.dungeons);
        Utility.instance.TurnOn(UIManager.instance.guild);
        Guild.instance.Manage();
    }
    public void Dungeon()
    {
        Utility.instance.TurnOff(UIManager.instance.background.gameObject);
        Utility.instance.TurnOff(UIManager.instance.guild);
        Utility.instance.TurnOn(UIManager.instance.dungeons);
        Utility.instance.TurnOff(UIManager.instance.menu);
        Utility.instance.TurnOff(UIManager.instance.events);
    }
}