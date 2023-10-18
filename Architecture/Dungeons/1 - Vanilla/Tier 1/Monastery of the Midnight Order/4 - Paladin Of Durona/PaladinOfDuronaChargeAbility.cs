using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinOfDuronaChargeAbility : Ability
{
    public override void Effect()
    {
        Character c = Target.instance.NoAggroRandom(character, DungeonManager.instance.currentDungeon.currentEncounter.player);
        character.target = target = character.dashCastTarget = c;
        cast = false;
        ProtectionDash();
    }
    private void ProtectionDash()
    {
        character.state = DecisionState.DashCast;
        dash = true;
        character.dashDirect = true;
    }
    public override void DashTrigger()
    {
        dash = false;
        character.dashCastTarget.TakeDamage(character, damage, true, "Charge!");
        character.state = DecisionState.Downtime;
    }
}