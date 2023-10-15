using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinOfAkalosHealAbility : Ability
{
    public override void Effect()
    {        
        target = character.target = Utility.instance.NeedsHeal(DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion());
        target.Heal(damage, false, character);
        Utility.instance.DamageNumber(character, $"Heal {target.characterName}", SpriteList.instance.bad);
        target = character.target = null;
    }
}
