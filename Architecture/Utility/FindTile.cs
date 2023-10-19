using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FindTile : MonoBehaviour
{
    public static FindTile instance;
    private void Awake()
    {
        instance = this;
    }
    internal Tile TileClosestToMostTargets(List<Tile> tileList, List<Character> player, float distance)
    {
        List<Tile> one = new List<Tile> { };
        List<Tile> two = new List<Tile> { };
        List<Tile> three = new List<Tile> { };
        foreach (Tile t in tileList)
        {
            int x = 0;
            foreach (Character p in player) if (Distance(t.transform.position, p.transform.position) <= distance) x++;
            if (x == 3) three.Add(t);
            else if (x == 2) two.Add(t);
            else if (x == 1) one.Add(t);
        }
        if (three.Count > 0) return three[UnityEngine.Random.Range(0, three.Count)];
        if (two.Count > 0) return two[UnityEngine.Random.Range(0, two.Count)];
        if (one.Count > 0) return one[UnityEngine.Random.Range(0, one.Count)];
        return null;
    }
    internal int MostTilesInRange(List<Tile> tileList, List<Character> player, float distance)
    {
        List<Tile> one = new List<Tile> { };
        List<Tile> two = new List<Tile> { };
        List<Tile> three = new List<Tile> { };
        foreach (Tile t in tileList)
        {
            int x = 0;
            foreach (Character p in player) if (Distance(t.transform.position, p.transform.position) <= distance) x++;
            if (x == 3) three.Add(t);
            else if (x == 2) two.Add(t);
            else if (x == 1) one.Add(t);
        }
        if (three.Count > 0) return 3;
        if (two.Count > 0) return 2;
        if (one.Count > 0) return 1;
        return 0;
    }

    public Tile FindKnockbackTile(Tile source, Tile current, float amount)
    {
        Tile destination = null;
        for (int i = 0; i < amount; i++)
        {
            if (Otherside(source, current) != null)
            {
                destination = Otherside(source, current);
                source = current;
                current = destination;
            }
            else break;
        }
        return destination;
    }
    public Tile FindClosestUnoccupiedTileAdjacentToTarget(Tile originTile, Tile destinationTile, List<Tile> exclusions)
    {
        List<Tile> tileList = new List<Tile> { };
        foreach (Tile t in destinationTile.neighbor)
        {
            //string a = "Neighbor : " + t.x + "," + t.y + " - ";
            if (t.OccupiedBy() == null)
            {
                //a += "Not Occupied. ";
                tileList.Add(t);
            }
            else { }//a += "Occupied by : " + t.OccupiedBy().characterName;
            //Debug.Log(a);
        }
        if (exclusions != null) if (exclusions.Count > 0) foreach (Tile t in exclusions) if (tileList.Contains(t)) tileList.Remove(t);
        Tile closest = tileList[0];
        //Calculate Distance to target
        float distance = Distance(destinationTile.transform.position, closest.transform.position) + Distance(originTile.transform.position, closest.transform.position);
        //Check each other in the list
        foreach (Tile c in tileList)
        {
            //If the distance to them is shorter
            if (Distance(destinationTile.transform.position, c.transform.position) +Distance(originTile.transform.position, c.transform.position) < distance)
            {
                //They are the target, new distance
                closest = c;
                distance = Distance(destinationTile.transform.position, c.transform.position) +Distance(originTile.transform.position, c.transform.position);
            }
        }
        return closest;
    }
    public Tile FindClosestUnoccupiedTile(Tile originTile)
    {
        foreach(Tile t in originTile.neighbor)
        {
            if (t.OccupiedBy() == null) return t;
        }
        return null;
    }
    public Tile FindClosestUnoccupiedTile(Tile originTile,Tile targetTile)
    {
        List<Tile> options = new List<Tile>();
        foreach (Tile t in originTile.neighbor) if (t.OccupiedBy() == null) options.Add(t);
        Tile closest = options[0];
        
        float distance = Distance(options[0], targetTile);
        if (options.Count > 1)
        {
            for (int i = 0; i < options.Count; i++)
            {
                Debug.Log($"{options[i].name} - {targetTile.name}: {Distance(options[i], targetTile) }");
                if (Distance(options[i], targetTile) < distance)
                {
                    distance = Distance(options[i], targetTile);
                    closest = options[i];
                }
            }
        }        
        return closest;
    }
    public Tile FindClosestUnoccupiedTileAdjacentToTarget(Tile originTile, Tile destinationTile)
    {
        return FindClosestUnoccupiedTileAdjacentToTarget(originTile, destinationTile, null);
    }

    internal Tile FindUnoccupiedTileAdjacentToTarget(Tile tile)
    {
        foreach (Tile t in tile.neighbor)
        {
            if (t.OccupiedBy() == null) return t;
        }
        return null;
    }
    public Tile FindClosestTileNotInHazard(Tile start)
    {
        Tile theTile = null;
        List<Tile> hazardTiles = new List<Tile> { };
        List<Tile> spent = new List<Tile> { };
        foreach(Hazard h in DungeonManager.instance.currentDungeon.currentEncounter.enemyHazards) foreach (Tile t in h.tiles) if (!hazardTiles.Contains(t)) hazardTiles.Add(t);
        spent.Add(start);
        while (theTile==null)
        {
            foreach (Tile t in spent.ToList()) 
            {
                foreach (Tile tile in t.neighbor) 
                {
                    if (!hazardTiles.Contains(tile)) return t;
                    if (!spent.Contains(tile))spent.Add(tile);
                }
            }
        }
        return theTile;
    }
    public Tile FindClosestTileNotInHazardTowardsBoss(Tile start, Tile bossTile)
    {
        return null;
    }
    public List<Tile> FindTilesAroundBoss(Tile bossTile)
    {
        List<Tile> list = new List<Tile> { };
        foreach (Tile t in bossTile.neighbor) if (t.OccupiedBy() == null) list.Add(t);
        return list;
    }
    public List<Tile> FindBackWallTiles()
    {
        List<Tile> list = new List<Tile> { };
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if ((t.x==1||t.y==1||t.x== DungeonManager.instance.currentDungeon.currentEncounter.arenaSizeX-1|| t.y == DungeonManager.instance.currentDungeon.currentEncounter.arenaSizeY - 1) && t.OccupiedBy() == null) list.Add(t);
        return list;
    }
    public bool IsAdjacent(Tile a, Tile b)
    {
        foreach (Tile t in b.neighbor) if (a.x == t.x && a.y == t.y) return true;
        return false;
    }
    public Tile One(Tile current)
    {
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.x == current.x - 1 && t.y == current.y - 1) return t;
        return null;
    }
    public Tile Two(Tile current)
    {
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.x == current.x && t.y == current.y - 1) return t;
        return null;
    }
    public Tile Three(Tile current)
    {
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.x == current.x + 1 && t.y == current.y - 1) return t;
        return null;
    }
    public Tile Four(Tile current)
    {
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.x == current.x - 1 && t.y == current.y) return t;
        return null;
    }
    public Tile Six(Tile current)
    {
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.x == current.x + 1 && t.y == current.y ) return t;
        return null;
    }
    public Tile Seven(Tile current)
    {
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.x == current.x - 1 && t.y == current.y + 1) return t;
        return null;
    }
    public Tile Eight(Tile current)
    {
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.x == current.x && t.y == current.y + 1) return t;
        return null;
    }
    public Tile Nine(Tile current)
    {
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.x == current.x + 1 && t.y == current.y + 1) return t;
        return null;
    }
    public Tile Otherside(Tile source,Tile current)
    {
        if (Nine(current) == source) return One(current);
        if (Eight(current) == source) return Two(current);
        if (Seven(current) == source) return Three(current);
        if (Six(current) == source) return Four(current);
        if (Four(current) == source) return Six(current);
        if (Three(current) == source) return Seven(current);
        if (Two(current) == source) return Eight(current);
        if (One(current) == source) return Nine(current);
        return null;
    }
    public Tile Location(Transform a, List<Tile> tileList)
    {
        foreach (Tile loc in tileList) if (loc.x == a.position.x && loc.y == a.position.y) return loc;
        return null;
    }
    public Tile Location(Transform a)
    {
        foreach (Tile loc in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (loc.x == a.position.x && loc.y == a.position.y) return loc;
        return null;
    }
    public Tile Location(Vector2 position)
    {
        foreach (Tile loc in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (loc.x == position.x && loc.y == position.y) return loc;
        return null;
    }
    public Tile Location(int x, int y, List<Tile> tileList)
    {
        foreach (Tile loc in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (loc.x == x && loc.y == y) return loc;
        return null;
    }
    public Tile Location(int x, int y)
    {
        foreach (Tile loc in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (loc.x == x && loc.y == y) return loc;
        return null;
    }
    internal Tile OppositeTile(Tile defenderTile, Tile attackerTile)
    {
        int x = attackerTile.x - defenderTile.x;
        int y = attackerTile.y - defenderTile.y;
        foreach (Tile l in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (l.x == defenderTile.x - x && l.y == defenderTile.y - y) return l;
        return null;
    }
    public Tile FindTileBesideTargetAlsoInRangeOfOtherTargets(Tile targetTile)
    {
        List<Tile> list = new List<Tile> { };
        foreach (Tile t in targetTile.neighbor) if (t.OccupiedBy() == null) list.Add(t);
        foreach(Tile t in list)
        {
            foreach(Tile tile in t.neighbor)
            {
                if (tile.OccupiedBy() != null && tile != targetTile) return tile;
            }
        }
        return null;
    }
    public List<Tile> TilesInRange(int range,Tile tile)
    {
        List<Tile> list = new List<Tile> {tile };
        for (int i = 0; i < range; i++) foreach (Tile t in list.ToList()) foreach (Tile n in t.neighbor) if (Cardinal(n, t) && !list.Contains(n)) list.Add(n);
        return list;
    }

    private bool Cardinal(Tile n, Tile t)
    {
        if (n.x == t.x + 1 && (n.y == t.y + 1 || n.y == t.y - 1)) return false;
        if (n.x == t.x - 1 && (n.y == t.y + 1 || n.y == t.y - 1)) return false;
        return true;
    }
    public float Distance(float x1, float x2, float y1, float y2)
    {
        float x = Math.Abs(x1 - x2);
        float y = Math.Abs(y1 - y2);
        x *= x;
        y *= y;
        return (float)Math.Sqrt(x + y);
    }
    public float Distance(Tile tile, Tile tile2)
    {
        return Distance(tile.x, tile2.x, tile.y, tile2.y);
    }
    public float Distance(Vector2 tile, Vector2 tile2)
    {
        return Distance(tile.x, tile2.x, tile.y, tile2.y);
    }
}
