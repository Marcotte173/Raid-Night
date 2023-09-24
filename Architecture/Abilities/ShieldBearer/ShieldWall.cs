using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWall : Ability
{
    public float shieldWallTime;
    public override void Effect()
    {
        AbilityCheck.instance.Shield(character, character, damage, shieldWallTime);
    }
}
