using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redoubt : Ability
{
    public float buffLength;
    public override void Effect()
    {
        if (AbilityCheck.instance.Redoubt(character) == null)
        {
            RedoubtBuff r = Instantiate(GameObjectList.instance.redoubt, character.transform);
            r.attacker = character;
            r.damage = damage;
            r.timer = buffLength;
            r.target = character;
            r.Timer();
        }
        else AbilityCheck.instance.Redoubt(character).ResetTimer();
    }
}
