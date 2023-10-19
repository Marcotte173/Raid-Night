using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel : Ability
{
    public int check;
    public List<float> tick;
    public override void UserUse()
    {
        if (noTarget)
        {
            character.actionCast = character.castTimer = character.CastTimer(castTime);
            cast = true;
            character.state = DecisionState.Cast;
        }
        else if (UserControl.instance.controlledCharacter.target != null)
        {
            target = UserControl.instance.controlledCharacter.target;
            if (ValidTarget())
            {
                rangeToTarget = FindTile.instance.Distance(character.transform.position, target.transform.position);
                if (InRange(rangeRequired))
                {
                    character.action = verb;
                    character.actionCast = character.castTimer = character.CastTimer(castTime);
                    character.mana -= energyRequired;
                    cooldownTimer = cooldownTime;
                    cast = true;
                    character.state = DecisionState.Cast;
                }
            }
            else
            {
                character.state = DecisionState.Downtime;
                Utility.instance.DamageNumber(character, "Not a valid Target", SpriteList.instance.bad);
            }
        }
        else
        {
            character.state = DecisionState.Downtime;
            Utility.instance.DamageNumber(character, "No Target", SpriteList.instance.bad);
        }
    }
    public override void ComputerUse()
    {
        rangeToTarget = FindTile.instance.Distance(character.transform.position, target.transform.position);
        if (InRange(rangeRequired))
        {
            character.action = verb;
            character.actionCast = character.castTimer = character.CastTimer(castTime);
            character.mana -= energyRequired;
            cooldownTimer = cooldownTime;
            cast = true;
            character.state = DecisionState.Cast;
        }
    }
    public override void Cast()
    {
        character.castTimer -= UnityEngine.Time.deltaTime;
        if (character.castTimer <= 0f)
        {
            check = 0;
            cast = false;
            character.state = DecisionState.Downtime;
        }
        else if (character.castTimer <= tick[check])
        {
            Projectile();
            check++;
        }
    }

    public virtual void Projectile()
    {
        
    }
}
