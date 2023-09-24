using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakAttack : Ability
{
    public float dotDamage;
    public override void Effect()
    {
        int howMany = (AbilityCheck.instance.SetupCount(character) >= 2) ? 2 : AbilityCheck.instance.SetupCount(character);
        float extraDamage = character.Damage() * dotDamage / 100 * howMany;
        AbilityCheck.instance.MySetup(character).SubtractCount(howMany);
        float dam = character.Damage() * damage / 100;
        dam += extraDamage;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(damage, threat), true, "Sneak Attack: ");
    }
}
