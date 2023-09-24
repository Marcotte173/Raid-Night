using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTuneBuff : Effect
{
    public float manaBonus;
    private void Start()
    {
        flavor.Add("In Tune");
        flavor.Add($"Heals target over time and increases Mana regen by {manaBonus}%");
        flavor.Add("");
        flavor.Add("");
        target.manaRegenMod.AddModifier(new StatModifier(manaBonus/100, StatModType.Percent, this, "In Tune"));
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public override void EffectTick()
    {
        target.Heal(damage,true, attacker);
    }

    public override void EffectEnd()
    {
        target.buff.Remove(this);
        target.manaRegenMod.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
