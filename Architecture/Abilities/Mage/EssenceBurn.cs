using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceBurn : Ability
{
    public float timer;
    public float spelllCostMod;
    public float recoveryLength;
    public float manaRecoveryMod;

    public override void Effect()
    {
        EssenceBurnBuff r = Instantiate(GameObjectList.instance.essenceBurn, character.transform);
        r.target = character;
        r.timer = timer;
        r.damage = damage;
        r.cost = spelllCostMod;
        r.recoveryLength = recoveryLength;
        r.manaRecoveryMod = manaRecoveryMod;
        r.Timer();
    }
}
