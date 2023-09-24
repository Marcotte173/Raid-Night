using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessSwing : Ability
{
    public float secondaryDamage;
    public float nerfAmount;
    public float dotTime;
    public override void Effect()
    {
        float primaryDam = character.Damage() * damage / 100;
        float secondaryDam = character.Damage() * secondaryDamage / 100;
        List<Character> enemies = character.InRange(character, rangeRequired,DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion());
        foreach (Character c in enemies)
        {
            if (c != target)
            {
                c.GetComponent<Boss>().TakeDamage(character, secondaryDam, Utility.instance.Threat(secondaryDam, threat), true, "Reckless Swing: ");
                break;
            }
        }
        RecklessSwingDefenceNerf();
        target.GetComponent<Boss>().TakeDamage(character, primaryDam, Utility.instance.Threat(primaryDam, threat), true, "");
    }
    void RecklessSwingDefenceNerf()
    {
        RecklessSwingDefenceNerf r = Instantiate(GameObjectList.instance.recklessSwingDefenceNerf, target.transform);
        r.attacker = character;
        r.damage = nerfAmount;
        r.timer = dotTime;
        r.target = character;
        r.Timer();
    }
}
