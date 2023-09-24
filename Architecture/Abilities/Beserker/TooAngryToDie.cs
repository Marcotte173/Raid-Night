using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooAngryToDie : Ability
{
    public float dotTimer;
    public float partyVamp;
    public float primaryVamp;
    public float threshHold;
    public override void Effect()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Player()) TooAngryToDieBuff(a);
        Utility.instance.DamageNumber(character, "Too Angry To Die!", SpriteList.instance.beserker);
    }
    void TooAngryToDieBuff(Character a)
    {
        TooAngryToDieBuff r = Instantiate(GameObjectList.instance.tooAngryToDie, a.transform);
        r.attacker = character;
        r.timer = dotTimer;
        r.target = a;
        if (a != character) r.damage = partyVamp;
        else r.damage = primaryVamp;
        r.Timer();
    }
}
