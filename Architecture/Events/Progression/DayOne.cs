using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayOne : Event
{
    public override void Go()
    {
        base.Go();
        EventManager.instance.eventText[0].text = "Raid Night";
        EventManager.instance.eventText[2].text = "Welcome to Raid Night, a game where you lead a guild to Glory!";
        EventManager.instance.eventText[4].text = "Recruit a band of heroes, manage egos and loot drops, and emerge as the top guild on the server";
        EventManager.instance.eventText[6].text = "Would you like some instructions, or are you ready to just jump in?";

        EventManager.instance.TwoButton("Instructions","Just let me play");
    }
    public override void ButtonOne()
    {
        EventManager.instance.triggeredEvents.RemoveAt(0);
        base.ButtonTwo();
    }

    public override void ButtonThree()
    {
        foreach (Event e in EventList.instance.events) if (e.id == 1)
        {
            e.eventStatus = EventStatus.Over;
            if (EventManager.instance.triggeredEvents.Contains(e)) EventManager.instance.triggeredEvents.Remove(e);
        }
        EventManager.instance.triggeredEvents.RemoveAt(0);
        base.ButtonTwo();
    }
}
