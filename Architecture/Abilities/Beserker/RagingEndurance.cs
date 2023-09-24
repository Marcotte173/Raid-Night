using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagingEndurance : Ability
{
    public bool mitigate;
    public float mitigationAmount;
    public override void Effect()
    {
        character.Heal(character.maxHealth.value * damage/100,false, character);
        mitigate = true;
        Utility.instance.DamageNumber(character, "Raging Endurance", SpriteList.instance.beserker);
    }
}
