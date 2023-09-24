using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Verdure : Heal
{    
    public float hotTime;
    public float hotDamage;
    public List<float> threshHold;
    public override void Effect()
    {
        Utility.instance.DamageNumber(character, "Verdure", SpriteList.instance.druid);
        target.Heal(damage * character.spellpower.value / 100,true, character);
        if (AbilityCheck.instance.GreaterHealHotCheck(target) == null) BigHealHot();
        else AbilityCheck.instance.GreaterHealHotCheck(target).ResetTimer();
    }
    public void BigHealHot()
    {
        GreaterHealHot h = Instantiate(GameObjectList.instance.greaterHealHot, target.transform);
        h.attacker = character;
        h.damage = character.spellpower.value * hotDamage / 100;
        h.threat = threat;
        h.threshHold = threshHold.ToList();
        h.timer = hotTime;
        h.target = target;
        h.Timer();
    }
}
