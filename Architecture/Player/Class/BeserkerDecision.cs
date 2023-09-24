using System;
using System.Collections.Generic;
using UnityEngine;

public class BeserkerDecision : MonoBehaviour
{
    public Beserker beserker;
    public Player player;
    public float skill;
    public int tree;
    void Start()
    {
        beserker = GetComponent<Beserker>();
        player = beserker.player;
    }
    public void UpdateSkill()
    {
        skill = player.currentSkill * 10;
        skill = skill % 10;
        tree = Mathf.FloorToInt(player.currentSkill);
        beserker.characterTree = tree;
    }

    public void Decide()
    {
        if (beserker.canCastSpells)
        {            
            if (beserker.spec == Spec.Stalwart)
            {
                if (tree == 1) Stalwart1();
                else if (tree == 2) Stalwart2();
                else if (tree == 3) Stalwart3();
                else if (tree == 4) Stalwart4();
                else if (tree == 5) Stalwart5();
            }
            if (beserker.spec == Spec.Focused)
            {
                if (Utility.instance.AggroCheck(beserker))
                {
                    if (tree == 1) Focused1();
                    else if (tree == 2) Focused2();
                    else if (tree == 3) Focused3();
                    else if (tree == 4) Focused4();
                    else if (tree == 5) Focused5();
                }
            }
        }
        else if (beserker.ability[0].cooldownTimer <= 0) beserker.state = DecisionState.Attack1;
    }
    //  1*   Read spell then pick a random spell
    public void Stalwart1()
    {
        int roll = UnityEngine.Random.Range(0, 4);
        if (roll == 0 && beserker.Mana() >= beserker.ability[3].energyRequired && beserker.ability[3].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack4;
        }
        else if (roll == 1 && beserker.Mana() >= beserker.ability[4].energyRequired && beserker.ability[4].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack5;
        }
        else if (roll == 2 && beserker.Mana() >= beserker.ability[1].energyRequired && beserker.ability[1].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack2;
        }
        else if (roll == 3 && beserker.Mana() > beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }

    //  2* Switch Between Reckless Swing and Too Angry to Die
    public void Stalwart2()
    {
        TooAngryToDie t = (TooAngryToDie)beserker.ability[4];
        //Too Angry To die
        if (beserker.Health() / beserker.maxHealth.value < t.threshHold && beserker.ability[4].cooldownTimer <= 0 && beserker.Mana() >= beserker.ability[4].energyRequired)
        {
            beserker.state = DecisionState.Attack4;
        }
        //RECKLESS SWING
        else if (beserker.InRange(beserker, beserker.ability[2].rangeRequired, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion()).Count > 1 && beserker.Mana() >= beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        //Basic attack
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }
    //  3*  Taunt anyone without Aggro, Otherwise, use skills as available
    public void Stalwart3()
    {
        if (beserker.ability[1].cooldownTimer <= 0 && beserker.Mana() >= beserker.ability[1].energyRequired)
        {
            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion())
            {
                if (AbilityCheck.instance.LostAggro(a, beserker))
                {
                    beserker.target = a;
                    beserker.state = DecisionState.Attack2;
                }
            }
        }
        TooAngryToDie t = (TooAngryToDie)beserker.ability[4];
        //Too Angry To die
        if (beserker.ability[4].cooldownTimer <= 0 && beserker.Mana() >= beserker.ability[4].energyRequired)
        {
            beserker.state = DecisionState.Attack4;
        }
        //RECKLESS SWING
        else if (beserker.InRange(beserker, beserker.ability[2].rangeRequired, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion()).Count > 1 && beserker.Mana() >= beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        //Basic attack
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }

    //  4*  Taunt someone without Aggro, Then Make sure you hit everyone, Uses skills as necessary
    public void Stalwart4()
    {
        TooAngryToDie t = (TooAngryToDie)beserker.ability[4];
        //Do you have the MAIN boss aggro
        bool bossAggro = !AbilityCheck.instance.LostAggro(DungeonManager.instance.currentDungeon.currentEncounter.Boss()[0], beserker);
        //If you don't have MAIN aggro, that is your target
        if (!bossAggro)
        {
            beserker.target = DungeonManager.instance.currentDungeon.currentEncounter.Boss()[0];
            if (beserker.ability[1].cooldownTimer <= 0 && beserker.Mana() >= beserker.ability[1].energyRequired)
            {
                beserker.state = DecisionState.Attack2;
            }
        }
        //Otherwise, get people who don't have aggro
        else
        {
            //If Taunt is up
            if (beserker.ability[1].cooldownTimer <= 0 && beserker.Mana() >= beserker.ability[1].energyRequired)
            {
                foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion())
                {
                    if (AbilityCheck.instance.LostAggro(a, beserker))
                    {
                        beserker.target = a;
                        beserker.state = DecisionState.Attack2;
                    }
                }
            }
            else
            {

            }
        }        
        //If you are below 30% and can cast it, cast Too Angry To die
        if (beserker.Health() / beserker.maxHealth.value < t.threshHold && beserker.ability[4].cooldownTimer <= 0 && beserker.Mana() >= beserker.ability[4].energyRequired)
        {
            beserker.state = DecisionState.Attack4;
        }
        //RECKLESS SWING
        else if (beserker.InRange(beserker, beserker.ability[2].rangeRequired, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion()).Count > 1 && beserker.Mana() >= beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        //Basic attack
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }
    public void Stalwart5()
    {
        if (beserker.ability[1].cooldownTimer <= 0 && beserker.Mana() >= beserker.ability[1].energyRequired)
        {
            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion())
            {
                if (AbilityCheck.instance.LostAggro(a, beserker))
                {
                    beserker.target = a;
                    beserker.state = DecisionState.Attack2;
                }
            }
        }
        TooAngryToDie t = (TooAngryToDie)beserker.ability[4];
        //If you are below 50% and can cast it, cast Too Angry To die
        if (beserker.Health() / beserker.maxHealth.value < t.threshHold && beserker.ability[4].cooldownTimer <= 0 && beserker.Mana() >= beserker.ability[4].energyRequired)
        {
            beserker.state = DecisionState.Attack4;
        }
        //Check for Reckless Swing Mana and Cooldown
        else if(beserker.Mana() >= beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            //If there is only 1 badguy, Reckless Swing
            if(DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion().Count == 1)
            {
                beserker.state = DecisionState.Attack3;
            }
            //If in Proper positioning, Reckless Swing
            else if (beserker.InRange(beserker, beserker.ability[2].rangeRequired, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion()).Count > 1)
            {
                beserker.state = DecisionState.Attack3;
            }
            //If NOT in proper positioning, Move to proper positioning
            else
            {
                Tile tileRun = FindTile.instance.FindTileBesideTargetAlsoInRangeOfOtherTargets(beserker.target.move.currentTile);
                if (tileRun != null)
                {
                    beserker.Run(tileRun);
                }
            }
        }
        //Basic attack
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }
    public void Focused1()
    {
        int roll = UnityEngine.Random.Range(0, 4);
        if (roll == 0 && beserker.Mana() >= beserker.ability[3].energyRequired && beserker.ability[3].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack4;
        }
        else if (roll == 1 && beserker.Mana() >= beserker.ability[4].energyRequired && beserker.ability[4].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack5;
        }
        else if (roll == 2 && beserker.Mana() >= beserker.ability[1].energyRequired && beserker.ability[1].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack2;
        }
        else if (roll == 3 && beserker.Mana() > beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }
    public void Focused2()
    {
        if (beserker.Mana() >= beserker.ability[1].energyRequired && beserker.ability[1].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack2;
        }
        else if (beserker.Mana() > beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }
    public void Focused3()
    {
        //WARCRY
        if (beserker.Mana() >= beserker.ability[4].energyRequired && beserker.ability[4].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack4;
        }
        //RECKLESS SWING
        else if (beserker.InRange(beserker, beserker.ability[2].rangeRequired, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion()).Count > 1 && beserker.Mana() >= beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack2;
        }
        //SAVAGE BLOW
        else if (beserker.Mana() >= beserker.ability[3].energyRequired && beserker.ability[4].cooldownTimer > 0 && beserker.ability[3].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack5;
        }
        //SLAM
        else if (beserker.Mana() >= beserker.ability[1].energyRequired && beserker.ability[1].cooldownTimer <= 0 && beserker.ability[4].cooldownTimer > 0 && beserker.ability[3].cooldownTimer > 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        //Basic attack
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }
    public void Focused4()
    { 
        //WARCRY
        if (beserker.Mana() >= beserker.ability[4].energyRequired && beserker.ability[4].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack4;
        }
        //SAVAGE BLOW
        else if (AbilityCheck.instance.SavageBlowDotCheck(beserker.target).timeRemaining<=2 && beserker.Mana() >= beserker.ability[3].energyRequired && beserker.ability[4].cooldownTimer > 0 && beserker.ability[3].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack5;
        }
        //RECKLESS SWING
        else if (beserker.InRange(beserker, beserker.ability[2].rangeRequired, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion()).Count > 1 && beserker.Mana() >= beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack2;
        }        
        //SLAM
        else if (beserker.Mana() >= beserker.ability[1].energyRequired && beserker.ability[1].cooldownTimer <= 0 && beserker.ability[4].cooldownTimer > 0 && beserker.ability[3].cooldownTimer > 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        //Basic attack
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }

    

    public void Focused5()
    {
        //WARCRY
        if (beserker.Mana() >= beserker.ability[4].energyRequired && beserker.ability[4].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack4;
        }
        //SAVAGE BLOW
        else if ((AbilityCheck.instance.SavageBlowDotCheck(beserker.target).timeRemaining <= 2 && beserker.Mana() >= beserker.ability[3].energyRequired && beserker.ability[4].cooldownTimer > 0 && beserker.ability[3].cooldownTimer <= 0)|| (beserker.ability[4].cooldownTimer > 0 && beserker.ability[3].cooldownTimer <= 0&&beserker.ability[1].cooldownTimer > 0 && beserker.ability[2].cooldownTimer > 0))
        {
            beserker.state = DecisionState.Attack5;
        }
        //RECKLESS SWING
        else if (beserker.InRange(beserker, beserker.ability[2].rangeRequired, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion()).Count > 1 && beserker.Mana() >= beserker.ability[2].energyRequired && beserker.ability[2].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack2;
        }
        //SLAM
        else if (beserker.Mana() >= beserker.ability[1].energyRequired && beserker.ability[1].cooldownTimer <= 0 && beserker.ability[4].cooldownTimer > 0 && beserker.ability[3].cooldownTimer > 0)
        {
            beserker.state = DecisionState.Attack3;
        }
        //Basic attack
        else if (beserker.ability[0].cooldownTimer <= 0)
        {
            beserker.state = DecisionState.Attack1;
        }
    }
    
}
