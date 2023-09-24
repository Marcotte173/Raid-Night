using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : Event
{    public override void Go()
    {
        base.Go();
        EventManager.instance.eventText[0].text = "itch.io Game Jam";
        EventManager.instance.eventText[2].text = "This will eventually be 'Raid Night'";
        EventManager.instance.eventText[4].text = "It is being developed as a tycoon game, where the \nmain game play loop is a series of Autochessed boss battles ";
        EventManager.instance.eventText[6].text = "For the moment, there are two bosses, a little bit of loot, and very little out of raid gameplay";
        EventManager.instance.eventText[8].text = "Check back every once in a while, it gets updated a LOT";
        EventManager.instance.OneButton("Good To Know");
    }
    public override void ButtonTwo()
    {
        EventManager.instance.triggeredEvents.RemoveAt(0);
        base.ButtonTwo();
    }
}
