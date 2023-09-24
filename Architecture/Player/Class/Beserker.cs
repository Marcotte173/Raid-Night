using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class Beserker : Class
{
    public float energyGainOnHit;
    public float energyGainOnReceiveHit;
    public override void Create()
    {
        base.Create();
        FindSpec();
        Equipment();
        CoreStats();
    }
    public override void CoreStats()
    {
        base.CoreStats();
        maxHealth.baseValue = StatSpecs.instance.beserkerMaxHealth[specNumber];
        maxMana.baseValue = StatSpecs.instance.beserkerMaxMana[specNumber];
        defence.baseValue = StatSpecs.instance.beserkerDefence[specNumber];
        spellpower.baseValue = StatSpecs.instance.beserkerSpellpower[specNumber];
        damage.baseValue = StatSpecs.instance.beserkerDamage[specNumber];
        crit.baseValue = StatSpecs.instance.beserkerCrit[specNumber];
        manaRegenValue.baseValue = StatSpecs.instance.beserkerManaRegenValue[specNumber];
        manaRegenTime.baseValue = StatSpecs.instance.beserkerManaRegenTime[specNumber];
    }
    public override void FindSpec()
    {
        spec = (specNumber == 0) ?Spec.Stalwart:Spec.Focused;
    }
   
    public override void Decision()
    {
        GetComponent<BeserkerDecision>().Decide();
    }
    public override void AbilityPopulate()
    {       
        if (specNumber == 0) AbilityLoad(AbilityList.instance.beserkerStalwart);
        else AbilityLoad(AbilityList.instance.beserkerFocused);
    }
    public override void Equipment()
    {
        headSets.Add(ItemList.instance.startingHead[5]);
        headSets.Add(ItemList.instance.startingHead[3]);
        chestSets.Add(ItemList.instance.startingChest[5]);
        chestSets.Add(ItemList.instance.startingChest[3]);
        legSets.Add(ItemList.instance.startingLegs[5]);
        legSets.Add(ItemList.instance.startingLegs[3]);
        feetSets.Add(ItemList.instance.startingFeet[5]);
        feetSets.Add(ItemList.instance.startingFeet[3]);
        trinketSets.Add(ItemList.instance.startingTrinket[4]);
        trinketSets.Add(ItemList.instance.startingTrinket[1]);
        weaponSets.Add(ItemList.instance.startingWeapon[3]);
        weaponSets.Add(ItemList.instance.startingWeapon[4]);
        offHandSets.Add(ItemList.instance.startingOffHand[5]);
        offHandSets.Add(ItemList.instance.startingOffHand[5]);
        SpecEquip();
    }    
}