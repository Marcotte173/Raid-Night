using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Boss
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
        AbilityLoad(AbilityList.instance.bardInspiring);
    }
}
