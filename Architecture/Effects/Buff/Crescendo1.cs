using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crescendo1 : Effect
{
    public float adBonus;
    public float damBonus;
    public float damBonusTime;
    
    private void Start()
    {
        flavor.Add("Crescendo");
        flavor.Add($"Heals target over time and increases Attack Damage by {adBonus * timeRemaining}%");
        flavor.Add("");
        flavor.Add("");
        target.damage.AddModifier(new StatModifier(adBonus*timeRemaining/100, StatModType.Percent, this, "Crescendo"));
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectTick()
    {
        target.damage.RemoveAllModifiersFromSource(this);
        target.damage.AddModifier(new StatModifier(adBonus * timeRemaining / 100, StatModType.Percent, this, "Crescendo"));
        flavor[1] = ($"Heals target over time and increases Attack Damage by {adBonus * timeRemaining}%");
        target.Heal(damage,true, attacker);   
    }

    public override void EffectEnd()
    {
        target.buff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.damage.RemoveAllModifiersFromSource(this);
        Destroy(gameObject);
    }
    public void Crescendo2()
    {
        Crescendo2 h = Instantiate(GameObjectList.instance.crescendo2, target.transform);
        h.attacker = attacker;
        h.damage = damBonus;
        h.timer = damBonusTime;
        h.target = target;
        h.Timer();
    }
    
}
