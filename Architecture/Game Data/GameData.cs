using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Time Data
    public int day;
    public int week;
    public int month;
    public int year;
    //Guild Data
    public float guildRenown;
    public float guildCurrency;
    public bool seeHidden;
    public List<bool> inGuild = new List<bool> { };
    public List<string> playerNames= new List<string> { };
    //Player Data
    public List<int> playerId;
    public List<float> decisionTime;
    public List<float> downTime;
    public List<float> readTime;
    public List<float>  beserkerSkill;
    public List<float>  mageSkill;
    public List<float>  rogueSkill;
    public List<float>  shieldBearerSkill;
    public List<float>  bardSkill;
    public List<float>  druidSkill;
    public List<float>  enchantmentShards;
    public List<int> task;
    public List<int>    timeOnTask;
    public List<int> traits;
    public List<int>    availableDays;
    public List<float>  xp;
    public List<float>  ambition;
    public List<float>  guildLoyalty;
    //Character Data
    public List<int> characterId;
    public List<int> characterClass;
    public List<int> specNumber;
    public List<int> headSets;
    public List<int> chestSets;
    public List<int> legSets;
    public List<int> feetSets;
    public List<int> trinketSets;
    public List<int> weaponSets;
    public List<int> offHandSets;
    
    
    

    public GameData()
    {
        day = 1;
        week = 1;
        month = 1;
        year = 2022;
        guildCurrency = 10;
        guildRenown = 10;
        seeHidden = false;
        playerId = new List<int> { };
        characterId = new List<int> { };
        playerNames = new List<string> { };
        decisionTime = new List<float> { };
        downTime = new List<float> { };
        readTime = new List<float> { };
        inGuild = new List<bool> { };
        beserkerSkill = new List<float> { };
        mageSkill = new List<float> { };
        rogueSkill = new List<float> { };
        shieldBearerSkill = new List<float> { };
        bardSkill = new List<float> { };
        druidSkill = new List<float> { };
        enchantmentShards = new List<float> { };
        task = new List<int> { };
        timeOnTask = new List<int> { };
        traits = new List<int> { };
        availableDays = new List<int> { };
        xp = new List<float> { };
        ambition = new List<float> { };
        guildLoyalty = new List<float> { };
        specNumber = new List<int> { };
        headSets = new List<int> { };
        chestSets = new List<int> { };
        legSets = new List<int> { };
        feetSets = new List<int> { };
        trinketSets = new List<int> { };
        weaponSets = new List<int> { };
        offHandSets = new List<int> { };
        characterClass = new List<int> { };
    }
}
