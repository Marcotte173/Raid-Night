using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarCry : Ability
{
    public float dotTimer;
    public override void Effect()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Player()) WarcryBuff(a);
        Utility.instance.DamageNumber(character, "Warcry", SpriteList.instance.beserker);
    }
    void WarcryBuff(Character a)
    {
        WarcryBuff r = Instantiate(GameObjectList.instance.warcry, a.transform);
        r.attacker = character;
        r.damage = damage;
        r.timer = dotTimer;
        r.target = a;
        r.Timer();
    }
}
