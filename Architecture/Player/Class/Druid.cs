using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Druid : Class
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
        GetComponent<DruidDecision>().Decide();
    }    

    public override void FindSpec()
    {
        spec = (specNumber == 0) ? Spec.Wrathful : Spec.Redemptive;
    }
    public override void AbilityPopulate()
    {
        if (specNumber == 0) AbilityLoad(AbilityList.instance.druidWrath);
        else AbilityLoad(AbilityList.instance.druidRedemptive);
    }

    public override void Equipment()
    {
        headSets.Add(ItemList.instance.startingHead[0]);
        chestSets.Add(ItemList.instance.startingChest[0]);
        legSets.Add(ItemList.instance.startingLegs[0]);
        feetSets.Add(ItemList.instance.startingFeet[0]);
        trinketSets.Add(ItemList.instance.startingTrinket[0]);
        trinketSets.Add(ItemList.instance.startingTrinket[2]);
        weaponSets.Add(ItemList.instance.startingWeapon[6]);
        weaponSets.Add(ItemList.instance.startingWeapon[7]);
        offHandSets.Add(ItemList.instance.startingOffHand[5]);
        offHandSets.Add(ItemList.instance.startingOffHand[1]);
        SpecEquip();
    }
    public override void CoreStats()
    {
        base.CoreStats();
        maxHealth.baseValue = StatSpecs.instance.druidMaxHealth[specNumber];
        maxMana.baseValue = StatSpecs.instance.druidMaxMana[specNumber];
        defence.baseValue = StatSpecs.instance.druidDefence[specNumber];
        spellpower.baseValue = StatSpecs.instance.druidSpellpower[specNumber];
        damage.baseValue = StatSpecs.instance.druidDamage[specNumber];
        crit.baseValue = StatSpecs.instance.druidCrit[specNumber];
        manaRegenValue.baseValue = StatSpecs.instance.druidManaRegenValue[specNumber];
        manaRegenTime.baseValue = StatSpecs.instance.druidManaRegenTime[specNumber];
    }    
}