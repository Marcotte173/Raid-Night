using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ClawsFromTheDeep : AbilityHazard
{
    public List<float> threshhold;
    public float timer;

    public override void TriggerHazard()
    {
        ClawsFromTheDeepHazard r = Instantiate(GameObjectList.instance.clawsFromTheDeep, DungeonManager.instance.currentDungeon.currentEncounter.transform);
        r.sound = SoundList.instance.clawsFromTheDeep;
        r.transform.position = hazardPosition;
        r.timer = timer;
        r.threshHold = threshhold.ToList();
        r.damage = character.spellpower.value * damage / 100;
        r.attacker = character;
        r.threat = threat;
        r.transform.localScale = new Vector3(width, length);
        r.Timer();
    }
}
