using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Summon
{
    
    public override void Decision()
    {
        if (ability[0].cooldownTimer <= 0)
        {
            state = DecisionState.Attack1;
        }
    }
}
