using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RaggedWound : Ability
{
    public float dotTime;
    public float dotDamage;
    public List<float> threshold;
    public override void Effect()
    {
        if (AbilityCheck.instance.BleedCheck(target) != null)
        {
            if (AbilityCheck.instance.RemainingBleed(target) > (threshold.Count - 1) * damage) AbilityCheck.instance.BleedCheck(target).ResetTimer();
            else
            {
                AbilityCheck.instance.BleedCheck(target).EffectEnd();
                RaggedWoundDot();
            }
        }        
        else RaggedWoundDot();
        float dam = character.Damage() * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), true, "Ragged Wound: ");
    }
    public void RaggedWoundDot()
    {        
        RaggedWoundBleed r = Instantiate(GameObjectList.instance.raggedWound, target.transform);
        r.attacker = character; 
        r.damage = character.Damage() * dotDamage / 100;
        r.timer = dotTime;
        r.target = target;
        r.threshHold = threshold.ToList();
        r.Timer();
    }
}
