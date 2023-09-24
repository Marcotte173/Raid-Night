using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStand : Ability
{
    public float buffRange;
    public float buffLength;
    public float threshHoldToUse;

    public override void Effect()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion())
        {
            if (Vector2.Distance(a.transform.position, character.transform.position) <= buffRange)
            {
                LastStandBuff r = Instantiate(GameObjectList.instance.lastStand, a.transform);
                r.attacker = character;
                r.target = a;
                r.timer = buffLength;
                r.Timer();
            }
        }
    }
}
