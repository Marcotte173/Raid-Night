using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Crescendo : Heal
{
    public float hotTime;
    public List<float> threshHold;
    public float aDBonus;
    public float damBonus;
    public float damBonusTime;
    public override void Effect()
    {
        Crescendo1 h = Instantiate(GameObjectList.instance.crescendo1, target.transform);
        h.attacker = character;
        h.damage = damage * character.spellpower.value / 100;
        h.timer = hotTime;
        h.threshHold = threshHold.ToList();
        h.adBonus = aDBonus;
        h.target = target;
        h.damBonus = damBonus;
        h.damBonusTime = damBonusTime;
        h.Timer();
    }
}
