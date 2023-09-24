using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAgent : MonoBehaviour,IDataPersistence
{
    public static CreateAgent instance;
    public int characterIdCounter;
    public int bossIdCounter;
    public Player sample;
    private void Awake()
    {
        instance = this;        
    }
    public void CreatePlayers()
    {
        for (int i = 0; i < 3; i++) CreateNewPlayer(0,1.5f,2.4f);
        for (int i = 0; i < 3; i++) CreateNewPlayer(1,1.5f,2.4f);
        for (int i = 0; i < 3; i++) CreateNewPlayer(2,1.5f,2.4f);
        for (int i = 0; i < 3; i++) CreateNewPlayer(3,1.5f,2.4f);
        for (int i = 0; i < 3; i++) CreateNewPlayer(4,1.5f,2.4f);
        for (int i = 0; i < 3; i++) CreateNewPlayer(5,1.5f,2.4f);    
    }

    public void CreateNewPlayer(int x,float min, float max)
    {
        //Instantiate Components
        Player p = Instantiate(GameObjectList.instance.player, transform);
        Move m = p.GetComponent<Move>();
        GameObject g = Instantiate(GameObjectList.instance.agent, p.transform);     
        p.mainClass = GiveClass(g, p, x,min,max);
        //Generate Name
        string name = Names.instance.userName[UnityEngine.Random.Range(0, Names.instance.userName.Count)];       
        Names.instance.userName.Remove(name);
        if (name == "FemboyEthnoState"||name == "Asmongold")
        {
            name = Names.instance.userName[UnityEngine.Random.Range(0, Names.instance.userName.Count)];
            Names.instance.userName.Remove(name);
        }
        p.playerName = p.name = name;
        //Add to main list
        CharacterList.instance.freeAgents.Add(p);
        //Specific to New PLayer SetUp      
        p.id = p.mainClass.id = characterIdCounter;
        characterIdCounter++;
        //Character Set Up
        GenericHookupPlayer(p, m);              
        p.ambition = UnityEngine.Random.Range(0,p.currentSkill*20+10);
        p.ambition = p.ambition > 99 ? 99 : p.ambition;
        p.GetAvailabilty();
        int traitRoll = UnityEngine.Random.Range(1,101);
        if(traitRoll >90)
        {
            List<int> available = new List<int> { };
            for (int i = 0; i < GameObjectList.instance.traits.Count; i++) available.Add(i);
            int a = available[UnityEngine.Random.Range(0, available.Count)];
            available.Remove(a);
            int b = available[UnityEngine.Random.Range(0, available.Count)];
            AddTrait(a,p);
            AddTrait(b,p);
        }
        else if (traitRoll > 70) AddTrait(UnityEngine.Random.Range(0, GameObjectList.instance.traits.Count),p);          
    }

    public Class GiveClass(GameObject g, Player p, int x,float min,float max )
    {
        List<float> list = new List<float> { };
        for (int i = 0; i < 6; i++)
        {
            if (i == x) list.Add((float)Math.Round(UnityEngine.Random.Range(min, max), 1));
            else list.Add((float)Math.Round(UnityEngine.Random.Range(0, 0.5f), 1));
        }        
        return GiveClass(g, p, x, list);
    }

    private Class GiveClass(GameObject g,Player p, int x, List<float> v)
    {
        p.beserkerSkill = v[0];
        p.druidSkill = v[1];
        p.rogueSkill = v[2];
        p.mageSkill = v[3];
        p.bardSkill = v[4];
        p.shieldBearerSkill = v[5];        
        if (x == 0)
        {
            g.AddComponent<Beserker>();
            g.AddComponent<BeserkerDecision>();            
            return g.GetComponent<Beserker>();           
        }
        else if (x == 1)
        {
            g.AddComponent<Druid>();
            g.AddComponent<DruidDecision>();            
            return g.GetComponent<Druid>();            
        }
        else if (x == 2)
        {
            g.AddComponent<Rogue>();
            g.AddComponent<RogueDecision>();            
            return  g.GetComponent<Rogue>();            
        }
        else if (x == 3)
        {
            g.AddComponent<Mage>();
            g.AddComponent<MageDecision>();           
            return g.GetComponent<Mage>();            
        }
        if (x == 4)
        {
            g.AddComponent<Bard>();
            g.AddComponent<BardDecision>();
            return g.GetComponent<Bard>();
        }
        else if(x == 5)
        {
            g.AddComponent<ShieldBearer>();
            g.AddComponent<ShieldBearerDecision>();           
            return g.GetComponent<ShieldBearer>();            
        }        
        return null;
    }

    public void GenericHookupPlayer(Player p,Move m)
    {
        GenericHookupCharacter(p.mainClass, p, m);
        if (p.altClass != null) GenericHookupCharacter(p.altClass, p, m);
        p.move = m;       
        m.character = p.mainClass;
        m.player = p;                   
        p.Main();
    }
    public void GenericHookupCharacter(Class c, Player p, Move m)
    {
        c.weapon.character = c;
        c.chest.character = c;
        c.legs.character = c;
        c.feet.character = c;
        c.trinket.character = c;
        c.weapon.character = c;
        c.offHand.character = c;
        c.ability = new List<Ability>();
        c.headSets = new List<ItemSO>();
        c.chestSets = new List<ItemSO>();
        c.legSets = new List<ItemSO>();
        c.feetSets = new List<ItemSO>();
        c.trinketSets = new List<ItemSO>();
        c.weaponSets = new List<ItemSO>();
        c.offHandSets = new List<ItemSO>();
        c.buff = new List<Effect>();
        c.debuff = new List<Effect>();
        c.maxHealth = new CharacterAttribute();
        c.maxMana = new CharacterAttribute();
        c.defence = new CharacterAttribute();
        c.attackPower = new CharacterAttribute();
        c.spellpower = new CharacterAttribute();
        c.damage = new CharacterAttribute();
        c.crit = new CharacterAttribute();
        c.vamp = new CharacterAttribute();
        c.movement = new CharacterAttribute();
        c.haste = new CharacterAttribute();
        c.thorns = new CharacterAttribute();
        c.manaRegenTime = new CharacterAttribute();
        c.manaRegenValue = new CharacterAttribute();
        c.physicalDamageMod = new CharacterAttribute();
        c.magicDamageMod = new CharacterAttribute();
        c.manaRegenMod = new CharacterAttribute();
        c.damageTakenMod = new CharacterAttribute();
        c.healingMod = new CharacterAttribute();
        c.energyCostMod = new CharacterAttribute();
        c.move = m;
        c.player = p;
        c.playerUI = p.playerUI;
        c.characterName = p.playerName;
        c.name = c.characterName;
        c.playerUI.GetComponent<SpriteRenderer>().sprite = c.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.characters[(int)c.species];
        c.SpriteOff();
        p.SetUpChar(c, p, m);
    }

    private void AddTrait(int a,Player p)
    {
        Trait trait = Instantiate(GameObjectList.instance.traits[a],p.transform);
        trait.player = p;
        trait.name = trait.traitName = GameObjectList.instance.traits[a].name;
        if (trait.immediate) trait.Effect();
        p.traits.Add(trait);
    }
    public void LoadData(GameData data)
    {
        for (int i = 0; i < data.playerId.Count; i++)
        {
            Player p = Instantiate(GameObjectList.instance.player, transform);
            Move m = p.GetComponent<Move>();
            GameObject g = Instantiate(GameObjectList.instance.agent, p.transform);
            p.playerName = data.playerNames[i];
            if (data.inGuild[i]) Guild.instance.roster.Add(p);
            else CharacterList.instance.freeAgents.Add(p);
            //Specific Loaded Player Data            
            p.beserkerSkill = data.beserkerSkill[i];
            p.mageSkill = data.mageSkill[i];
            p.rogueSkill = data.rogueSkill[i];
            p.shieldBearerSkill = data.shieldBearerSkill[i];
            p.bardSkill = data.bardSkill[i];
            p.druidSkill = data.druidSkill[i];
            p.enchantmentShards = data.enchantmentShards[i];
            p.task = (Task)data.task[i];
            p.timeOnTask = data.timeOnTask[i];
            p.xp = data.xp[i];
            p.ambition = data.ambition[i];
            p.guildLoyalty = data.guildLoyalty[i];
            p.id = data.playerId[i];
            ///Add Traits
            List <int> traits = ReturnNumbers(data.traits.ToList(),i);
            for (int j = 0; j < traits.Count; j++) AddTrait(traits[j], p);
            //Add Available Days
            List<int> availableDays = ReturnNumbers(data.availableDays.ToList(), i);
            for (int j = 0; j < availableDays.Count; j++) p.availableDays.Add(availableDays[j]);
            ///
            //Find Main (and if necessary, Alt) Character
            ///
            int mainInt = 99;
            int altInt = 99;
            for (int j = 0; j < data.characterId.Count; j++)
            {
                if (data.characterId[j] == i && mainInt == 99) mainInt = j;
                else if (data.characterId[j] == i && mainInt != 99) altInt = j;
            }
            p.mainClass = GiveClass(g, p, data.characterClass[mainInt], new List<float> { data.beserkerSkill[i], data.druidSkill[i], data.rogueSkill[i], data.mageSkill[i], data.bardSkill[i], data.shieldBearerSkill[i] });
            //Generic Hookup, things that are the same across the board
            GenericHookupPlayer(p, m);
            LoadCharacterData(p.mainClass,mainInt,data);
            if (altInt != 99)
            {
                p.altClass = GiveClass(g, p, data.characterClass[altInt], new List<float> { data.beserkerSkill[i], data.druidSkill[i], data.rogueSkill[i], data.mageSkill[i], data.bardSkill[i], data.shieldBearerSkill[i] });
                LoadCharacterData(p.altClass, altInt,data);
            }
        }
        characterIdCounter = data.playerId.Count;
        
    }

    private void LoadCharacterData(Class c, int x,GameData data)
    {        
        c.specNumber = data.specNumber[x];
        List<int> head = ReturnNumbers(data.headSets.ToList(), x);
        List<int> chest = ReturnNumbers(data.chestSets.ToList(), x);
        List<int> legs = ReturnNumbers(data.legSets.ToList(), x);
        List<int> feet = ReturnNumbers(data.feetSets.ToList(), x);
        List<int> weapon = ReturnNumbers(data.weaponSets.ToList(), x);
        List<int> offHand = ReturnNumbers(data.offHandSets.ToList(), x);
        List<int> trinket = ReturnNumbers(data.trinketSets.ToList(), x);
        c.id = data.characterId[x];

        ///Add Head Gear
        c.headSets[0] = ItemList.instance.equipmentMasterList[head[0]];
        if(head.Count ==2) c.headSets[1] = ItemList.instance.equipmentMasterList[head[1]];
        ///Add Chest Gear
        
        c.chestSets[0] = ItemList.instance.equipmentMasterList[chest[0]];
        if (chest.Count == 2) c.chestSets[1] = ItemList.instance.equipmentMasterList[chest[1]];
        ///Add Legs Gear
        
        c.legSets[0] = ItemList.instance.equipmentMasterList[legs[0]];
        if (legs.Count == 2) c.legSets[1] = ItemList.instance.equipmentMasterList[legs[1]];
        ///Add Feet Gear
       
        c.feetSets[0] = ItemList.instance.equipmentMasterList[feet[0]];
        if (feet.Count == 2) c.feetSets[1] = ItemList.instance.equipmentMasterList[feet[1]];
        ///Add Weapon Gear
        
        c.weaponSets[0] = ItemList.instance.equipmentMasterList[weapon[0]];
        if (weapon.Count == 2)  c.weaponSets[1] = ItemList.instance.equipmentMasterList[weapon[1]];
        ///Add Off Hand Gear


        c.offHandSets[0] = ItemList.instance.equipmentMasterList[offHand[0]];
        if (offHand.Count == 2) c.offHandSets[1] = ItemList.instance.equipmentMasterList[offHand[1]];
        ///Add Trinket Gear
        
        c.trinketSets[0] = ItemList.instance.equipmentMasterList[trinket[0]];
        if (trinket.Count == 2)  c.trinketSets[1] = ItemList.instance.equipmentMasterList[trinket[1]];
        
    }

    //THIS IS TOO BIG  A PROCESS TO HAPPEN OFTEN. COME BACK TO THIS
    //MAKE IT SO THE LISTS ARE GENERATED ONCE< THEN JUST LOOK AT THE LISTS
    public List<int> ReturnNumbers(List<int> list,int x)
    {
        List<List<int>> listOfLists = new List<List<int>> { };
        List<int> numbers = list.ToList();
        while (numbers.Count > 0)
        {
            List<int> newList = new List<int> { };
            int count = numbers[0];
            numbers.RemoveAt(0);          
            for (int i = 0; i < count; i++)
            {
                newList.Add(numbers[0]);
                numbers.RemoveAt(0);
            }
            listOfLists.Add(newList);
        }
        return listOfLists[x];
    }

    public void SaveData(GameData data)
    {
        data.playerNames.Clear();
        data.decisionTime.Clear();
        data.downTime.Clear();
        data.readTime.Clear();
        data.inGuild.Clear();
        data.beserkerSkill.Clear();
        data.mageSkill.Clear();
        data.rogueSkill.Clear();
        data.shieldBearerSkill.Clear();
        data.bardSkill.Clear();
        data.druidSkill.Clear();
        data.enchantmentShards.Clear();
        data.task.Clear();
        data.timeOnTask.Clear();
        data.traits.Clear();
        data.availableDays.Clear();
        data.xp.Clear();
        data.ambition.Clear();
        data.guildLoyalty.Clear();
        data.playerId.Clear();
        data.characterId.Clear();
        data.specNumber.Clear();
        data.headSets.Clear();
        data.chestSets.Clear();
        data.legSets.Clear();
        data.feetSets.Clear();
        data.trinketSets.Clear();
        data.weaponSets.Clear();
        data.offHandSets.Clear();
        foreach (Player p in CharacterList.instance.CharactersInTheGame()) SavePlayer(p,data);
    }

    public void SavePlayer(Player p, GameData data)
    {
        data.playerId.Add(p.id);
        data.playerNames.Add(p.playerName);
        data.decisionTime.Add(p.decisionTime);
        data.downTime.Add(p.downTime);
        data.readTime.Add(p.readTime);
        if (Guild.instance.roster.Contains(p)) data.inGuild.Add(true);
        else data.inGuild.Add(false);
        data.beserkerSkill.Add(p.beserkerSkill);
        data.mageSkill.Add(p.mageSkill);
        data.rogueSkill.Add(p.rogueSkill);
        data.shieldBearerSkill.Add(p.shieldBearerSkill);
        data.bardSkill.Add(p.bardSkill);
        data.druidSkill.Add(p.druidSkill);
        data.enchantmentShards.Add(p.enchantmentShards);
        data.task.Add((int)p.task);
        data.timeOnTask.Add(p.timeOnTask);
        data.traits.Add(p.traits.Count);
        for (int i = 0; i < p.traits.Count; i++) data.traits.Add(p.traits[i].id);
        data.availableDays.Add(p.availableDays.Count);
        for (int i = 0; i < p.availableDays.Count; i++) data.availableDays.Add(p.availableDays[i]);
        data.xp.Add(p.downTime);
        data.ambition.Add(p.downTime);
        data.guildLoyalty.Add(p.downTime);
        if (p.mainClass != null) SaveCharacter(p.mainClass, data);
        if (p.altClass != null) SaveCharacter(p.altClass, data);        
    }

    private void SaveCharacter(Character c, GameData data)
    {
        Class cl = (Class)c;
        data.characterClass.Add(cl.GetComponent<Beserker>() ? 0 : cl.GetComponent<Druid>() ? 1 : cl.GetComponent<Rogue>() ? 2 : cl.GetComponent<Mage>() ? 3 : cl.GetComponent<ShieldBearer>() ? 4 : 5);
        data.characterId.Add(c.id);
        data.specNumber.Add(cl.specNumber);
        data.headSets.Add(cl.headSets.Count);
        for (int i = 0; i < cl.headSets.Count; i++) data.headSets.Add(cl.headSets[i].id); 
        data.chestSets.Add(cl.chestSets.Count);
        for (int i = 0; i < cl.chestSets.Count; i++) data.chestSets.Add(cl.chestSets[i].id);
        data.legSets.Add(cl.legSets.Count);
        for (int i = 0; i < cl.legSets.Count; i++) data.legSets.Add(cl.legSets[i].id);
        data.feetSets.Add(cl.feetSets.Count);
        for (int i = 0; i < cl.feetSets.Count; i++) data.feetSets.Add(cl.feetSets[i].id);
        data.trinketSets.Add(cl.trinketSets.Count);
        for (int i = 0; i < cl.trinketSets.Count; i++) data.trinketSets.Add(cl.trinketSets[i].id);
        data.weaponSets.Add(cl.weaponSets.Count);
        for (int i = 0; i < cl.weaponSets.Count; i++) data.weaponSets.Add(cl.weaponSets[i].id);
        data.offHandSets.Add(cl.offHandSets.Count);
        for (int i = 0; i < cl.offHandSets.Count; i++) data.offHandSets.Add(cl.offHandSets[i].id);
    }
}
