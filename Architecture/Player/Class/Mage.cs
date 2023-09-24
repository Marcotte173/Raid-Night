using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Class
{ 
    public override void Create()
    {
        base.Create();
        FindSpec();
        Equipment();
        CoreStats();
    }
    public override void UpdateStuff()
    {
        base.UpdateStuff();
    }

    public override void Decision()
    {
        GetComponent<MageDecision>().Decide();
    }
    public override void FindSpec()
    {
        spec = (specNumber == 0) ? Spec.Explosive : Spec.Focused;
    }
    public override void AbilityPopulate()
    {
        if (specNumber == 0) AbilityLoad(AbilityList.instance.mageExplosive);
        else AbilityLoad(AbilityList.instance.mageFocused);
    }

    public override void Equipment()
    {
        headSets.Add(ItemList.instance.startingHead[0]);
        chestSets.Add(ItemList.instance.startingChest[0]);
        legSets.Add(ItemList.instance.startingLegs[0]);
        feetSets.Add(ItemList.instance.startingFeet[0]);
        trinketSets.Add(ItemList.instance.startingTrinket[0]);
        weaponSets.Add(ItemList.instance.startingWeapon[6]);
        offHandSets.Add(ItemList.instance.startingOffHand[5]);
        SpecEquip();
    }
    public override void CoreStats()
    {
        base.CoreStats();
        maxHealth.baseValue = StatSpecs.instance.mageMaxHealth[specNumber];
        maxMana.baseValue = StatSpecs.instance.mageMaxMana[specNumber];
        defence.baseValue = StatSpecs.instance.mageDefence[specNumber];
        spellpower.baseValue = StatSpecs.instance.mageSpellpower[specNumber];
        damage.baseValue = StatSpecs.instance.mageDamage[specNumber];
        crit.baseValue = StatSpecs.instance.mageCrit[specNumber];
        manaRegenValue.baseValue = StatSpecs.instance.mageManaRegenValue[specNumber];
        manaRegenTime.baseValue =  StatSpecs.instance.mageManaRegenTime[specNumber];
    }
}