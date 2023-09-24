using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpikeBuff : Effect
{
    private void Start()
    {
        flavor.Add("Shield Spike");
        flavor.Add($"Thorns +{damage}");
        flavor.Add("");
        flavor.Add("");
        target.thorns.AddModifier(new StatModifier(damage, StatModType.Flat, this, "Shield Spike"));
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
        
    public override void EffectEnd()
    {
        target.thorns.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.targetFromTaunt = null;
        target.buff.Remove(this);
        Destroy(gameObject);
    }
}
