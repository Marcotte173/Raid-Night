using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Tile currentTile;
    public Tile nextTile;    
    public List<Tile> path;
    public List<Tile> unwalkable;
    public List<Tile> closed;
    public List<Tile> open;
    public Vector2 prevPosition;
    public bool isMoving;
    public bool on;
    public Character character;
    public Player player;
    public List<string> objectName;

    public void UpdateMove()
    {
        if (on) CurrentTile();            
    }
    public List<Character> PeopleWhoAreNotMeOrMyTarget()
    {
        List<Character> newList = new List<Character> { };
        foreach(Character a in DungeonManager.instance.currentDungeon.currentEncounter.Characters()) if (a != character && a != character.target) newList.Add(a);
        return newList;
    }
    public void CurrentTile()
    {
        currentTile = FindTile.instance.Location(new Vector2(Mathf.Round(transform.position.x), (Mathf.Round(transform.position.y))));
    }
}
