using System;
using System.Collections.Generic;
using UnityEngine;

public class DruidDecision : MonoBehaviour
{
    public Druid druid;
    public Player player;
    public float skill;
    public int tree;
    public float blightThresholdSnakeBite;
    public float blightThresholdRot;
    void Start()
    {
        druid = GetComponent<Druid>();
        player = druid.player;
    }
    public void UpdateSkill()
    {
         skill = player.currentSkill * 10;
        skill = skill % 10;
        tree = Mathf.FloorToInt(player.currentSkill);
        druid.characterTree = tree;
    }

    public void Decide()
    {
        if (druid.canCastSpells)
        {
            if (druid.spec == Spec.Wrathful)
            {
                if (tree == 1) Wrathful1();
                else if (tree == 2) Wrathful2();
                else if (tree == 3) Wrathful3();
                else if (tree == 4) Wrathful4();
                else if (tree == 5) Wrathful5();
            }
            if (druid.spec == Spec.Redemptive)
            {
                if (tree == 1) Redemption1();
                else if (tree == 2) Redemption2();
                else if (tree == 3) Redemption3();
                else if (tree == 4) Redemption4();
                else if (tree == 5) Redemption5();
            }
        }
        else if (druid.ability[0].cooldownTimer <= 0)
        {
            druid.state = DecisionState.Attack1;
        }
    }

    private void Redemption1()
    {
        Redemption3();
    }

    private void Redemption2()
    {
        Redemption3();
    }

    private void Redemption3()
    {        
        Rebirth r = (Rebirth)druid.ability[4];
        Hibernation h = (Hibernation)druid.ability[3];
        Verdure v = (Verdure)druid.ability[2];
        NaturesMedicine n = (NaturesMedicine)druid.ability[1];
        //RESSURECT
        r.target = null;
        if (DungeonManager.instance.currentDungeon.currentEncounter.KOPlayer().Count > 0)
        {
            r.target = AbilityCheck.instance.FindRessurectTarget();
        }
        if (r.target != null && druid.Mana() >= r.energyRequired)
        {
            druid.state = DecisionState.Attack5;
        }
        /////////
        //HEALS//
        /////////
        //HIBERNATE TANK
        if (druid.primaryHealTarget.Health() / druid.primaryHealTarget.maxHealth.value < h.primaryThreshHold / 100 && h.cooldownTimer <= 0)
        {
            h.target = druid.primaryHealTarget;
            druid.state = DecisionState.Attack4;
        }
        //HIBERNATE PARTY
        else if (AbilityCheck.instance.SecondaryHealTarget(h.partyThreshHold, druid) != null && h.cooldownTimer <= 0)
        {
            h.target = AbilityCheck.instance.SecondaryHealTarget(h.partyThreshHold, druid);
            druid.state = DecisionState.Attack4;
        }
        //BIG HEAL TANK
        else if (druid.primaryHealTarget.Health() / druid.primaryHealTarget.maxHealth.value < v.primaryThreshHold / 100 && v.cooldownTimer <= 0)
        {
            v.target = druid.primaryHealTarget;
            druid.state = DecisionState.Attack3;
        }
        //BIG HEAL SELF
        else if (druid.Health() / druid.maxHealth.value < v.primaryThreshHold / 100)
        {
            v.target = druid;
            druid.state = DecisionState.Attack3;
        }
        //SMALL HEAL TANK
        else if (druid.primaryHealTarget.Health() / druid.primaryHealTarget.maxHealth.value < n.primaryThreshHold / 100 && n.cooldownTimer <= 0)
        {
            n.target = druid.primaryHealTarget;
            druid.state = DecisionState.Attack2;
        }
        //BIG HEAL PARTY
        else if (AbilityCheck.instance.SecondaryHealTarget(v.partyThreshHold, druid) != null && v.cooldownTimer <= 0)
        {
            v.target = AbilityCheck.instance.SecondaryHealTarget(v.partyThreshHold, druid);
            druid.state = DecisionState.Attack3;
        }
        //SMALL HEAL SELF
        else if (druid.Health() / druid.maxHealth.value < n.primaryThreshHold / 100 && n.cooldownTimer <= 0)
        {
            n.target = druid;
            druid.state = DecisionState.Attack2;
        }
        //SMALL HEAL PARTY
        else if (AbilityCheck.instance.SecondaryHealTarget(n.partyThreshHold, druid) != null && v.cooldownTimer <= 0)
        {
            n.target = AbilityCheck.instance.SecondaryHealTarget(n.partyThreshHold, druid);
            druid.state = DecisionState.Attack2;
        }
        //END HEALS
        //BASIC ATTACK
        else if (druid.ability[0].cooldownTimer <= 0)
        {
            druid.state = DecisionState.Attack1;
        }
    }

    private void Redemption4()
    {
        Redemption3();
    }

    private void Redemption5()
    {
        Redemption3();
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
        //NATURES ALLY
        if (druid.Mana() >= druid.ability[3].energyRequired && druid.ability[3].cooldownTimer <= 0)
        {
            druid.state = DecisionState.Attack4;
        }
        //BLIGHT
        else if (AbilityCheck.instance.MyRot(druid.target, druid) != null && AbilityCheck.instance.MySnakeBite(druid.target, druid) != null && druid.Mana() >= druid.ability[4].energyRequired && druid.ability[4].cooldownTimer <= 0 && (AbilityCheck.instance.MyRot(druid.target, druid).timer <= blightThresholdRot || AbilityCheck.instance.MySnakeBite(druid.target, druid).timer >= blightThresholdSnakeBite))
        {
            druid.state = DecisionState.Attack5;
        }
        //ROT
        else if (AbilityCheck.instance.RotCheck(druid.target, druid) && druid.Mana() >= druid.ability[1].energyRequired && druid.ability[1].cooldownTimer <= 0)
        {
            druid.state = DecisionState.Attack2;
        }
        //SNAKEBITE
        else if (AbilityCheck.instance.SnakeBiteCheck(druid.target, druid) && druid.Mana() >= druid.ability[2].energyRequired && druid.ability[2].cooldownTimer <= 0)
        {
            druid.state = DecisionState.Attack3;
        }
        else if (druid.ability[0].cooldownTimer <= 0)
        {
            druid.state = DecisionState.Attack1;
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
}