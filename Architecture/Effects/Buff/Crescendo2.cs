using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crescendo2 : Effect
{
    private void Start()
    {
        flavor.Add("Crescendo");
        flavor.Add($"Physical Damage increased by {damage}%");
        flavor.Add("");
        flavor.Add("");
        target.physicalDamageMod.AddModifier(new StatModifier(damage/100, StatModType.Percent, this, "Crescendo"));
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.buff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.physicalDamageMod.RemoveAllModifiersFromSource(this);
        Destroy(gameObject);
    }
}
