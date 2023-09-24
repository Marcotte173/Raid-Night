using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public class Bard : Class
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
        if (primaryHealTarget == null) GetPrimaryHealTarget();
    }
    public override void Decision()
    {
       GetComponent<BardDecision>().Decide();
    }    

    public override void FindSpec()
    {
        spec = (specNumber == 0) ? Spec.Tranquil : Spec.Inspiring;
    }
    

    public override void Equipment()
    {
        headSets.Add(ItemList.instance.startingHead[2]);
        headSets.Add(ItemList.instance.startingHead[2]);
        chestSets.Add(ItemList.instance.startingChest[2]);
        chestSets.Add(ItemList.instance.startingChest[2]);
        legSets.Add(ItemList.instance.startingLegs[2]);
        legSets.Add(ItemList.instance.startingLegs[2]);
        feetSets.Add(ItemList.instance.startingFeet[2]);
        feetSets.Add(ItemList.instance.startingFeet[2]);
        trinketSets.Add(ItemList.instance.startingTrinket[2]);
        trinketSets.Add(ItemList.instance.startingTrinket[2]);
        weaponSets.Add(ItemList.instance.startingWeapon[7]);
        weaponSets.Add(ItemList.instance.startingWeapon[5]);
        offHandSets.Add(ItemList.instance.startingOffHand[0]);
        offHandSets.Add(ItemList.instance.startingOffHand[5]);
        SpecEquip();
    }
    public override void AbilityPopulate()
    {
        if (specNumber == 0) AbilityLoad(AbilityList.instance.bardTranquil);
        else AbilityLoad(AbilityList.instance.bardInspiring);
    }
    public override void CoreStats()
    {
        base.CoreStats();
        maxHealth.baseValue = StatSpecs.instance.bardMaxHealth[specNumber];
        maxMana.baseValue = StatSpecs.instance.bardMaxMana[specNumber];
        defence.baseValue = StatSpecs.instance.bardDefence[specNumber];
        spellpower.baseValue = StatSpecs.instance.bardSpellpower[specNumber];
        damage.baseValue = StatSpecs.instance.bardDamage[specNumber];
        crit.baseValue = StatSpecs.instance.bardCrit[specNumber];
        manaRegenValue.baseValue = StatSpecs.instance.bardManaRegenValue[specNumber];
        manaRegenTime.baseValue = StatSpecs.instance.bardManaRegenTime[specNumber];
    }
}