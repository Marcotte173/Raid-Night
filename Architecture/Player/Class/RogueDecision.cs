using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueDecision : MonoBehaviour
{
    public Rogue rogue;
    public Player player;
    public float skill;
    public int tree;
    void Start()
    {
        rogue = GetComponent<Rogue>();
        player = rogue.player;
    }

    public void UpdateSkill()
    {
        skill = player.currentSkill * 10;
        skill = skill % 10;
        tree = Mathf.FloorToInt(player.currentSkill);
        rogue.characterTree = tree;
    }
    public void Decide()
    {
        if (rogue.canCastSpells)
        {
            if (rogue.spec == Spec.Focused)
            {
                if (tree == 1) Focused1();
                else if (tree == 2) Focused2();
                else if (tree == 3) Focused3();
                else if (tree == 4) Focused4();
                else if (tree == 5) Focused5();
            }
            if (rogue.spec == Spec.Wrathful)
            {
                if (tree == 1) Wrathful1();
                else if (tree == 2) Wrathful2();
                else if (tree == 3) Wrathful3();
                else if (tree == 4) Wrathful4();
                else if (tree == 5) Wrathful5();
            }
        }
        else if (rogue.ability[0].cooldownTimer <= 0)
        {
            rogue.action = $"Moving";
            rogue.state = DecisionState.Attack1;
        }
    }
    private void Wrathful1()
    {
        Wrathful3();
    }
    private void Wrathful2()
    {
        Wrathful3();
    }
    private void Wrathful3()
    {
        if (rogue.Mana() >= rogue.ability[2].energyRequired && rogue.ability[2].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack3;
        }
        if (rogue.Mana() >= rogue.ability[1].energyRequired && rogue.ability[1].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack2;
        }
        else if (rogue.Mana() >= rogue.ability[3].energyRequired && rogue.ability[3].cooldownTimer <= 0 && AbilityCheck.instance.SetupCount(rogue) > 0)
        {
            rogue.state = DecisionState.Attack4;
        }
        else if (rogue.Mana() >= rogue.ability[4].energyRequired && rogue.ability[4].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack5;
        }
        else if (rogue.ability[0].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack1;
        }
    }
    private void Wrathful4()
    {
        Wrathful3();
    }
    private void Wrathful5()
    {
        Wrathful3();
    }

    private void Focused1()
    {
        int roll = UnityEngine.Random.Range(0, 4);
        if (roll == 0 && rogue.Mana() >= rogue.ability[3].energyRequired && rogue.ability[3].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack4;
        }
        else if (roll == 1 && rogue.Mana() >= rogue.ability[4].energyRequired && rogue.ability[4].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack5;
        }
        else if (roll == 2 && rogue.Mana() >= rogue.ability[1].energyRequired && rogue.ability[1].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack2;
        }
        else if (roll == 3 && rogue.Mana() > rogue.ability[2].energyRequired && rogue.ability[2].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack3;
        }
        else if (rogue.ability[0].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack1;
        }
    }

    private void Focused2()
    {
        if (rogue.Mana() >= rogue.ability[3].energyRequired && rogue.ability[3].cooldownTimer <= 0 && AbilityCheck.instance.SetupCount(rogue) > 4)
        {
            rogue.state = DecisionState.Attack4;
        }
        else if (rogue.Mana() >= rogue.ability[1].energyRequired && rogue.ability[1].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack2;
        }
        else if (rogue.ability[0].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack1;
        }
    }

    private void Focused3()
    {

        if (rogue.Mana() >= rogue.ability[2].energyRequired && rogue.ability[2].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack3;
        }
        else if (rogue.Mana() > rogue.ability[1].energyRequired && rogue.ability[1].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack2;
        }
        else if (rogue.Mana() >= rogue.ability[3].energyRequired && rogue.ability[3].cooldownTimer <= 0 && AbilityCheck.instance.SetupCount(rogue) > 0)
        {
            rogue.state = DecisionState.Attack4;
        }
        else if (rogue.Mana() >= rogue.ability[4].energyRequired && rogue.ability[4].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack5;
        }
        else if (rogue.ability[0].cooldownTimer <= 0)
        {
            rogue.state = DecisionState.Attack1;
        }
    }

    private void Focused4()
    {
        Focused3();
    }

    private void Focused5()
    {
        Focused3();
    }   
}