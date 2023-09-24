using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirgeOfDemise : Heal
{
    public float damageBonus;
    public float hotTime;
    public override void Effect()
    {
        target.Heal(damage * character.spellpower.value / 100,true,character);
        if (AbilityCheck.instance.DirgeOfDemiseBuffCheck(target) == null) DirgeOfDemiseBuff();
        else AbilityCheck.instance.DirgeOfDemiseBuffCheck(target).ResetTimer();
    }
    public void DirgeOfDemiseBuff()
    {
        DirgeOfDemiseBuff h = Instantiate(GameObjectList.instance.dirgeOfDemiseBuff, target.transform);
        h.attacker = character;
        h.damage = character.spellpower.value * damageBonus / 100;
        h.timer = hotTime;
        h.target = target;
        h.Timer();
    }
}
