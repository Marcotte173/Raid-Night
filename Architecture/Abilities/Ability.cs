using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [HideInInspector]
    public Character character;
    [HideInInspector]
    public Character target;
    public string abilityName;
    public string verb;
    public string flavor;
    public float rangeRequired;
    public float energyRequired;    
    public float cooldownTime;
    public float cooldownTimer;
    public float damage;
    public float threat;
    public float castTime;
    [HideInInspector]
    public float rangeToTarget;
    [HideInInspector]
    public bool cast;
    [HideInInspector]
    public bool dash;
    public bool passive;
    public bool mobile;
    public bool noTarget;
    public bool losRequired;
    public Sprite pic;
    public string abilityNameTwo;
    public Sprite picTwo;
    public string flavorTwo;

    private void Awake()
    {
        
    }

    public virtual void UpdateStuff()
    {
        
    }

    public virtual void Use()
    {
        //USER CONTROL
        if (UserControl.instance.control == Control.UserControl && UserControl.instance.controlledCharacter == character) UserUse();
        //COMPUTER CONTROL
        else ComputerUse();   
    }

    public virtual void UserUse()
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
                rangeToTarget = Vector3.Distance(character.transform.position, target.transform.position);
                if (InRange(rangeRequired))
                {
                    character.actionCast = character.castTimer = character.CastTimer(castTime);
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

    public virtual bool ValidTarget() => character.Enemy(character, target);

    public virtual void ComputerUse()
    {
        character.action = $"Moving";
        rangeToTarget = Vector3.Distance(character.transform.position, target.transform.position);
        if (InRange(rangeRequired))
        {
            character.actionCast = character.castTimer = character.CastTimer(castTime);
            cast = true;
            character.state = DecisionState.Cast;
        }
    }
    public virtual void Cast()
    {
        character.action = verb;
        character.castTimer -= UnityEngine.Time.deltaTime;
        if (character.castTimer <= 0f)
        {            
            cast = false;
            character.state = DecisionState.Downtime;
            cooldownTimer = character.CastTimer(cooldownTime);
            character.mana -= energyRequired;
            Effect();
        }
    }
    public virtual void Effect()
    {

    }
    public bool InRange(float range)
    {
        if(character.GetComponent<Boss>()) return CheckRange(range);
        else
        {
            //USER CONTROL
            if (UserControl.instance.control == Control.UserControl && UserControl.instance.controlledCharacter == character)
            {
                ///
                //WASD CONTROL SCHEME
                ///
                if (UserControl.instance.controlType == ControlType.WASD)
                {
                    if (rangeToTarget <= range)
                    {
                        if(LOS(target)) return true;
                        else
                        {
                            Utility.instance.DamageNumber(character.GetComponent<Class>(), "No Line of Sight", SpriteList.instance.bad);
                            character.state = DecisionState.Downtime;
                            return false;
                        }
                    }
                    else
                    {
                        Utility.instance.DamageNumber(character.GetComponent<Class>(), "Out Of Range", SpriteList.instance.bad);
                        character.state = DecisionState.Downtime;
                        return false;
                    }
                }
                ///
                //MOUSE CONTROL SCHEME
                ///
                else
                {
                    if (LOS(target)) return CheckRange(range);
                    else
                    {
                        Utility.instance.DamageNumber(character.GetComponent<Class>(), "No Line of Sight", SpriteList.instance.bad);
                        character.state = DecisionState.Downtime;
                        return false;
                    }
                }
            }
            //COMPUTER CONTROL
            else return CheckRange(range);
        }
    }

    private bool LOS(Character target)
    {
        return (character.move.currentTile.LOS(target.move.currentTile));
    }

    private bool CheckRange(float range)
    {
        if (rangeToTarget <= range && !character.move.unwalkable.Contains(character.move.currentTile))
        {
            if (MoveManager.instance.IsAtTile(character.move, character.move.currentTile))
            {
                character.move.isMoving = false;
                return true;
            }
            else
            {
                MoveManager.instance.Move(character.move);
            }
        }
        else MoveManager.instance.MoveAgent(character.move);
        return false;
    }

    public virtual void StartBattle()
    {

    }
    public virtual void EndBattle()
    {

    }
    public virtual void DashTrigger()
    {

    }
}
