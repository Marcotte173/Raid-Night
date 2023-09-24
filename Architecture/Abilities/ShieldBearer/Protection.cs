using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : Heal
{
    
    public float protectionLength;
    public override void Effect()
    {
        character.dashCastTarget = target;
        cast = false;
        ProtectionDash();
    }
    private void ProtectionDash()
    {
        character.state = DecisionState.DashCast;
        dash = true;
        character.dashDirect = false;
    }
    public override void DashTrigger()
    {
        dash = false;
        ProtectionBuff(target);
        ProtectionBuff(character);
        character.state = DecisionState.Downtime;
    }

    private void ProtectionBuff(Character shieldTarget)
    {
        AbilityCheck.instance.Shield(character, shieldTarget, damage / 100 * character.offHand.defence, protectionLength);
    }
}
