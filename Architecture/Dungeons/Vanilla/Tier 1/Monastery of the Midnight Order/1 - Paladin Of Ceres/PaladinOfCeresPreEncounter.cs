using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinOfCeresPreEncounter : PreEncounter
{
    public override void Begin()
    {
        circumstance = 0;
        EncounterUI.instance.flavor[0].text = "Paladin of Ceres";
        EncounterUI.instance.flavor[1].text = "";
        EncounterUI.instance.flavor[2].text = "";
        EncounterUI.instance.flavor[3].text = "As you move down the hallways clearing out some of the disciples you move into a large prayer room and are confronted by a worshipper of the forge goddess, Ceres";
        EncounterUI.instance.flavor[4].text = "";
        EncounterUI.instance.flavor[5].text = "A Paladin of the flame.";
        EncounterUI.instance.flavor[6].text = "";
        EncounterUI.instance.flavor[7].text = "Move yourself into position and prepare for the fight.";
        EncounterUI.instance.flavor[8].text = "";
        EncounterUI.instance.flavor[9].text = $"+{EncounterUI.instance.currentEncounter.pullTime} mins";
        TimeManagement.instance.MinuteAdd(20);
        Setup("Let's do this!");
    }
}
