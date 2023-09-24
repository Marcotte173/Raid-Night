using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDecayBuff : Effect
{
    public float moveChange;
    void Start()
    {
        flavor.Add("Time Decay");
        flavor.Add("Increases movement speed by "+ moveChange + " percent");
        flavor.Add("");
        flavor.Add("");
        target.buff.Add(this);
        target.movement.AddModifier(new StatModifier(moveChange/100, StatModType.Percent, this, "Time Decay Movement Buff"));
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectEnd()
    {
        target.movement.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.buff.Remove(this);
        Destroy(gameObject);
    }
}
