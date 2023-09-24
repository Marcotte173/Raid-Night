using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinaleInspire : Heal
{
    public float hasteLength;
    public float hasteBuff;
    public override void Effect()
    {       
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion())
        {
            float hasteBonus = AbilityCheck.instance.TotalHotBuffs(a) * 3;
            a.ManaGain(a.maxMana.value * damage/100);            
            foreach (Effect e in a.buff.ToList())
            {
                if (e.type1 == EffectType.Hot || e.type2 == EffectType.Hot || e.type3 == EffectType.Hot)
                {
                    if (e.GetType() == typeof(Crescendo1))
                    {
                        Crescendo1 c = (Crescendo1)e;
                        c.Crescendo2();
                    }
                    e.EffectEnd();
                }
            }
            FinaleInspireBuff(hasteBonus, a);
        }
    }
    public void FinaleInspireBuff(float damage, Character a)
    {
        FinaleInspiring h = Instantiate(GameObjectList.instance.finaleInspire, a.transform);
        h.attacker = character;
        h.damage = hasteBuff + damage;
        h.timer = hasteLength;
        h.target = a;
        h.Timer();
    }
}
