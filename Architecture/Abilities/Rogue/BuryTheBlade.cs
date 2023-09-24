using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuryTheBlade : Ability
{
    public float dotDamage;
    public override void Effect()
    {
        float extraDamage = character.Damage() * dotDamage / 100 * AbilityCheck.instance.SetupCount(character);
        if(AbilityCheck.instance.MySetup(character)!=null)AbilityCheck.instance.MySetup(character).EffectEnd();
        float dam = character.Damage() * damage / 100;
        dam += extraDamage;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(damage, threat), true, "Bury The Blade: ");
    }
}
