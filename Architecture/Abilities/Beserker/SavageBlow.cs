using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SavageBlow : Ability
{
    public float dotTime;
    public float dotDamage;
    public List<float> threshold;
    public override void Effect()
    {
        SavageBlowDot();
        float dam = character.Damage() * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), true, "Savage Blow: ");
    }
    public void SavageBlowDot()
    {
        SavageBlowDot r = Instantiate(GameObjectList.instance.savageBlowDot, target.transform);
        r.attacker = character;
        r.damage = character.Damage() * dotDamage / 100;
        r.timer = dotTime;
        r.target = target;
        r.threshHold = threshold.ToList();
        r.Timer();
    }
}
