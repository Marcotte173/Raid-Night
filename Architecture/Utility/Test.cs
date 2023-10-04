using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Test : MonoBehaviour
{
    public static Test instance;
    public TMP_Text agentCount;
    public TMP_InputField agentStar;
    public TMP_InputField agentdecisionTime;
    public TMP_InputField agentdownTime;
    public List<Toggle> dungeons;
    public List<Toggle> encounters;
    public List<Toggle> agents;
    public List<Player> testAgents;
    public List<Player> testAgentsToGoInDungeon;
    public List<Dungeon> availableDungeons;
    public Dungeon testDungeon;
    public List<Encounter> availableEncounters;
    private void Awake()
    {
        instance = this;
    }
    public void Begin()
    {
        GameManager.instance.test = true;
        Utility.instance.TurnOff(UIManager.instance.menu);
        Utility.instance.TurnOff(UIManager.instance.guild);
        Utility.instance.TurnOff(UIManager.instance.background.gameObject);
        Utility.instance.TurnOff(UIManager.instance.dungeons.gameObject);
        Utility.instance.TurnOn(UIManager.instance.testMenu.gameObject);
        testAgents.Add(CharacterList.instance.freeAgents[0]);
        testAgents.Add(CharacterList.instance.freeAgents[1]);        
        testAgents.Add(CharacterList.instance.freeAgents[3]);
        testAgents.Add(CharacterList.instance.freeAgents[4]);
        testAgents.Add(CharacterList.instance.freeAgents[6]);
        testAgents.Add(CharacterList.instance.freeAgents[7]);
        testAgents.Add(CharacterList.instance.freeAgents[9]);
        testAgents.Add(CharacterList.instance.freeAgents[10]);
        testAgents.Add(CharacterList.instance.freeAgents[12]);
        testAgents.Add(CharacterList.instance.freeAgents[13]);
        testAgents.Add(CharacterList.instance.freeAgents[15]);
        testAgents.Add(CharacterList.instance.freeAgents[16]);
        agentStar.text = 3.ToString();
        agentdownTime.text = .2f.ToString();
        agentdecisionTime.text = .2f.ToString();
        foreach (Toggle t in agents) t.isOn = false;
        foreach (Toggle t in dungeons) t.isOn = false;
        foreach (Toggle t in encounters) t.isOn = false;        
        foreach (Toggle t in dungeons) Utility.instance.TurnOff(t.gameObject);
        foreach (Toggle t in encounters) Utility.instance.TurnOff(t.gameObject);
        foreach (Dungeon d in DungeonManager.instance.pve) availableDungeons.Add(d);
        for (int i = 0; i < availableDungeons.Count; i++)
        {
            Utility.instance.TurnOn(dungeons[i].gameObject);
            dungeons[i].GetComponentInChildren<Text>().text = availableDungeons[i].dungeonName;
        }        
        testAgentsToGoInDungeon.Clear();
    }

    public void DungeonToggle1() => DungeonToggle(0);
    public void DungeonToggle2() => DungeonToggle(1);
    public void DungeonToggle3() => DungeonToggle(2);
    public void DungeonToggle4() => DungeonToggle(3);
    public void DungeonToggle5() => DungeonToggle(4);

    public void DungeonToggle(int x)
    {
        for (int i = 0; i <  availableDungeons.Count; i++)
        {
            if(x== i)
            {
                if (dungeons[i].isOn)
                {
                    if (testDungeon != availableDungeons[i])
                    {
                        testDungeon = Instantiate(availableDungeons[i]);
                        availableEncounters.Clear();
                        foreach (Encounter e in availableDungeons[i].encounter) availableEncounters.Add(e);
                        for (int j = 0; j < availableEncounters.Count; j++)
                        {
                            Utility.instance.TurnOn(encounters[j].gameObject);
                            encounters[j].GetComponentInChildren<Text>().text = availableEncounters[j].name;
                        }
                        for (int h = 0; h < dungeons.Count; h++) if (h != x) dungeons[h].isOn = false;
                        foreach (Toggle t in encounters) t.isOn = true;
                    }
                }
                else
                {
                    Destroy(testDungeon.gameObject);
                    testDungeon = null;
                }
            } 
        }
    }

    public void EncounterToggle()
    {
        for (int i = 0; i < availableEncounters.Count; i++)
        {
            if (encounters[i].isOn)
            {
                if (!testDungeon.encounter.Contains(availableEncounters[i])) testDungeon.encounter.Add(availableEncounters[i]);
            }
            else
            {
                if (testDungeon.encounter.Contains(availableEncounters[i])) testDungeon.encounter.Remove(availableEncounters[i]);
            }
        }
        if(testDungeon!=null) EncounterSort();
    }

    private void EncounterSort()
    {
        Encounter temp;
        for (int j = 0; j <= testDungeon.encounter.Count - 2; j++)
        {
            for (int i = 0; i <= testDungeon.encounter.Count - 2; i++)
            {
                if (testDungeon.encounter[i].id > testDungeon.encounter[i + 1].id)
                {
                    temp = testDungeon.encounter[i + 1];
                    testDungeon.encounter[i + 1] = testDungeon.encounter[i];
                    testDungeon.encounter[i] = temp;
                }
            }
        }
    }

    public void AgentToggle()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i].isOn)
            {
                if (!testAgentsToGoInDungeon.Contains(testAgents[i])) testAgentsToGoInDungeon.Add(testAgents[i]);
            }
            else
            {
                if (testAgentsToGoInDungeon.Contains(testAgents[i])) testAgentsToGoInDungeon.Remove(testAgents[i]);
            }
        }
        agentCount.text = $"{testAgentsToGoInDungeon.Count}/5 Agents";
    }

    public void BeginButton()
    {
        foreach(Player p in testAgentsToGoInDungeon)
        {
            p.currentSkill = Convert.ToInt32(agentStar.text);
            p.currentClass.decisionTime = p.decisionTime = (float)Convert.ToDouble(agentdecisionTime.text);
            p.currentClass.downTime = p.downTime = (float)Convert.ToDouble(agentdownTime.text);
            if (p.currentClass.GetType() == typeof(Bard)) p.currentClass.GetComponent<BardDecision>().UpdateSkill();
            else if (p.currentClass.GetType() == typeof(Beserker)) p.currentClass.GetComponent<BeserkerDecision>().UpdateSkill();
            else if (p.currentClass.GetType() == typeof(Mage)) p.currentClass.GetComponent<MageDecision>().UpdateSkill();
            else if (p.currentClass.GetType() == typeof(Druid)) p.currentClass.GetComponent<DruidDecision>().UpdateSkill();
            else if (p.currentClass.GetType() == typeof(Rogue)) p.currentClass.GetComponent<RogueDecision>().UpdateSkill();
            else if (p.currentClass.GetType() == typeof(ShieldBearer)) p.currentClass.GetComponent<ShieldBearerDecision>().UpdateSkill();
        }
        UIManager.instance.PVEStart();
        DungeonManager.instance.TestRaid(testDungeon, testAgentsToGoInDungeon);
    }
}