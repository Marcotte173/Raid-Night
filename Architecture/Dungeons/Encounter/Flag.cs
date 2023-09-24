using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public int id;
    public Tile home;
    public int x;
    public int y;
    public bool drag;
    public bool canDrag;
    Vector2 mousePosition;
    Vector2 objPosition;

    private void OnMouseDrag()
    {
        if (DungeonManager.instance.raidMode == RaidMode.Setup && UserControl.instance.mouseTile.OccupiedBy() == null)
        {
            foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.possibleFlagTiles)
            {
                if (t == UserControl.instance.mouseTile)
                {
                    t.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.flagGroundColor[id * 2];
                    t.flag = (TileFlag)id;
                }
                else if (UserControl.instance.mouseTile.neighbor.Contains(t))
                {
                    foreach (Tile tile in t.neighbor)
                    {
                        tile.flag = (TileFlag)id;
                        t.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.flagGroundColor[id * 2 + 1];
                    }
                }
                else t.GetComponent<SpriteRenderer>().sprite = t.unselected;
            }
            mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector2(objPosition.x, objPosition.y);
        }
    }
    private void OnMouseUp()
    {   
        if(DungeonManager.instance.raidMode == RaidMode.Setup && UserControl.instance.mouseTile.OccupiedBy() == null)
        {
            foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.possibleFlagTiles)
            {
                t.GetComponent<SpriteRenderer>().sprite = t.pic;
                if (t == UserControl.instance.mouseTile)
                {
                    if (id == 0)
                    {
                        DungeonManager.instance.currentDungeon.currentEncounter.blueFlagTiles.Clear();
                        DungeonManager.instance.currentDungeon.currentEncounter.blueFlagTiles.Add(t);
                        foreach(Tile tile in t.neighbor) if(tile.id==0)DungeonManager.instance.currentDungeon.currentEncounter.blueFlagTiles.Add(tile);
                    }
                    else if (id == 1)
                    {
                        DungeonManager.instance.currentDungeon.currentEncounter.redFlagTiles.Clear();
                        DungeonManager.instance.currentDungeon.currentEncounter.redFlagTiles.Add(t);
                        foreach (Tile tile in t.neighbor) if (tile.id == 0) DungeonManager.instance.currentDungeon.currentEncounter.redFlagTiles.Add(tile);
                    }
                    else if (id == 2)
                    {
                        DungeonManager.instance.currentDungeon.currentEncounter.greenFlagTiles.Clear();
                        DungeonManager.instance.currentDungeon.currentEncounter.greenFlagTiles.Add(t);
                        foreach (Tile tile in t.neighbor) if (tile.id == 0) DungeonManager.instance.currentDungeon.currentEncounter.greenFlagTiles.Add(tile);
                    }
                    else if (id == 3)
                    {
                        DungeonManager.instance.currentDungeon.currentEncounter.yellowFlagTiles.Clear();
                        DungeonManager.instance.currentDungeon.currentEncounter.yellowFlagTiles.Add(t);
                        foreach (Tile tile in t.neighbor) if (tile.id == 0) DungeonManager.instance.currentDungeon.currentEncounter.yellowFlagTiles.Add(tile);
                    }
                }
                if (DungeonManager.instance.currentDungeon.currentEncounter.possibleFlagTiles.Contains(UserControl.instance.mouseTile)) NewPosition();
                else HomePosition();
            }   
        }        
    }

    public void NewPosition()
    {
        if(DungeonManager.instance.raidMode == RaidMode.Setup) transform.position = UserControl.instance.mouseTile.transform.position;
    }
    public void HomePosition()
    {        
        transform.position = home.transform.position;
    }
}
