using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirgeOfDemiseBuff : Effect
{
    public float cost;
    private void Start()
    {
        flavor.Add("Dirge Of Demise");
        flavor.Add("Player Damage and Spellpower increased by " + damage);
        flavor.Add("");
        flavor.Add("");
        target.damage.AddModifier(new StatModifier(damage, StatModType.Flat, this, "Dirge Of Demise Damage"));
        target.spellpower.AddModifier(new StatModifier(damage, StatModType.Flat, this, "Dirge Of Demise Spellpower"));
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.damage.RemoveAllModifiersFromSource(this);
        target.spellpower.RemoveAllModifiersFromSource(this);
        target.buff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
