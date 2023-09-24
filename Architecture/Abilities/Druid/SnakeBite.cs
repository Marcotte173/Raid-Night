using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBite : Ability
{
    public List<float> threshhold;
    public float dotTime;
    public float dotDamage;
    public override void Effect()
    {
        float dam = character.spellpower.value * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), false, "Snake Bite: ");
        if (AbilityCheck.instance.MySnakeBite(target, character) == null) SnakeBiteDot();
        else AbilityCheck.instance.MySnakeBite(target, character).ResetTimer();
    }
    public void SnakeBiteDot()
    {
        SnakeBiteDot r = Instantiate(GameObjectList.instance.snakeBite, target.transform);
        r.attacker = character;
        r.damage = character.spellpower.value * dotDamage / 100;
        r.threat = r.damage * threat / 100;
        r.threshHold = threshhold.ToList();
        r.timer = dotTime;
        r.target = target;
        r.Timer();
    }
}
