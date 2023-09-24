using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeek : Event
{
    public override void Go()
    {
        base.Go();
        EventManager.instance.eventText[0].text = "New Week";
        EventManager.instance.eventText[7].text = "\t\tAll Dungeon Progress has been reset";
        EventManager.instance.OneButton("Ok");
        foreach (Dungeon d in DungeonManager.instance.pve)
        {
            d.encountersCompleted = 0;
        }
    }
    public override void Expire()
    {
        eventStatus = EventStatus.Unavailable;
        available[1]++;
        expire[1]++;
    }

    public override void ButtonTwo()
    {
        EventManager.instance.triggeredEvents.RemoveAt(0);
        base.ButtonTwo();
    }
}
