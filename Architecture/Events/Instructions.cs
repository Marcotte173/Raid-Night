using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : Event
{
    int page;
    public int totalPages;
    private void Awake()
    {
        page = 1;
    }
    public override void Go()
    {
        base.Go();
        Page();
    }

    public void Page()
    {
        if(page == 1)
        {
            EventManager.instance.eventText[0].text = "Recruiting";
            EventManager.instance.eventText[2].text = "The key to any good guild is attracting the right people";
            EventManager.instance.eventText[4].text = "As your guild gains more renown, you will attract more skilled players";
            EventManager.instance.eventText[6].text = "Take a look at the recruitment page to see available players";
            EventManager.instance.OneButton("Got It");
        }
        else if (page == 2)
        {
            EventManager.instance.eventText[0].text = "Guild";
            EventManager.instance.eventText[2].text = "The guild page gives you information on your guild";
            EventManager.instance.eventText[4].text = "From here, you can check in on guild members";
            EventManager.instance.eventText[6].text = "You can peruse guild information such as renown";
            EventManager.instance.eventText[8].text = "You can even check how your guild is doing compared to your rivals";
            EventManager.instance.TwoButton("Back","Got It");
        }
        else if (page == 3)
        {
            EventManager.instance.eventText[0].text = "Raid";
            EventManager.instance.eventText[2].text  = "In the raid menu, you will select a raid for players to try";
            EventManager.instance.eventText[4].text  = "It will give a description of the raid as well as recommended gear score";
            EventManager.instance.eventText[6].text  = "It will also give you an estimated time frame";
            EventManager.instance.eventText[8].text = "Some players will have tight schedules that need to be taken into acocunt";
            EventManager.instance.TwoButton("Back", "Got It");
        }
        else if (page == 4)
        {
            EventManager.instance.eventText[0].text = "Raid Contd.";
            EventManager.instance.eventText[2].text  = "In the raid, the game becomes an auto chess";
            EventManager.instance.eventText[4].text  = "You will place your characters, and begin the match";
            EventManager.instance.eventText[6].text  = "The characters will fight the boss on their own, you will have limited control";
            EventManager.instance.eventText[8].text = "Placement, gear, and pre fight preparation will have a large effect on the outcome";
            EventManager.instance.TwoButton("Back", "Got It");
        }
        else if (page == 5)
        {
            EventManager.instance.eventText[0].text = "Time";
            EventManager.instance.eventText[2].text = "Time is an important factor in the game";
            EventManager.instance.eventText[4].text = "Your raiders have schedules you will need to manage";
            EventManager.instance.eventText[6].text = "Raids have lockouts. Your progression is saved until Monday, at which point they reset";
            EventManager.instance.eventText[8].text = "Effective time management is the key to success";
            EventManager.instance.TwoButton("Back", "I'm ready to play");
        }

    }
    public override void ButtonOne()
    {
        page--;
        Page();
    }    
    public override void ButtonTwo()
    {
        page++;
        Page();
    }
    public override void ButtonThree()
    {
        if (page == totalPages)
        {
            EventManager.instance.triggeredEvents.RemoveAt(0);
            base.ButtonTwo();
            page = 1;
        }
        else
        {
            page++;
            Page();
        }
    }
}
