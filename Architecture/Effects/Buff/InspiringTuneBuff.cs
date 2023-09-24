using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspiringTuneBuff : Effect
{
    public float damageReduction;
    private void Start()
    {
        flavor.Add("Inspiring Tune");
        flavor.Add($"Heals target over time and reduces Damage taken by {damageReduction}%");
        flavor.Add("");
        flavor.Add("");
        target.damageTakenMod.AddModifier(new StatModifier(-damageReduction/100, StatModType.Percent, this, "Inspiring Tune"));
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
        target.damageTakenMod.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
