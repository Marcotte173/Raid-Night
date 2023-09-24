using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDecayDebuff : Effect
{
    private void Start()
    {
        flavor.Add("Time Decay ");
        flavor.Add("Damages target over time");
        flavor.Add("");
        flavor.Add("");
        target.debuff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectTick()
    {
        target.GetComponent<Boss>().TakeDamage(attacker, damage, Utility.instance.Threat(damage, threat), false, "");
    }
    public override void EffectEnd()
    {
        target.GetComponent<Boss>().TakeDamage(attacker, damage, Utility.instance.Threat(damage, threat), false, "");
        target.debuff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
