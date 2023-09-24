using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceRecovery : Effect
{
    private void Start()
    {
        flavor.Add("Essence Recovery");
        flavor.Add("Mana Regen increased by " + damage + " percent");
        flavor.Add("Cannot cast Spells"); 
        flavor.Add("");
        target.buff.Add(this);
        target.manaRegenMod.AddModifier(new StatModifier(damage/100, StatModType.Percent, this, "Essence Recovery"));
        target.canCastSpells = false;
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.buff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.manaRegenMod.RemoveAllModifiersFromSource(this);
        target.canCastSpells = true;
        Destroy(gameObject);
    }
}
