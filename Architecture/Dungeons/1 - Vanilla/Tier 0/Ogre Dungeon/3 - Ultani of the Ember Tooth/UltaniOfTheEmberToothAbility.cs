using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class UltaniOfTheEmberToothAbility : Ability
{
    public List<float> threshhold;
    public float dotTime;
    public override void Effect()
    {
        BurnDot r = Instantiate(GameObjectList.instance.burn, target.transform);
        r.attacker = character;
        r.damage = damage;
        r.threat = threat / 100;
        r.threshHold = threshhold.ToList();
        r.timer = dotTime;
        r.target = target;
        r.Timer();
    }
}
