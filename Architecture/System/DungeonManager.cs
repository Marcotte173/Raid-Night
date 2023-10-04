using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    public List<Dungeon> pve;
    public Dungeon currentDungeon;    
    public RaidMode raidMode;
    public GameObject dungeonAgents;
    public Expansion expansion;
    public float patch;
    private void Awake()
    {
        instance = this;
        patch = 1f;
        for (int i = 0; i < pve.Count; i++)
        {
            Dungeon e = Instantiate(pve[i], transform);
            e.name = pve[i].dungeonName;
            pve[i] = e; 
        }
    }

    internal void Raid(Dungeon e,List<Character> roster)
    {
        currentDungeon = e;
        currentDungeon.agentsInDungeon.Clear();
        foreach (Character a in roster) LoadInDungeon(a.player); 
        currentDungeon.GetComponent<EncounterManager>().Begin(currentDungeon.encounter[currentDungeon.encountersCompleted]);
    }

    internal void TestRaid(Dungeon dungeon, List<Player> players)
    {
        currentDungeon = dungeon;
        currentDungeon.agentsInDungeon.Clear();
        foreach (Player a in players) LoadInDungeon(a);
        currentDungeon.GetComponent<EncounterManager>().Begin(currentDungeon.encounter[currentDungeon.encountersCompleted]);
    }
    public void PutInArena(Player p, Vector2 position)
    {
        currentDungeon.currentEncounter.player.Add(p.currentClass);              
        p.transform.SetParent(EncounterUI.instance.charactersGameObject.transform);
        p.transform.position = position;
    }
    public void PutInArena(Character p, Vector2 position)
    {
        currentDungeon.currentEncounter.boss.Add(p);        
        p.transform.SetParent(EncounterUI.instance.charactersGameObject.transform);
        p.transform.position = position;
    }
    public void SendHome(Player p)
    {
        if(currentDungeon.currentEncounter.player.Contains(p.currentClass)) currentDungeon.currentEncounter.player.Remove(p.currentClass);
        p.transform.SetParent(GameManager.instance.guild.transform);
    }
    public void LoadInDungeon(Player p)
    {
        currentDungeon.agentsInDungeon.Add(p.currentClass);
        p.transform.SetParent(dungeonAgents.transform);
    }
    public void DungeonToLoad(Character p)
    {        
        p.player.transform.SetParent(dungeonAgents.transform);
    }
}
