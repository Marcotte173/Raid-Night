using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public string dungeonName;
    public Sprite dungeonPic;
    public int id;
    public int dungeonCompleted;
    public int maxPlayers;
    public List<Character> agentsInDungeon; 
    public int recommendedGearScore;
    public List<string> description;
    public List<Encounter> encounter;
    public int encountersCompleted;
    public Encounter currentEncounter;
    public Expansion expansion;
    public float tier;
}
