using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebirth : Heal
{
    public override void Effect()
    {
        Character resTarget = target;        
        target.ko = false;
        resTarget.canCastSpells = true;
        resTarget.SpriteOn();
        Tile t = FindTile.instance.FindClosestUnoccupiedTileAdjacentToTarget(target.move.currentTile, character.move.currentTile, null);
        resTarget.move.transform.position = resTarget.move.prevPosition = t.transform.position;
        resTarget.health = Mathf.Round(resTarget.maxHealth.value * .2f);
        resTarget.mana = Mathf.Round(resTarget.maxMana.value * .2f);
        Utility.instance.DamageNumber(resTarget, "Nature's Medicine", SpriteList.instance.druid);
    }
    public override void UserUse()
    {
        if (DungeonManager.instance.currentDungeon.currentEncounter.KOPlayer().Count > 0)
        {
            if (UserControl.instance.controlledCharacter.target != null || noTarget)
            {
                target = UserControl.instance.controlledCharacter.target;
                if (ValidTarget() && DungeonManager.instance.currentDungeon.currentEncounter.KOPlayer().Contains(target))
                {
                    Use2();
                }
                else
                {
                    character.state = DecisionState.Downtime;
                    Utility.instance.DamageNumber(character, "Not a valid Target", SpriteList.instance.bad);
                }
            }
            else
            {
                target = DungeonManager.instance.currentDungeon.currentEncounter.KOPlayer()[0];
                Use2();
            }
        }
        else
        {
            character.state = DecisionState.Downtime;
            Utility.instance.DamageNumber(character, "No one is KOd", SpriteList.instance.bad);
        }
    }

    public void Use2()
    {
        rangeToTarget = Vector3.Distance(character.transform.position, target.transform.position);
        if (InRange(rangeRequired))
        {
            character.actionCast = character.castTimer = character.CastTimer(castTime);
            cast = true;
            character.state = DecisionState.Cast;
        }
    }
}
