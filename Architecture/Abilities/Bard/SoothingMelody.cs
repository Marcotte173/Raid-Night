using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SoothingMelody : Heal
{
    public float hotTime;
    public List<float> threshHold;
    public override void Effect()
    {
        if (AbilityCheck.instance.SoothingMelodyHotCheck(target) == null) SoothingMelodyHot();
        else AbilityCheck.instance.SoothingMelodyHotCheck(target).ResetTimer();
    }
    public void SoothingMelodyHot()
    {
        SoothingMelodyHot h = Instantiate(GameObjectList.instance.soothingMelodyHot, target.transform);
        h.attacker = character;
        h.damage = character.spellpower.value * damage / 100;
        h.threshHold = threshHold.ToList();
        h.timer = hotTime;
        h.target = target;
        h.Timer();
    }
}
