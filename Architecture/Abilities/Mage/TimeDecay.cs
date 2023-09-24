using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TimeDecay : Ability
{
    public float moveBonus;
    public float moveBonusTime;
    public float dotTime;
    public List<float> threshHold;
    public override void Effect()
    {
        TimeDecayBuff();
        TimeDecayDebuff();
    }
    private void TimeDecayBuff()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion())
        {
            TimeDecayBuff r = Instantiate(GameObjectList.instance.timeDecayBuff, a.transform);
            r.moveChange = moveBonus;
            r.timer = moveBonusTime;
            r.target = a;
            r.Timer();
        }
    }
    private void TimeDecayDebuff()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion())
        {
            TimeDecayDebuff r = Instantiate(GameObjectList.instance.timeDecayDebuff, a.transform);
            r.attacker = character;
            r.damage = character.spellpower.value * damage / 100;
            r.threat = threat;
            r.threshHold = threshHold.ToList();
            r.timer = dotTime;
            r.target = a;
            r.Timer();
        }
    }
}