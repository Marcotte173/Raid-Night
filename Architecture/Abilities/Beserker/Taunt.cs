using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : Ability
{
    public float dotTime;
    public override void Effect()
    {
        TauntDebuff r = Instantiate(GameObjectList.instance.taunt, target.transform);
        r.attacker = character;
        r.timer = dotTime;
        r.target = target ;
        r.Timer();
    }
}
