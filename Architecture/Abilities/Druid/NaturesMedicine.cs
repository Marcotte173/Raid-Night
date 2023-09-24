using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturesMedicine : Heal
{
    public override void Effect()
    {
        Utility.instance.DamageNumber(character, "Nature's Medicine", SpriteList.instance.druid);
        target.Heal(damage * character.spellpower.value / 100,true, character);        
    }    
}
