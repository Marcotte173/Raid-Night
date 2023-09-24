using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneTendrils : Effect
{
    public float moveChange;
    public float hasteChange;
    private void Start()
    {
        flavor.Add("Arcane Tendrils");
        flavor.Add("Lowers attack and movespeed by " + moveChange + " percent");
        flavor.Add("");
        flavor.Add("");
        target.haste.AddModifier(new StatModifier(-hasteChange/100, StatModType.Flat, this, "Tendrils Haste Debuff"));
        target.movement.AddModifier(new StatModifier(-moveChange/100, StatModType.Percent, this, "Tendrils Move Debuff"));
        target.debuff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectEnd()
    {
        target.haste.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.movement.RemoveAllModifiersFromSource(this);
        target.debuff.Remove(this);
        Destroy(gameObject);
    }
}
