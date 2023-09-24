using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinOfAkalosPreEncounter : PreEncounter
{
    public override void Begin()
    {
        circumstance = 0;
        EncounterUI.instance.flavor[0].text = "Paladin of Akalos";
        EncounterUI.instance.flavor[1].text = "";
        EncounterUI.instance.flavor[2].text = "After finishing the Paladin of Ceres, you trudge forward";
        EncounterUI.instance.flavor[3].text = "";
        EncounterUI.instance.flavor[4].text = $"{DungeonManager.instance.currentDungeon.currentEncounter.player[Random.Range(0, DungeonManager.instance.currentDungeon.currentEncounter.player.Count)].characterName} mentions that he heard about a secret in this hallway.";
        EncounterUI.instance.flavor[5].text = "Do you want to take some time and check the hallway? ";
        EncounterUI.instance.flavor[6].text = "";
        EncounterUI.instance.flavor[7].text = "You know that you don’t have all night, some members have work in the morning, do you want to take the time to look around?";
        EncounterUI.instance.flavor[8].text = "";
        EncounterUI.instance.flavor[9].text = "";
        TwoButtons("Check the hallway", "Move On");
    }
    public override void ButtonOne()
    {
        int roll = Random.Range(1, 101);
        EncounterUI.instance.flavor[0].text = (roll < 21) ? "You found it!" : "It must be around here somewhere!";
        EncounterUI.instance.flavor[1].text = "";
        EncounterUI.instance.flavor[2].text = "";
        EncounterUI.instance.flavor[3].text = "";
        EncounterUI.instance.flavor[4].text = (roll < 21) ? "Hidden behind the wall, you discover a MGUFFIN!" : "After about 25 minutes of searching, you move on";
        EncounterUI.instance.flavor[5].text = "";
        EncounterUI.instance.flavor[6].text = "";
        EncounterUI.instance.flavor[7].text = (roll < 21) ? $"+{EncounterUI.instance.currentEncounter.pullTime + 5} mins" : $"+{EncounterUI.instance.currentEncounter.pullTime + 25} mins";
        EncounterUI.instance.flavor[8].text = "";
        EncounterUI.instance.flavor[9].text = "";
        TimeManagement.instance.MinuteAdd((roll < 21) ? EncounterUI.instance.currentEncounter.pullTime + 5 : EncounterUI.instance.currentEncounter.pullTime + 25);
        if(roll < 21)
        {
            //Give Mgguffin
        }
        Setup("Ok");
    }

    public override void ButtonTwo()
    {
        EncounterUI.instance.flavor[0].text = "Moving On";
        EncounterUI.instance.flavor[1].text = "";
        EncounterUI.instance.flavor[2].text = "";
        EncounterUI.instance.flavor[3].text = "You really don't have the time to waste";
        EncounterUI.instance.flavor[4].text = "";
        EncounterUI.instance.flavor[5].text = "You clear the mobs and move on";
        EncounterUI.instance.flavor[6].text = "";
        EncounterUI.instance.flavor[7].text = "";
        EncounterUI.instance.flavor[8].text = "";
        EncounterUI.instance.flavor[9].text = $"+{EncounterUI.instance.currentEncounter.pullTime} mins";
        TimeManagement.instance.MinuteAdd(EncounterUI.instance.currentEncounter.pullTime);
        Setup("Ok");
    }
}
