using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : Ability
{
    public override void Effect()
    {
        float dam = character.Damage() * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), true, "Slam: ");
    }
}
