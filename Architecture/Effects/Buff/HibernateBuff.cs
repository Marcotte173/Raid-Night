using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HibernateBuff : Effect
{
    private void Start()
    {
        flavor.Add("Hibernate");
        flavor.Add("Heals target over time");
        flavor.Add("Target is asleep and cannot act");
        flavor.Add("");
        Utility.instance.DamageNumber(target, $"Asleep", SpriteList.instance.druid);
        target.buff.Add(this);
        target.state = DecisionState.Asleep;
        target.action = "Asleep";
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectTick()
    {
        target.Heal(damage,true, attacker);
    }
    public override void EffectEnd()
    {
        target.buff.Remove(this);
        Utility.instance.DamageNumber(target, $"Awake", SpriteList.instance.druid);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.state = DecisionState.Decision;
        Destroy(gameObject);
    }
}
