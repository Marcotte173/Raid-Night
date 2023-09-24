using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InTune : Heal
{
    public float hotTime;
    public float manaRegen;
    public List<float> threshHold;
    public override void Effect()
    {
        if (AbilityCheck.instance.InTune(target) == null) InTuneHot();
        else AbilityCheck.instance.InTune(target).ResetTimer();       
    }

    private void InTuneHot()
    {
        InTuneBuff h = Instantiate(GameObjectList.instance.inTune, target.transform);
        h.attacker = character;
        h.damage = damage * character.spellpower.value / 100; ;
        h.timer = hotTime;
        h.threshHold = threshHold.ToList();
        h.manaBonus = manaRegen;
        h.target = target;
        h.Timer();
    }
}
