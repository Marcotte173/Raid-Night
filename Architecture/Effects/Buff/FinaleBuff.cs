using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleBuff : Effect
{
    public float moveChange;
    void Start()
    {
        flavor.Add("Grand Finale");
        flavor.Add("Haste increased by increased by " + damage + " percent");
        flavor.Add("");
        flavor.Add("");        
        if(target.haste.value >0) target.haste.AddModifier(new StatModifier(damage/100, StatModType.Percent, this, "Finale Haste Buff"));
        else target.haste.AddModifier(new StatModifier(damage, StatModType.Flat, this, "Finale Haste Buff"));
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectEnd()
    {
        target.haste.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.buff.Remove(this);
        Destroy(gameObject);
    }
}
