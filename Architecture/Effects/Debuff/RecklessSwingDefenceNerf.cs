using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessSwingDefenceNerf : Effect
{
    private void Start()
    {
        flavor.Add("Recklessness");
        flavor.Add("Defence increased by " + damage + " percent");
        flavor.Add("");
        flavor.Add("");
        target.defence.AddModifier(new StatModifier(-damage,StatModType.Percent, this,"Recklessness"));
        target.debuff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.defence.RemoveAllModifiersFromSource(this);
        target.debuff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
