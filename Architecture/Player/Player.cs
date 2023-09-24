using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Task {None, Grind, Potion,Craft,Research,Break}
public enum AggroDecision {Avoid,Kite,Bezerk };
public enum HazardDecision {Avoid,RunThrough,Tank };
public class Player : MonoBehaviour
{
    public int id;
    public CharacterUI playerUI;
    public string playerName;
    //Time to perform actions
    public float decisionTime;
    public float downTime;
    public float readTime;
    //Awareness and reaction
    public float aggroBelief;
    //Skill At classes
    public float beserkerSkill;
    public float mageSkill;
    public float rogueSkill;
    public float shieldBearerSkill;
    public float bardSkill;
    public float druidSkill;
    public float currentSkill;
    //Management Variables
    public float enchantmentShards;
    public Task task;
    public int timeOnTask;
    public List<Trait> traits;
    public List<int> availableDays;
    public float xp;
    public float ambition;
    public float guildLoyalty;
    //Other Variables to tie evrything together
    public Tile moveTarget;
    public Class currentClass;
    public Class mainClass;
    public Class altClass;
    public Move move;
    public AggroDecision aggroDec;
    public HazardDecision hazardDec;
    private void Awake()
    {
        decisionTime = UnityEngine.Random.Range(0.2f, 1.4f);
        downTime = UnityEngine.Random.Range(0.2f, 1.4f);
        readTime = UnityEngine.Random.Range(0.2f, 1.4f);
        playerUI = GetComponent<CharacterUI>();
    }

    public void UpdateName() => name = $"{playerName} - {Utility.instance.SpecName(currentClass)} - {currentClass.Score()}";

    public void CreateAlt(Class playerClass)
    {
        Class core1 = Instantiate(playerClass, currentClass.transform);
        altClass = core1;
        SetUpChar(core1, this, move);
        Utility.instance.TurnOff(core1.gameObject);
    }
    public void SetUpChar(Class core, Player player, Move move)
    {
        playerUI.characterNameText.text = "";
        core.decisionTime = player.decisionTime;
        core.downTime = player.downTime;
        core.readTime = player.readTime;
        core.specNumber = core.id % 2;
        core.Create();
    }

    public void Main() 
    {
        currentClass = mainClass;
        currentClass = mainClass;
        move.character = mainClass;
        Utility.instance.TurnOn(mainClass.gameObject);
        if(altClass != null) Utility.instance.TurnOff(altClass.gameObject);
        PlayerSkill();        
        UpdateName();
    }   

    private void PlayerSkill()
    {
        if (currentClass.GetComponent<Bard>())
        {
            BardDecision decision = currentClass.GetComponent<BardDecision>();
            decision.bard = (Bard)currentClass;
            decision.player = this;            
            currentSkill = bardSkill;
            decision.UpdateSkill();            
        }
        else if (currentClass.GetComponent<Mage>())
        {
            MageDecision decision = currentClass.GetComponent<MageDecision>();
            decision.mage = (Mage)currentClass;
            decision.player = this;            
            currentSkill = mageSkill;
            decision.UpdateSkill();
           
        }
        else if (currentClass.GetComponent<Rogue>())
        {
            RogueDecision decision = currentClass.GetComponent<RogueDecision>();
            decision.rogue = (Rogue)currentClass;
            decision.player = this;
            currentSkill = rogueSkill;
            decision.UpdateSkill();           
        }
        else if (currentClass.GetComponent<Beserker>())
        {
            BeserkerDecision decision = currentClass.GetComponent<BeserkerDecision>();
            decision.beserker = (Beserker)currentClass;
            decision.player = this;
            currentSkill = beserkerSkill;
            decision.UpdateSkill();            
        }
        else if (currentClass.GetComponent<ShieldBearer>())
        {
            ShieldBearerDecision decision = currentClass.GetComponent<ShieldBearerDecision>();
            decision.shieldBearer = (ShieldBearer)currentClass;
            decision.player = this;
            currentSkill = shieldBearerSkill;
            decision.UpdateSkill();           
        }
        else if (currentClass.GetComponent<Druid>())
        {
            DruidDecision decision = currentClass.GetComponent<DruidDecision>();
            decision.druid = (Druid)currentClass;
            decision.player = this;
            currentSkill = druidSkill;
            decision.UpdateSkill();            
        }
    }

    public void Alt()
    {
        currentClass = altClass;
        move.character = altClass;
        Utility.instance.TurnOn(altClass.gameObject);
        Utility.instance.TurnOff(mainClass.gameObject);
        PlayerSkill();
        UpdateName();
    }

    public void Switch()
    {
        if (currentClass == mainClass) Alt();
        else Main();
    }

    internal void GetAvailabilty()
    {
        availableDays.Clear();
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            availableDays.Add(1);
            availableDays.Add(3);
            availableDays.Add(5);
            availableDays.Add(7);
        }
        else
        {
            availableDays.Add(2);
            availableDays.Add(4);
            availableDays.Add(6);
        }
    }
}
