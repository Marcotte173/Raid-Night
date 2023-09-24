using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedoubtBuff : Effect
{
    private void Start()
    {
        flavor.Add("Redoubt Spike");
        flavor.Add($"Increased armor by {damage}");
        flavor.Add("Thorns affects all enemies near the Shield Bearer");
        flavor.Add("");
        target.defence.AddModifier(new StatModifier(damage, StatModType.Flat, this, "Redoubt"));
        target.thornsAoe = true;
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.defence.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.thornsAoe = false;
        target.targetFromTaunt = null;
        target.buff.Remove(this);
        Destroy(gameObject);
    }
}
