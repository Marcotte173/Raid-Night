using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Hemorage : Ability
{
    public float maxCast;
    public float dotDamage;
    public float dotTime;
    public List<float> threshold;
    public override void Effect()
    {
        float dam = character.Damage() * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), true, "hemorage: ");
        float totalRemove = (AbilityCheck.instance.SetupCount(character) - maxCast > 0) ? maxCast : AbilityCheck.instance.SetupCount(character);
        if (totalRemove > 0)
        {
            AbilityCheck.instance.MySetup(character).SubtractCount(Convert.ToInt32(totalRemove));
            if (AbilityCheck.instance.BleedCheck(target) != null)
            {
                if (AbilityCheck.instance.RemainingBleed(target) > (threshold.Count - 1) * damage) AbilityCheck.instance.BleedCheck(target).ResetTimer();
                else
                {
                    AbilityCheck.instance.BleedCheck(target).EffectEnd();
                    HemorageBuff(totalRemove);
                }
            }
            else HemorageBuff(totalRemove);
        }
    }
    public void HemorageBuff(float totalRemove)
    {
        HemorageBleed r = Instantiate(GameObjectList.instance.hemorage, target.transform);
        r.timer = dotTime;
        r.attacker = character;
        r.threshHold = threshold.ToList();
        r.Timer();
        r.damage = dotDamage * totalRemove;
        r.target = target;
    }
}
