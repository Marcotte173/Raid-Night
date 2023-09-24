using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummyPreEncounter : PreEncounter
{
    public override void Circumstances()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.BeginPreSetup();
    }
}
