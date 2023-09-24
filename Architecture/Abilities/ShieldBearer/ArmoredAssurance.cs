using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredAssurance : Heal
{
    public override void Effect()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion())
        {
            if (AbilityCheck.instance.ShieldValue(a) > 0)
            {
                a.Heal(AbilityCheck.instance.ShieldValue(a) * damage / 100,true, character);
                AbilityCheck.instance.MyShield(a).EffectEnd();
            }
        }
    }
}
