using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBearerDecision : MonoBehaviour
{
    public ShieldBearer shieldBearer;
    public Player player;
    public float skill;
    public int tree;
    void Start()
    {
        shieldBearer = GetComponent<ShieldBearer>();
        player = shieldBearer.player;
    }
    public void UpdateSkill()
    {
        skill = player.currentSkill * 10;
        skill = skill % 10;
        tree = Mathf.FloorToInt(player.currentSkill);
        shieldBearer.characterTree = tree;
    }

    public void Decide()
    {
        if (shieldBearer.canCastSpells)
        {
            if (shieldBearer.spec == Spec.Inspiring)
            {
                if (tree == 1) Inspiring1();
                else if (tree == 2) Inspiring2();
                else if (tree == 3) Inspiring3();
                else if (tree == 4) Inspiring4();
                else if (tree == 5) Inspiring5();
            }
            if (shieldBearer.spec == Spec.Stalwart)
            {
                if (tree == 1) Stalwart1();
                else if (tree == 2) Stalwart2();
                else if (tree == 3) Stalwart3();
                else if (tree == 4) Stalwart4();
                else if (tree == 5) Stalwart5();
            }
        }
        else if (shieldBearer.ability[0].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack1;
        }
    }

    private void Inspiring1()
    {
        Inspiring3();    
    }

    private void Inspiring2()
    {
        Inspiring3();
    }

    private void Inspiring3()
    {
        Class c = ProtectionCheck();
        if (LastStandCheck() &&shieldBearer.Mana() > shieldBearer.ability[4].energyRequired && shieldBearer.ability[4].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack5;
        }
        else if (shieldBearer.Mana() > shieldBearer.ability[3].energyRequired && shieldBearer.ability[3].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack4;
        }        
        else if (AbilityCheck.instance.AmountOfActiveShields(DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion()) > 0 && shieldBearer.Mana() > shieldBearer.ability[2].energyRequired && shieldBearer.ability[2].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack3;
        }
        else if(c!=null && shieldBearer.Mana() > shieldBearer.ability[1].energyRequired && shieldBearer.ability[1].cooldownTimer <= 0)
        {
            shieldBearer.ability[1].target = shieldBearer.target = c;
            shieldBearer.state = DecisionState.Attack2;
        }
        else if (shieldBearer.ability[0].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack1;
        }
    }   

    private void Inspiring4()
    {
        Inspiring3();
    }

    private void Inspiring5()
    {
        Inspiring3();
    }

    private void Stalwart1()
    {
        Stalwart3();
    }

    private void Stalwart2()
    {
        Stalwart3();
    }

    private void Stalwart3()
    {
        //Taunt
        if (AbilityCheck.instance.LostAggro(shieldBearer.target, shieldBearer) && shieldBearer.ability[1].cooldownTimer <= 0 && shieldBearer.Mana() >= shieldBearer.ability[1].energyRequired)
        {
            shieldBearer.state = DecisionState.Attack2;
        }
        else if (shieldBearer.canCastSpells && shieldBearer.Mana() >= shieldBearer.ability[4].energyRequired && shieldBearer.ability[4].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack5;
        }
        else if (shieldBearer.canCastSpells && shieldBearer.Mana() >= shieldBearer.ability[3].energyRequired && shieldBearer.ability[3].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack4;
        }
        else if (shieldBearer.canCastSpells && shieldBearer.Mana() >= shieldBearer.ability[2].energyRequired && shieldBearer.ability[2].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack3;
        }
        else if (shieldBearer.ability[0].cooldownTimer <= 0)
        {
            shieldBearer.state = DecisionState.Attack1;
        }
    }

    private void Stalwart4()
    {
        Stalwart3();
    }

    private void Stalwart5()
    {
        Stalwart3();
    }

    private Class ProtectionCheck()
    {
        Encounter e = DungeonManager.instance.currentDungeon.currentEncounter;
        if (e.Boss().Count > 0)
        {
            Class c = (Class)e.boss[0].target;
            if (c.spec != Spec.Stalwart && c != shieldBearer) return c;
            if (c.health < c.maxHealth.value && c != shieldBearer) return c;
        }
        return null;
    }
    private bool LastStandCheck()
    {
        LastStand l = (LastStand)shieldBearer.ability[4];
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion())
        {
            if (a.health <= a.maxHealth.value * l.threshHoldToUse && Vector2.Distance(a.transform.position, transform.position) <=l.buffRange) return true;
        }
        return false;
    }
    
}
