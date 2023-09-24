using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawsFromTheDeepHazard : Hazard
{
    public override void EffectTick()
    {
        base.EffectTick();
        foreach (Character a in enemies) a.GetComponent<Boss>().TakeDamage(attacker, damage, Utility.instance.Threat(damage, threat), false, "");
    }
    public override void EffectEnd()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        DungeonManager.instance.currentDungeon.currentEncounter.playerHazards.Remove(this);
        Destroy(gameObject);
    }
}
