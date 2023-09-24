using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BloodyMess : Ability
{
    public float dotTime;
    public float dotDamage;
    public List<float> threshold;
    public override void Effect()
    {
        List<Character> target = new List<Character> { } ;
        foreach(Character a in DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion())
        {
            foreach(Tile t in character.TileRadius(character.move.currentTile, 3))
            {
                if(t == a.move.currentTile)
                {
                    target.Add(a);                    
                    break;
                }               
            }
        }
        if (target.Count > 0)
        {
            foreach(Character a in target) Bloody(a);
            if (AbilityCheck.instance.MySetup(character) != null) AbilityCheck.instance.MySetup(character).EffectEnd();
        }
    }

    public void Bloody(Character hit)
    {
        float extraDamage = 0;
        if (AbilityCheck.instance.MySetup(character) != null)
        {
            extraDamage += character.Damage() * dotDamage / 100 * AbilityCheck.instance.SetupCount(character);            
            if (AbilityCheck.instance.BleedCheck(target) != null)
            {
                if (AbilityCheck.instance.RemainingBleed(target) > (threshold.Count - 1) * damage) AbilityCheck.instance.BleedCheck(target).ResetTimer();
                else
                {
                    AbilityCheck.instance.BleedCheck(target).EffectEnd();
                    BloodyDot(extraDamage, hit);
                }
            }
            else BloodyDot(extraDamage, hit);
        }        
        float dam = character.Damage() * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(damage, threat), true, "Bloody Mess: ");        
    }

    private void BloodyDot(float dam, Character hit)
    {
        BloodyMessBleed r = Instantiate(GameObjectList.instance.bloodyMess, hit.transform);
        r.attacker = character;
        r.damage = dam;
        r.timer = dotTime;
        r.target = hit;
        r.threshHold = threshold.ToList();
        r.Timer();
    }
}
