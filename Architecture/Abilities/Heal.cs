using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public float primaryThreshHold;
    public float partyThreshHold;
    public override void ComputerUse()
    {
        if (character.Mana() >= energyRequired)
        {
            base.ComputerUse();
        }
        else
        {
            character.action = $"Waiting for mana";
        }
    }
    public override bool ValidTarget() => !character.Enemy(character, base.target);
}
