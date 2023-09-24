using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpike : Ability
{
    public float buffTimer;
    public override void Effect()
    {
        if (AbilityCheck.instance.ShieldSpike(character) == null)
        {
            ShieldSpikeBuff r = Instantiate(GameObjectList.instance.shieldSpike, character.transform);
            r.attacker = character;
            r.damage = damage / 100 * character.offHand.defence;
            r.timer = buffTimer;
            r.target = character;
            r.Timer();
        }
        else AbilityCheck.instance.ShieldSpike(character).ResetTimer();
    }
}
