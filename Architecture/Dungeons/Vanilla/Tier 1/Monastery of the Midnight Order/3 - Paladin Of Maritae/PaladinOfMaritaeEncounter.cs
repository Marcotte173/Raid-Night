using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinOfMaritaeEncounter : EncounterFeatures
{
    public List<int> x;
    public List<int> y;
    public override void SpawnPOI()
    {
        base.SpawnPOI();
        Pillar(x[0], y[0]);
        Pillar(x[0], y[0]+1);
        Pillar(x[0], y[0]+2);
        Pillar(x[0] +1, y[0]);
        Pillar(x[0] +2, y[0]);
        

        Pillar(x[0], y[1]-2);
        Pillar(x[0], y[1]-1);
        Pillar(x[0], y[1]);
        Pillar(x[0]+1, y[1]);
        Pillar(x[0]+2, y[1]);

        Pillar(x[1]-2, y[0]);
        Pillar(x[1]-1, y[0]);
        Pillar(x[1], y[0]);
        Pillar(x[1], y[0]+1);
        Pillar(x[1], y[0]+2);

        Pillar(x[1]-2, y[1]);
        Pillar(x[1]-1, y[1]);
        Pillar(x[1], y[1]);
        Pillar(x[1], y[1]-1);
        Pillar(x[1], y[1]-2);
    }

    private void Pillar(int v1, int v2)
    {
        List<Tile> tileList = GetComponent<Encounter>().tileList;
        foreach (Tile t in tileList)
        {
            if (t.x == v1 && t.y == v2)
            {
                t.id = 2;
                t.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.arcanosPillar;
                t.pic = SpriteList.instance.arcanosPillar;
                break;
            }
        }

    }
}
