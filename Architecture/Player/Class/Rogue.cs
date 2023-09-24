using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rogue : Class
{ 
    public override void Create()
    {
        base.Create();
        FindSpec();
        Equipment();
        CoreStats();
    }
    public override void FindSpec()
    {
        spec = (specNumber == 0) ? Spec.Wrathful : Spec.Focused;
    }

    public override void AbilityPopulate()
    {
        if (specNumber == 0) AbilityLoad(AbilityList.instance.rogueWrathful);
        else AbilityLoad(AbilityList.instance.rogueFocused);
    }
    public override void Equipment()
    {
        headSets.Add(ItemList.instance.startingHead[1]);
        chestSets.Add(ItemList.instance.startingChest[1]);
        legSets.Add(ItemList.instance.startingLegs[1]);
        feetSets.Add(ItemList.instance.startingFeet[1]);
        trinketSets.Add(ItemList.instance.startingTrinket[1]);
        weaponSets.Add(ItemList.instance.startingWeapon[2]);
        offHandSets.Add(ItemList.instance.startingOffHand[2]);
        SpecEquip();
    }
    public override void CoreStats()
    {
        base.CoreStats();
        maxHealth.baseValue = StatSpecs.instance.rogueMaxHealth[specNumber];
        maxMana.baseValue = StatSpecs.instance.rogueMaxMana[specNumber];
        defence.baseValue = StatSpecs.instance.rogueDefence[specNumber];
        spellpower.baseValue = StatSpecs.instance.rogueSpellpower[specNumber];
        damage.baseValue = StatSpecs.instance.rogueDamage[specNumber];
        crit.baseValue = StatSpecs.instance.rogueCrit[specNumber];
        manaRegenValue.baseValue = StatSpecs.instance.rogueManaRegenValue[specNumber];
        manaRegenTime.baseValue  = StatSpecs.instance.rogueManaRegenTime[specNumber];
    }
    public override void Decision()
    {
        GetComponent<RogueDecision>().Decide();
    }   
}