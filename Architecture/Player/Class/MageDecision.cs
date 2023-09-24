using System;
using System.Collections.Generic;
using UnityEngine;

public class MageDecision : MonoBehaviour
{
    public Mage mage;
    public Player player;
    public float skill;
    public int tree;
    public int preferredSpell;
    public bool burnPhase;
    public float burnPhaseThreshhold;
    void Start()
    {
        mage = GetComponent<Mage>();
        player = mage.player;
    }
    public void UpdateSkill()
    {
        skill = player.currentSkill * 10;
        skill = skill % 10;        
        tree = Mathf.FloorToInt(player.currentSkill);
        mage.characterTree = tree;
    }
    public void Decide()
    {
        if (mage.canCastSpells)
        {
            if (Utility.instance.AggroCheck(mage))
            {
                if (mage.spec == Spec.Focused)
                {
                    if (tree == 1) Focused1();
                    else if (tree == 2) Focused2();
                    else if (tree == 3) Focused3();
                    else if (tree == 4) Focused4();
                    else if (tree == 5) Focused5();
                }
                if (mage.spec == Spec.Explosive)
                {
                    if (tree == 1) Explosive1();
                    else if (tree == 2) Explosive2();
                    else if (tree == 3) Explosive3();
                    else if (tree == 4) Explosive4();
                    else if (tree == 5) Explosive5();
                }
            }            
        }
        else if (mage.ability[0].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack1;
        }
    }
    public void Explosive1()
    {
        int roll = UnityEngine.Random.Range(0, 4);
        if (roll == 0 && mage.Mana() >= mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack4;
        }
        else if (roll == 1 && mage.Mana() >= mage.ability[4].energyRequired && mage.ability[4].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack5;
        }
        else if (roll == 2 && mage.Mana() >= mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack2;
        }
        else if (roll == 3 && mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack3;
        }
        else if (mage.ability[0].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack1;
        }
    }
    public void Explosive2()
    {
        if (mage.Mana() > mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack4;
        }
        else if (mage.ability[0].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack1;
        }
    }
    /// <summary>
    /// 3 Star - Original Tree. Casting in order but does not look for optimal times. 
    /// </summary>
    public void Explosive3()
    {
        //Time Decay
        if (mage.Mana() > mage.ability[4].energyRequired && mage.ability[4].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack5;
        }
        //Claws
        else if (mage.Mana() > mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack2;
        }
        //Fireball
        else if (mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack3;
        }
        //Missile
        else if (mage.Mana() > mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack4;
        }       
        else if (mage.ability[0].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack1;
        }
    }
    public void Explosive4()
    {
        ClawsFromTheDeep c = (ClawsFromTheDeep)mage.ability[1];
        //Time Decay
        if (mage.Mana() > mage.ability[4].energyRequired && mage.ability[4].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack5;
        }
        //If 3 people in range of claws and Fireball
        else if (FindTile.instance.MostTilesInRange(DungeonManager.instance.currentDungeon.currentEncounter.tileList, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion(), c.width) == 3)
        {
            //if everything else isActiveAndEnabled on cooldown, claws
            if(mage.ability[4].cooldownTimer > 0 && mage.ability[3].cooldownTimer > 0 && mage.ability[2].cooldownTimer > 0)
            {
                if (mage.Mana() > mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0)
                {
                    mage.state = DecisionState.Attack2;
                }
            }
            //If not, and fireball is off cooldown and you have mana, Fireball
            else
            {
                if (mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
                {
                    mage.state = DecisionState.Attack3;
                }
            }
        }
        //Missile
        else if (mage.Mana() > mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack4;
        }
        //Claws
        else if (mage.Mana() > mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack2;
        }
        //Fireball
        else if (mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack3;
        }
    }
    public void Explosive5()
    {
        ClawsFromTheDeep c = (ClawsFromTheDeep)mage.ability[1];
        //Time Decay
        if (mage.Mana() > mage.ability[4].energyRequired && mage.ability[4].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack5;
        }
        //If 3 people in range of claws and Fireball
        else if (FindTile.instance.MostTilesInRange(DungeonManager.instance.currentDungeon.currentEncounter.tileList, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion(), c.width) >1)
        {
            //if everything else isActiveAndEnabled on cooldown, claws
            if (mage.ability[4].cooldownTimer > 0 && mage.ability[3].cooldownTimer > 0 && mage.ability[2].cooldownTimer > 0)
            {
                if (mage.Mana() > mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0)
                {
                    mage.state = DecisionState.Attack2;
                }
            }
            //If not, and fireball is off cooldown and you have mana, Fireball
            else
            {
                if (mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
                {
                    mage.state = DecisionState.Attack3;
                }
            }
        }
        //Missile
        else if (mage.Mana() > mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack4;
        }
        //Claws
        else if (mage.Mana()/mage.maxMana.value>.75f&& mage.ability[1].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack2;
        }
        //Fireball
        else if (mage.Mana() / mage.maxMana.value > .75f && mage.ability[2].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack3;
        }
    }
    /// <summary>
    /// 1 Star - Player reads spells and casts randomly
    /// </summary>
    public void Focused1()
    {
        int roll = UnityEngine.Random.Range(0, 4);
        if (roll == 0 && mage.Mana() >= mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack4;
        }
        else if (roll == 1 && mage.Mana() >= mage.ability[4].energyRequired && mage.ability[4].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack5;
        }
        else if (roll == 2 && mage.Mana() >= mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0 && !CastingArcaneShackles())
        {
            mage.state = DecisionState.Attack2;
        }
        else if (roll == 3 && mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack3;
        }
        else if (mage.ability[0].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack1;
        }        
    }

    /// <summary>
    /// 2 Star - Picks a spell and sticks with it
    /// </summary>
    public void Focused2()
    {
        if (mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack3;
        }
        else if (mage.ability[0].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack1;
        }
    }

    /// <summary>
    /// 3 Star - Original Tree. Casting in order but does not look for optimal times. 
    /// </summary>
    public void Focused3()
    {
        if (mage.Mana() >= mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack4;
        }
        else if (mage.Mana() >= mage.ability[4].energyRequired && mage.ability[4].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack5;
        }
        else if (mage.Mana() >= mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0 && !CastingArcaneShackles())
        {
            mage.state = DecisionState.Attack2;
        }
        else if (mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack3;
        }
        else if (mage.ability[0].cooldownTimer <= 0)
        {
            mage.state = DecisionState.Attack1;
        }       
    }

    /// <summary>
    /// 4 Star - Tries to find best times to cast dps cooldowns
    /// </summary>
    public void Focused4()
    {
        if (burnPhase)
        {
            if (mage.Mana() >= mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
            {
                mage.state = DecisionState.Attack4;
            }
            else if (mage.Mana() >= mage.ability[4].energyRequired && mage.ability[4].cooldownTimer <= 0)
            {
                mage.state = DecisionState.Attack5;
            }
            else if (mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
            {
                mage.state = DecisionState.Attack3;
            }
            else
            {
                burnPhase = false;
            }
        }
        else
        {
            if (mage.Mana() >= mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0 && !CastingArcaneShackles())
            {
                mage.state = DecisionState.Attack2;
            }
            else if (mage.Mana() >= mage.maxMana.value * burnPhaseThreshhold)
            {
                burnPhase = true;
            }
            else if (mage.ability[0].cooldownTimer <= 0)
            {
                mage.state = DecisionState.Attack1;
            }
        }
    }
   

    /// <summary>
    /// 5 Star - Balances Max damage with not pulling aggro
    /// </summary>
    public void Focused5()
    {
        if (burnPhase)
        {
            if (mage.Mana() >= mage.ability[3].energyRequired && mage.ability[3].cooldownTimer <= 0)
            {
                mage.state = DecisionState.Attack4;
            }
            else if (mage.Mana() >= mage.ability[4].energyRequired && mage.ability[4].cooldownTimer <= 0)
            {
                mage.state = DecisionState.Attack5;
            }
            else if (mage.Mana() > mage.ability[2].energyRequired && mage.ability[2].cooldownTimer <= 0)
            {
                mage.state = DecisionState.Attack3;
            }
            else
            {
                burnPhase = false;
            }
        }
        else
        {
            if (mage.Mana() >= mage.ability[1].energyRequired && mage.ability[1].cooldownTimer <= 0 && !CastingArcaneShackles())
            {
                mage.state = DecisionState.Attack2;
            }
            else if (mage.Mana() >= mage.maxMana.value * burnPhaseThreshhold)
            {
                burnPhase = true;
            }
            else if (mage.ability[0].cooldownTimer <= 0)
            {
                mage.state = DecisionState.Attack1;
            }
        }
    }

    //////
    /////
    ////
    ///
    //CHECKS
    ///
    ////
    /////
    public bool CastingArcaneShackles()
    {
        foreach (Character e in DungeonManager.instance.currentDungeon.currentEncounter.Mage()) if (e.GetComponent<Character>().state == DecisionState.Attack3) return true;
        return false;
    }
    
     
}
