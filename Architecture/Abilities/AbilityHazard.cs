using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityHazard : Ability
{
    [HideInInspector]
    public Vector2 hazardPosition;
    public RuntimeAnimatorController hazardAnimation;
    public float width;
    public float length;
    [HideInInspector]
    public Animator outlineAnimator;
    [HideInInspector]
    public bool user;
    public bool castingHazard;

    public override void Effect()
    {
        if (UserControl.instance.control == Control.PlayerChoice)
        {
            hazardPosition = new Vector2(character.target.transform.position.x, character.target.transform.position.y);
            Trigger();
        }
    }
    public override void Cast()
    {
        if (UserControl.instance.control == Control.UserControl && UserControl.instance.selectedCharacter == character)
        {
            Animator a = Instantiate(GameObjectList.instance.outlineAnimator);
            outlineAnimator = a;
            outlineAnimator.GetComponent<Animator>().runtimeAnimatorController = hazardAnimation;
            outlineAnimator.transform.localScale = new Vector3(width, length);
            user = true;
            cast = false;
        }
        else base.Cast();
        
    }

    private void Trigger()
    {
        if (user)
        {            
            castingHazard = false;
            user = false;
            Destroy(outlineAnimator.gameObject);
        }
        TriggerHazard();        
    }

    public virtual void TriggerHazard()
    {
        
    }

    public override void UpdateStuff()
    {
        if (user)
        {
            if (castingHazard)
            {
                character.action = verb;
                character.castTimer -= UnityEngine.Time.deltaTime;
                if (character.castTimer <= 0f)
                {
                    Trigger();                    
                    character.state = DecisionState.Downtime;
                    cooldownTimer = character.CastTimer(cooldownTime);
                    character.mana -= energyRequired;
                }
            }
            else
            {
                Tile tile = null;
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList)
                {
                    if (t.x == Mathf.Round(pos.x) && t.y == Mathf.Round(pos.y))
                    {
                        tile = t;
                        outlineAnimator.transform.position = new Vector2(t.x, t.y);
                        break;
                    }
                }
                if (Input.GetMouseButtonUp(0) && tile != null)
                {
                    hazardPosition = new Vector2(tile.x, tile.y);
                    castingHazard = true;
                }
            }            
        }
    }
}