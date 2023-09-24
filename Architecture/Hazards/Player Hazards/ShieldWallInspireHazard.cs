using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWallInspireHazard : Hazard
{
    public float shieldWallStackLength;
    public List<float> shieldWallStackAmount;
    public override void EffectTick()
    {
        foreach (Character a in playersAndMinions) AbilityCheck.instance.ShieldStack(attacker,a, shieldWallStackLength, shieldWallStackAmount);
    }
    public override void EffectEnd()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        DungeonManager.instance.currentDungeon.currentEncounter.playerHazards.Remove(this);
        Destroy(gameObject);
    }
}
