using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : Ability
{
    public override void Effect()
    {
        int x = 1;
        CleaveStrike(target);
        List<Tile> tiles = new List<Tile> { };
        tiles.Add(target.move.currentTile);
        Character secondaryTarget = character.FindAdjacentEnemy(tiles, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion());
        if (secondaryTarget != null)
        {
            x++;
            CleaveStrike(secondaryTarget);
            Character tertiaryTarget = character.FindAdjacentEnemy(tiles, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion());
            if (tertiaryTarget != null)
            {
                x++;
                CleaveStrike(tertiaryTarget);
            }
        }
        if (AbilityCheck.instance.SetupCount(character) > 0) AbilityCheck.instance.MySetup(character).AddCount(x);
        else character.ability[5].GetComponent<Setup>().SetupBuff(x);
        character.state = DecisionState.Downtime;
    }
   

    private void CleaveStrike(Character target)
    {
        float dam = character.Damage() * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), true, "Cleave: ");
    }
}
