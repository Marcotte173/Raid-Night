using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AminarisAbility : Ability
{
    public List<Character> affectedPlayer;
    private void Start()
    {
        cooldownTimer = 6;
    }
    public override void Effect()
    {
        Character c = Target.instance.Farthest(character, DungeonManager.instance.currentDungeon.currentEncounter.Player());
        affectedPlayer.Add(character.target);
        affectedPlayer.Add(c);
        target = character.target = character.dashCastTarget = c;
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
        character.dashDirect = false;
        foreach (Character c in affectedPlayer) c.TakeDamage(character, damage, true, "Charge!");
        if (!affectedPlayer[0].ko) character.target = affectedPlayer[0];
        character.state = DecisionState.Downtime;
    }
}
