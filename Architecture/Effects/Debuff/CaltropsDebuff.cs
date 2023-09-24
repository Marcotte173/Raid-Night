using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropsDebuff : Effect
{
    public float moveChange;
    private void Start()
    {
        flavor.Add("Caltrops");
        flavor.Add("Lowers movespeed by " + moveChange * 100 + " percent");
        flavor.Add("");
        flavor.Add("");
        target.movement.AddModifier(new StatModifier(-moveChange, StatModType.Percent, this, "Caltrops Move Debuff"));
        target.debuff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectEnd()
    {
        target.movement.RemoveAllModifiersFromSource(this);
        target.debuff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
