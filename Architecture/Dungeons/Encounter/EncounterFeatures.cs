using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EncounterFeatures : MonoBehaviour
{  
    public virtual void SpawnPOI()
    {
        List<Tile> tileList = GetComponent<Encounter>().tileList;
        int arenaSizeX = GetComponent<Encounter>().arenaSizeX;
        int arenaSizeY = GetComponent<Encounter>().arenaSizeY;
        foreach (Tile t in tileList)
        {
            if (t.x == 0 || t.x == arenaSizeX - 1 || t.y == 0 || t.y == arenaSizeY - 1)
            {
                t.GetComponent<SpriteRenderer>().sortingLayerName = "Wall";
                t.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.greyArena[2];
                t.pic = SpriteList.instance.greyArena[2];
                t.id = 1;
            }
            else
            {
                t.pic = SpriteList.instance.greyArena[0];
                t.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.greyArena[0];
            }
        }
    }
}
