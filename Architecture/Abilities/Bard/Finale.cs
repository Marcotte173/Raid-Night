using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Finale : Heal
{
    public float hasteLength;
    public float hasteBuff;
    public override void Effect()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion())
        {
            float healBonus = 0;
            foreach (Effect e in a.buff.ToList())
            {               
                if (e.type1 == EffectType.Hot || e.type2 == EffectType.Hot || e.type3 == EffectType.Hot)
                {
                    healBonus += (e.damage * (e.threshHold.Count - 1)) * 2;
                    if (e.GetType() == typeof(Crescendo1))
                    {
                        Crescendo1 c = (Crescendo1)e;
                        c.Crescendo2();
                    }
                    e.EffectEnd();
                }
            }
            Debug.Log(healBonus);
            float dam = character.spellpower.value * (damage / 100) + healBonus;
            a.Heal(dam, true, character);
            FinaleBuff(a);     
        }
        
    }
    public void FinaleBuff(Character a)
    {
        FinaleBuff h = Instantiate(GameObjectList.instance.finaleBuff, a.transform);
        h.attacker = character;
        h.damage = hasteBuff;
        h.timer = hasteLength;
        h.target = a;
        h.Timer();
    }
}
