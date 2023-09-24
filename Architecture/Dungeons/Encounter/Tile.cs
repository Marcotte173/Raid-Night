using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum TileFlag {None,Blue,Red,Green,Yellow };

public class Tile : MonoBehaviour
{
    public RaycastHit2D[] hit;
    public List<Tile> hitTile;
    public int x;
    public int y;
    public float g;
    public float h;
    public float f;
    public Sprite pic;    
    public Sprite unselected;
    public Sprite characterMove;
    public Tile parent;
    public List<Tile> neighbor;
    public int cost;
    public int id;
    public TileFlag flag;
    public bool collide;
    public Character OccupiedBy()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Characters())
        {
            if (Mathf.Round(a.transform.position.x) == this.x && Mathf.Round(a.transform.position.y) == this.y) return a;
        }
        return null;
    }
    public bool LOS(Tile target)
    {
        hitTile.Clear();
        hit = Physics2D.RaycastAll(transform.position, target.transform.position - transform.position, Vector2.Distance(transform.position, target.transform.position));
        if (hit.Length > 0)
        {
            {
                foreach (RaycastHit2D h in hit) if (h.collider.GetComponent<Tile>())
                {
                    hitTile.Add(h.collider.GetComponent<Tile>());
                }
            }
        }
        else return false;
        foreach (Tile t in hitTile)
        {
            if(t.id == 2)
            {
                Debug.Log(t.name);
                Debug.Log("NO LOS!");
                return false;
            }
        }           
        return true;
    }
}