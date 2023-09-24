using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoothingMelodyHot : Effect
{
    private void Start()
    {
        flavor.Add("Soothing Melody");
        flavor.Add("Heals target over time target over time");
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
