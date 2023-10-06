using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleansingFlame : Hazard
{   
    public override void EffectTick()
    {
        base.EffectTick();
        foreach (Character a in playersAndMinions) a.GetComponent<Class>().TakeDamage(attacker, damage, false, "");
    }
    public override void EffectEnd()
    {
        SoundManager.instance.StopEffect(sound);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        DungeonManager.instance.currentDungeon.currentEncounter.enemyHazards.Remove(this);
        Destroy(gameObject);
    }
}
