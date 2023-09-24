using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandingJob : Trait
{
    public override void Effect()
    {
        player.availableDays.Clear();
        player.availableDays.Add(6);
        player.availableDays.Add(7);
    }
}
