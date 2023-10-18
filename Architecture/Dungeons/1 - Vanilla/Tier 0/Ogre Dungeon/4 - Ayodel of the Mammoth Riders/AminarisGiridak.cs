using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AminarisGiridak : Boss
{
    public override List<Ability> AbilityListReturn()
    {
        return AbilityList.instance.aminaris;
    }
    public override void Decision()
    {
        if (ability[1].cooldownTimer <= 0)
        {
            state = DecisionState.Attack2;
        }
        else if (ability[0].cooldownTimer <= 0)
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
