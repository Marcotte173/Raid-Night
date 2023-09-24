using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiftwork : Trait
{
    public override void Effect()
    {
        player.GetAvailabilty();
    }
}
