using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropHazard : Hazard
{
    public float moveChange;
    public float caltropLength;
    public override void EffectTick()
    {
        foreach (Character a in enemies)
        {
            float dam = damage * attacker.Damage() / 100;
            a.TakeDamage(attacker, dam, Utility.instance.Threat(dam, threat), false, "");
            if (AbilityCheck.instance.CaltropCheck(a) != null) AbilityCheck.instance.CaltropCheck(a).ResetTimer();
            else
            {
                CaltropsDebuff r = Instantiate(GameObjectList.instance.caltrops, a.transform);
                r.timer = caltropLength;
                r.Timer();
                r.moveChange = moveChange;
                r.target = a;
                r.attacker = attacker; 
            }
        }
    }
    public override void EffectEnd()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        DungeonManager.instance.currentDungeon.currentEncounter.playerHazards.Remove(this);
        Destroy(gameObject);
    }
}
