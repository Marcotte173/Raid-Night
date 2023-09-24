using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Boss
{
    public override void Decision()
    {
        if (ability[0].cooldownTimer <= 0)
        {
            state = DecisionState.Attack1;
        }
    }
    public override void AbilityPopulate()
    {
        ability[0].castTime = attackSpeed;
        ability[0].damage = damage.value;
    }
    public override void CoreStats()
    {
        base.CoreStats();
    }
}
