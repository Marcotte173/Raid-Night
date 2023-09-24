using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InspiringTune : Heal
{
    public float hotTime;
    public float damageReduction;
    public List<float> threshHold;
    public override void Effect()
    {
        if (AbilityCheck.instance.InspiringTune(target) == null) InspiringTuneHot();
        else AbilityCheck.instance.InspiringTune(target).ResetTimer();
       
    }

    private void InspiringTuneHot()
    {
        InspiringTuneBuff h = Instantiate(GameObjectList.instance.inspiringTune, target.transform);
        h.attacker = character;
        h.damage = damage * character.spellpower.value / 100; ;
        h.timer = hotTime;
        h.threshHold = threshHold.ToList();
        h.damageReduction = damageReduction;
        h.target = target;
        h.Timer();
    }
}
