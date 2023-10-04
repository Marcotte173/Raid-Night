using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KwasiOfTheAllSeeingEye : Boss
{
    public override void Equipment()
    {
        Equip.instance.Item(ItemList.instance.noItem, head);
        Equip.instance.Item(ItemList.instance.noItem, chest);
        Equip.instance.Item(ItemList.instance.noItem, legs);
        Equip.instance.Item(ItemList.instance.noItem, feet);
        Equip.instance.Item(ItemList.instance.noItem, trinket);
        Equip.instance.Item(ItemList.instance.noItem, weapon);
        Equip.instance.Item(ItemList.instance.noItem, offHand);
    }
    public override List<Ability> AbilityListReturn()
    {
        return AbilityList.instance.kwasi;
    }
    public override void Decision()
    {
        if (ability[0].cooldownTimer <= 0)
        {
            state = DecisionState.Attack1;
        }
    }
    public override void AbilityPopulate()
    {
        AbilityLoad(AbilityListReturn());
        ability[0].castTime = attackSpeed;
        ability[0].damage = damage.value;
    }
    public override void CoreStats()
    {
        base.CoreStats();
    }
}
