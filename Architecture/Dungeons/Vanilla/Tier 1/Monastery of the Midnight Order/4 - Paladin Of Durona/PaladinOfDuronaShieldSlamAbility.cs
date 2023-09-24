using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaladinOfDuronaShieldSlamAbility : Ability
{
    public float knockbackAmount;
    public override void Effect()
    {
        target.TakeDamage(character, damage, true, "Shield Slam");
        target.knockbackAmount = knockbackAmount;
        target.knockBackSource = character.move.currentTile;
        target.state = DecisionState.Knockback;
        target.knockBack = false;
        Boss b = (Boss)character;
        b.ClearAggro();
    }
}
