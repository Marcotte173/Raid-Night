using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceBurnBuff : Effect
{
    public float cost;
    public float recoveryLength;
    public float manaRecoveryMod;
    private void Start()
    {
        flavor.Add("Essence Burn");
        flavor.Add("Magic Damage increased by " + damage + " percent");
        flavor.Add("Spell Cost increased by " + cost + " percent");
        flavor.Add("");
        target.buff.Add(this);
        target.magicDamageMod.AddModifier(new StatModifier(damage/100, StatModType.Percent, this, "Essence Burn Spellpower"));
        target.energyCostMod.AddModifier(new StatModifier(cost/100, StatModType.Percent, this, "Essence Burn Cost"));
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.buff.Remove(this);
        target.magicDamageMod.RemoveAllModifiersFromSource(this);
        target.energyCostMod.RemoveAllModifiersFromSource(this);
        EssenceRecoveryHot();
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
    public void EssenceRecoveryHot()
    {
        EssenceRecovery r = Instantiate(GameObjectList.instance.essenceRecovery);
        r.target = target;
        r.timer = recoveryLength;
        r.damage = manaRecoveryMod;
        r.Timer();
    }
}
