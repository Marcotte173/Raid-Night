using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    public static CharacterList instance;
    public List<Player> freeAgents;
    private void Awake()
    {
        instance = this;
    }

    public List<Player> CharactersInTheGame()
    {
        List<Player> all = new List<Player> { };
        foreach (Player p in freeAgents) all.Add(p);
        foreach (Player p in Guild.instance.roster) all.Add(p);
        return all;
    }
}