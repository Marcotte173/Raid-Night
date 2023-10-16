using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RethaoftheBoneKnittersRegenerationAbility : Ability
{
    public float hotTime;
    public List<float> threshHold;
    public override void Effect()
    {
        RegenerationHOT h = Instantiate(GameObjectList.instance.regeneration, target.transform);
        h.attacker = character;
        h.damage = damage;
        h.threshHold = threshHold.ToList();
        h.timer = hotTime;
        h.target = character;
        h.Timer();
    }
}
