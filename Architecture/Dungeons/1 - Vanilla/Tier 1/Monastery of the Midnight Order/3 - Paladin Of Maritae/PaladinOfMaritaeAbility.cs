using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class PaladinOfMaritaeAbility : Ability
{
    public float width;
    public float length;
    public float timer;
    public List<float> threshhold;
    public Sprite groundPic;
    [HideInInspector]
    public List<Tile> groundTiles;
    public float groundEffectTimer;
    public float groundEffectTime;
    [HideInInspector]
    public List<Tile> current;
    [HideInInspector]
    public List<Tile> next;
    [HideInInspector]
    public List<Tile> remaining;
    public Color32 preCastColor = new Color32(74, 229, 71, 255);
    public Color32 castColor = new Color32(243, 15, 15, 255);
    public bool effect;
    private void Start()
    {
        groundEffectTimer = groundEffectTime;
        cooldownTimer = 3;
    }
    public override void ComputerUse()
    {
        character.action = $"Moving";
        rangeToTarget = Vector3.Distance(character.transform.position, target.transform.position);
        if (InRange(rangeRequired))
        {            
            character.actionCast = character.castTimer = character.CastTimer(castTime);
            cast = true;
            Tile t = character.move.currentTile;
            groundTiles.Clear();
            foreach (Tile tile in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.LOS(tile) && tile.id == 0) groundTiles.Add(tile);
            foreach (Tile tile in groundTiles)tile.GetComponent<SpriteRenderer>().sprite = groundPic;
            character.state = DecisionState.Cast;                       
        }
    }

    public override void UpdateStuff()
    {        
        if (effect)
        {
            if (remaining.Count > 0)
            {
                groundEffectTimer -= Time.deltaTime;
                if (groundEffectTimer <= 0)
                {
                    groundEffectTimer = groundEffectTime;
                    foreach (Tile t in current.ToList())
                    {
                        foreach (Tile tile in remaining.ToList())
                        {
                            if (t.neighbor.Contains(tile))
                            {
                                remaining.Remove(tile);
                                next.Add(tile);
                                FX(tile);
                            }
                        }
                    }
                    current = next;
                }
            }
            else
            {
                remaining.Clear();
                next.Clear();
                current.Clear();
                effect = false;
                foreach (Tile tile in DungeonManager.instance.currentDungeon.currentEncounter.tileList) FX(tile);
            }   
        }
    }

    private void FX(Tile tile)
    {
        tile.GetComponent<SpriteRenderer>().sprite = tile.pic;
    }

    public override void Effect()
    {
        effect = true;
        foreach (Character p in DungeonManager.instance.currentDungeon.currentEncounter.player) if (groundTiles.Contains(p.move.currentTile) && !p.ko) p.TakeDamage(character, damage, true, "Earth Shaker");
        remaining = groundTiles;
        foreach (Tile tile in remaining.ToList())
        {
            if (character.move.currentTile.neighbor.Contains(tile))
            {
                remaining.Remove(tile);
                current.Add(tile);
                FX(tile);
            }
        }
        //foreach (Tile tile in groundTiles) tile.GetComponent<SpriteRenderer>().sprite = tile.pic;
        Utility.instance.DamageNumber(character, "EARTH SHAKER", SpriteList.instance.bad);       
        //EarthShaker
    }
}