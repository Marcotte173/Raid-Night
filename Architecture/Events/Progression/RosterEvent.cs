using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosterEvent : Event
{
    public RaidRoster roster;
    private void Awake()
    {
        roster = Instantiate(GameObjectList.instance.raidRoster, transform);
        roster.name = "Scheduling Roster";
    }
    public override void Go()
    {
        base.Go();
        EventManager.instance.eventText[0].text = roster.dungeon.dungeonName;
        EventManager.instance.eventText[7].text = "\t\tGather your party and venture forth";
        EventManager.instance.OneButton("Ok");
    }
    public override void ButtonTwo()
    {
        EventManager.instance.triggeredEvents.RemoveAt(0);
        base.ButtonTwo();
        Menu.instance.Dungeon();        
        DungeonManager.instance.Raid(roster.dungeon,roster.roster);
        roster.ready = false;
    }  
}
