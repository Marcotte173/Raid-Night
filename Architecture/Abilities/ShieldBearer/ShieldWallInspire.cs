using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWallInspire : AbilityHazard
{
    public float buffLength;
    public float shieldLength;
    public List<float> threshHold;
    public override void TriggerHazard()
    {
        ShieldWallInspireHazard r = Instantiate(GameObjectList.instance.shieldWallInspire, DungeonManager.instance.currentDungeon.currentEncounter.transform);
        r.timer = buffLength;
        r.damage = character.defence.value * damage / 100;
        r.shieldWallStackAmount = new List<float> { 0, r.damage, r.damage * 2, r.damage * 3, r.damage * 4, r.damage * 5 };
        r.threshHold = threshHold.ToList();
        r.shieldWallStackLength = shieldLength;
        r.attacker = character;
        r.transform.localScale = new Vector3(width, length);
        r.transform.position = hazardPosition;
        r.Timer();
    }
}
