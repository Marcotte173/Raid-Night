using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Uplift : Heal
{
    public float hotTime;
    public float hotHeal;
    public List<float> threshHold;
    public override void Effect()
    {
        target.Heal(damage * character.spellpower.value / 100,true, character);
        if (AbilityCheck.instance.UpliftHotCheck(target) == null) UpliftHot();
        else AbilityCheck.instance.UpliftHotCheck(target).ResetTimer();
    }
    public void UpliftHot()
    {
        UpliftHot h = Instantiate(GameObjectList.instance.upliftHot, target.transform);
        h.attacker = character;
        h.damage = character.spellpower.value * hotHeal / 100;
        h.threshHold = threshHold.ToList();
        h.timer = hotTime;
        h.target = target;
        h.Timer();
    }
}
