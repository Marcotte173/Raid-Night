using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyodelOfTheMammothRiders : Boss
{
    public int phase;
    public List<Sprite> pic;
    public AminarisGiridak minion;
    public override void Equipment()
    {
        Equip.instance.Item(ItemList.instance.noItem, head);
        Equip.instance.Item(ItemList.instance.noItem, chest);
        Equip.instance.Item(ItemList.instance.noItem, legs);
        Equip.instance.Item(ItemList.instance.noItem, feet);
        Equip.instance.Item(ItemList.instance.noItem, trinket);
        Equip.instance.Item(ItemList.instance.noItem, weapon);
        Equip.instance.Item(ItemList.instance.noItem, offHand);
        phase = 0;
        GetComponent<SpriteRenderer>().sprite = pic[phase];
    }
    public override List<Ability> AbilityListReturn()
    {
        return AbilityList.instance.ayodel;
    }
    public override void Decision()
    {   
        if(phase == 0)
        {
            if(health < maxHealth.value / 2)
            {
                Debug.Log("Whoah");
                AyodelOfTheMammothRiderAbility a = (AyodelOfTheMammothRiderAbility)ability[1];
                a.SummonMinion();
            }
            else if(ability[1].cooldownTimer <= 0) state = DecisionState.Attack2;
            else if (ability[0].cooldownTimer <= 0) state = DecisionState.Attack1;
        }
        else if (ability[0].cooldownTimer <= 0) state = DecisionState.Attack1;
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
