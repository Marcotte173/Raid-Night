using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBearer : Class
{
    public override void Create()
    {
        base.Create();
        FindSpec();
        Equipment();
        CoreStats();
    }
    public override void Decision()
    {
        GetComponent<ShieldBearerDecision>().Decide();
    }  
    public override void FindSpec()
    {
        spec = (specNumber == 0) ? Spec.Stalwart : Spec.Inspiring;
    }
    public override void AbilityPopulate()
    {
        base.AbilityPopulate();
        if (specNumber == 0) AbilityLoad(AbilityList.instance.shieldStalwart);
        else AbilityLoad(AbilityList.instance.shieldInspiring);
    }

    public override void Equipment()
    {
        headSets.Add(ItemList.instance.startingHead[5]);
        headSets.Add(ItemList.instance.startingHead[4]);
        chestSets.Add(ItemList.instance.startingChest[5]);
        chestSets.Add(ItemList.instance.startingChest[4]);
        legSets.Add(ItemList.instance.startingLegs[5]);
        legSets.Add(ItemList.instance.startingLegs[4]);
        feetSets.Add(ItemList.instance.startingFeet[5]);
        feetSets.Add(ItemList.instance.startingFeet[4]);
        trinketSets.Add(ItemList.instance.startingTrinket[4]);
        trinketSets.Add(ItemList.instance.startingTrinket[3]);
        weaponSets.Add(ItemList.instance.startingWeapon[0]);
        weaponSets.Add(ItemList.instance.startingWeapon[1]);
        offHandSets.Add(ItemList.instance.startingOffHand[3]);
        offHandSets.Add(ItemList.instance.startingOffHand[4]);
        SpecEquip();
    }
    public override void CoreStats()
    {
        base.CoreStats();
        maxHealth.baseValue = StatSpecs.instance.shieldBearerMaxHealth[specNumber];
        maxMana.baseValue = StatSpecs.instance.shieldBearerMaxMana[specNumber];
        defence.baseValue = StatSpecs.instance.shieldBearerDefence[specNumber];
        spellpower.baseValue = StatSpecs.instance.shieldBearerSpellpower[specNumber];
        damage.baseValue = StatSpecs.instance.shieldBearerDamage[specNumber];
        crit.baseValue = StatSpecs.instance.shieldBearerCrit[specNumber];
        manaRegenValue.baseValue = StatSpecs.instance.shieldBearerManaRegenValue[specNumber];
        manaRegenTime.baseValue  = StatSpecs.instance.shieldBearerManaRegenTime[specNumber];
    }
}