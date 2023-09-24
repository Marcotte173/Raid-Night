using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpliftHot : Effect
{
    private void Start()
    {
        flavor.Add("Uplift");
        flavor.Add("Heals target over time");
        flavor.Add("");
        flavor.Add("");
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectTick()
    {
        target.Heal(damage,true, attacker);
    }
    public override void EffectEnd()
    {
        target.buff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
