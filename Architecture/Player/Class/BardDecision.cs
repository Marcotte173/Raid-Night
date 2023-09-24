using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BardDecision : MonoBehaviour
{
    public Bard bard;
    public Player player;
    public float skill;
    public int tree;
    void Start()
    {
        bard = GetComponent<Bard>();
        player = bard.player;        
    }

    public void UpdateSkill()
    {
        skill = player.currentSkill * 10;
        skill = skill % 10;
        tree = Mathf.FloorToInt(player.currentSkill);
        bard.characterTree = tree;
    }

    public void Decide()
    {
        if (bard.canCastSpells)
        {
            if (bard.spec == Spec.Inspiring)
            {
                if (tree == 1) Inspire1();
                else if (tree == 2) Inspire2();
                else if (tree == 3) Inspire3();
                else if (tree == 4) Inspire4();
                else if (tree == 5) Inspire5();
            }
            if (bard.spec == Spec.Tranquil)
            {
                if (tree == 1) Tranquil1();
                else if (tree == 2) Tranquil2();
                else if (tree == 3) Tranquil3();
                else if (tree == 4) Tranquil4();
                else if (tree == 5) Tranquil5();
            }
        }
        else if (bard.ability[0].cooldownTimer <= 0)
        {
            bard.state = DecisionState.Attack1;
        }     
    }

    

    public void Inspire1()
    {
        Inspire3();
    }
    public void Inspire2()
    {
        Inspire3();
    }
    public void Inspire3()
    {
        //FINALE TANK
        if (bard.Mana() > bard.ability[4].energyRequired && bard.ability[4].cooldownTimer <= 0)
        {
            bard.ability[4].target = bard.target = FindFinaleTarget();
            bard.state = DecisionState.Attack5;
        }
        else if (bard.Mana() > bard.ability[3].energyRequired && bard.ability[3].cooldownTimer <= 0)
        {
            bard.ability[3].target = bard.target = FindCrescendoTarget();
            bard.state = DecisionState.Attack4;
        }
        else if (bard.Mana() > bard.ability[1].energyRequired && bard.ability[1].cooldownTimer <= 0)
        {
            bard.ability[1].target = bard.target = FindInspiringTuneTarget();
            bard.state = DecisionState.Attack2;
        }
        else if (bard.Mana() > bard.ability[2].energyRequired && bard.ability[2].cooldownTimer <= 0)
        {
            bard.ability[2].target = bard.target = FindInTuneTarget();
            bard.state = DecisionState.Attack3;
        }
        else if (bard.ability[0].cooldownTimer <= 0)
        {
            bard.state = DecisionState.Attack1;
        }
    }
    public void Inspire4()
    {
        Inspire3();
    }
    public void Inspire5()
    {
        Inspire3();
    }
    public void Tranquil1()
    {
        Tranquil3();
    }
    public void Tranquil2()
    {
        Tranquil3();
    }
    public void Tranquil3()
    {
        Finale f = (Finale)bard.ability[4];
        DirgeOfDemise d = (DirgeOfDemise)bard.ability[3];
        Uplift u = (Uplift)bard.ability[2];
        SoothingMelody s = (SoothingMelody)bard.ability[1];
        ////FINALE TANK
        if (bard.primaryHealTarget.Health() / bard.primaryHealTarget.maxHealth.value < f.primaryThreshHold / 100 && f.cooldownTimer <= 0)
        {
            f.target = bard.primaryHealTarget;
            bard.state = DecisionState.Attack5;
        }
        //FINALE PARTY
        else if (AbilityCheck.instance.SecondaryHealTarget(f.partyThreshHold, bard) != null && f.cooldownTimer <= 0)
        {
            f.target = AbilityCheck.instance.SecondaryHealTarget(f.partyThreshHold, bard);
            bard.state = DecisionState.Attack4;
        }
        //DIRGE OF DEMISE TANK
        else if (bard.primaryHealTarget.Health() / bard.primaryHealTarget.maxHealth.value < d.primaryThreshHold / 100 && d.cooldownTimer <= 0)
        {
            d.target = bard.primaryHealTarget;
            bard.state = DecisionState.Attack4;
        }
        //DIRGE OF DEMISE SELF
        else if (bard.Health() / bard.maxHealth.value < d.primaryThreshHold / 100 && d.cooldownTimer <= 0)
        {
            d.target = bard;
            bard.state = DecisionState.Attack4;
        }
        //UPLIFT TANK
        else if (bard.primaryHealTarget.Health() / bard.primaryHealTarget.maxHealth.value < u.primaryThreshHold / 100 && u.cooldownTimer <= 0)
        {
            u.target = bard.primaryHealTarget;
            bard.state = DecisionState.Attack3;
        }
        //UPLIFT SELF
        else if (bard.Health() / bard.maxHealth.value < u.primaryThreshHold / 100 && u.cooldownTimer <= 0)
        {
            u.target = bard;
            bard.state = DecisionState.Attack3;
        }
        //UPLIFT PARTY
        else if (bard.primaryHealTarget.Health() / bard.primaryHealTarget.maxHealth.value < u.partyThreshHold / 100 && u.cooldownTimer <= 0)
        {
            u.target = AbilityCheck.instance.SecondaryHealTarget(u.partyThreshHold, bard);
            bard.state = DecisionState.Attack3;
        }
        //SOOTHING MELODY TANK
        else if (bard.primaryHealTarget.Health() / bard.primaryHealTarget.maxHealth.value < s.primaryThreshHold / 100 && s.cooldownTimer <= 0)
        {
            s.target = bard.primaryHealTarget;
            bard.state = DecisionState.Attack2;
        }
        //DIRGE OF DEMISE PARTY
        else if (AbilityCheck.instance.SecondaryHealTarget(d.partyThreshHold, bard) != null && d.cooldownTimer <= 0)
        {
            d.target = AbilityCheck.instance.SecondaryHealTarget(d.partyThreshHold, bard);
            bard.state = DecisionState.Attack4;
        }
        //SOOTHING MELODY  SELF
        else if (bard.Health() / bard.maxHealth.value < s.primaryThreshHold / 100 && s.cooldownTimer <= 0)
        {
            s.target = bard;
            bard.state = DecisionState.Attack2;
        }
        //SOOTHING MELODY  PARTY
        else if (AbilityCheck.instance.SecondaryHealTarget(s.partyThreshHold, bard) != null && s.cooldownTimer <= 0)
        {
            s.target = AbilityCheck.instance.SecondaryHealTarget(s.partyThreshHold, bard);
            bard.state = DecisionState.Attack2;
        }
        //END HEALS
        //BASIC ATTACK
        else if (bard.ability[0].cooldownTimer <= 0)
        {
            bard.state = DecisionState.Attack1;
        }
    }
    public void Tranquil4()
    {

        Tranquil3();
    }
    public void Tranquil5()
    {
        Tranquil3();
    }

    /// 
    /// Find Buff Targets
    /// 
    /// 

    private Character FindCrescendoTarget()
    {
        List<Character> agents = DungeonManager.instance.currentDungeon.currentEncounter.Player();
        Character x = DungeonManager.instance.currentDungeon.currentEncounter.Player()[0];
        for (int i = 1; i < agents.Count; i++)
        {
            if (DungeonManager.instance.currentDungeon.currentEncounter.Player()[i].damage.value > x.damage.value)
            {
                x = DungeonManager.instance.currentDungeon.currentEncounter.Player()[i];
            }
        }
        return x;
    }

    private Character FindInspiringTuneTarget()
    {
        List<Character> agents = DungeonManager.instance.currentDungeon.currentEncounter.Player();
        Character x = DungeonManager.instance.currentDungeon.currentEncounter.Player()[0];
        for (int i = 1; i < agents.Count; i++)
        {
            if (DungeonManager.instance.currentDungeon.currentEncounter.Player()[i].maxHealth.value > x.maxHealth.value)
            {
                x = DungeonManager.instance.currentDungeon.currentEncounter.Player()[i];
            }
        }
        return x;
    }

    private Character FindInTuneTarget()
    {
        List<Character> agents = DungeonManager.instance.currentDungeon.currentEncounter.Player();
        Character x = DungeonManager.instance.currentDungeon.currentEncounter.Player()[0];
        for (int i = 1; i < agents.Count; i++)
        {
            if (DungeonManager.instance.currentDungeon.currentEncounter.Player()[i].maxMana.value > x.maxMana.value)
            {
                x = DungeonManager.instance.currentDungeon.currentEncounter.Player()[i];
            }
        }
        return x;
    }

    private Character FindFinaleTarget()
    {
        List<Character> agents = DungeonManager.instance.currentDungeon.currentEncounter.Player();
        Character x = DungeonManager.instance.currentDungeon.currentEncounter.Player()[0];
        for (int i = 1; i < agents.Count; i++)
        {
            if (AbilityCheck.instance.TotalHotBuffs(DungeonManager.instance.currentDungeon.currentEncounter.Player()[i]) > AbilityCheck.instance.TotalHotBuffs(x))
            {
                x = DungeonManager.instance.currentDungeon.currentEncounter.Player()[i];
            }
        }
        return x;
    }
    
}
