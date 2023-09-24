using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Rot : Ability
{
    public List<float> threshhold;
    public float dotTime;
    public override void Effect()
    {
        if (AbilityCheck.instance.MyRot(target, character) == null) RotDot();
        else AbilityCheck.instance.MyRot(target, character).ResetTimer();
    }
    public void RotDot()
    {
        RotDot r = Instantiate(GameObjectList.instance.rot, target.transform);
        r.attacker = character;
        r.damage = character.spellpower.value * damage / 100;
        r.threat = threat / 100;
        r.threshHold = threshhold.ToList();
        r.timer = dotTime;
        r.target = target;
        r.Timer();
    }
}
