using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntDebuff : Effect
{
    private void Start()
    {
        flavor.Add("Taunt");
        flavor.Add("Target is compelled to attack taunter");
        flavor.Add("");
        flavor.Add("");
        target.targetFromTaunt = attacker ;
        target.debuff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.targetFromTaunt = null;
        target.debuff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
