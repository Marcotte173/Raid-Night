using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hibernation : Heal
{    
    public float dotTime;
    public override void Effect()
    {
        HibernateBuff h = Instantiate(GameObjectList.instance.hibernate, target.transform);
        h.attacker = character;
        h.damage = character.spellpower.value * damage / 100;
        h.threat = threat;
        h.timer = Mathf.Round(dotTime / (1 + character.spellpower.value / 100));
        for (float i = h.timer; i <= 0; i--) h.threshHold.Add(i);
        h.target = target;
        Debug.Log(h.timer);
        h.Timer();
    }
}
