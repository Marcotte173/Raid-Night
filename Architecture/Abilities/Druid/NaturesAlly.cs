using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturesAlly : Ability
{
    [HideInInspector]
    public List<Tile> summonTiles;
    public float howManyWolves;
    public float wolfHp;
    public float wolfDamage;
    public float wolfDefence;
    public float wolfCrit;
    public float duration;
    public float moveSpeed;
    public float wolfAttackSpeed;
    public override void Effect()
    {
        summonTiles.Clear();
        for (int i = 0; i < howManyWolves; i++) NaturesAllySummonWolf();
    }
    public void NaturesAllySummonWolf()
    {
        Wolf w = Instantiate(GameObjectList.instance.wolf, character.transform);
        w.move = w.GetComponent<Move>();
        w.transform.SetParent(DungeonManager.instance.currentDungeon.currentEncounter.transform);
        w.move.character = w;
        DungeonManager.instance.currentDungeon.currentEncounter.playerMinionSummons.Add(w);
        w.transform.SetParent(DungeonManager.instance.currentDungeon.currentEncounter.transform);
        Tile t = FindTile.instance.FindClosestUnoccupiedTileAdjacentToTarget(target.move.currentTile, character.move.currentTile, summonTiles);
        summonTiles.Add(t);
        w.transform.position = new Vector2(t.x, t.y);
        w.move.CurrentTile();
        w.move.on = true;
        w.characterName = "Wolf " + summonTiles.Count.ToString();
        w.move.prevPosition = w.transform.position;
        w.GetComponent<Summon>().time = duration;
        w.GetComponent<Summon>().summoner = character;
        w.maxHealth.baseValue = w.GetComponent<Wolf>().maxHealth.baseValue = wolfHp;
        w.movement.baseValue = moveSpeed;
        w.defence.baseValue = wolfDefence;
        w.damage.baseValue = wolfDamage;
        w.crit.baseValue = wolfCrit;
        w.AbilityAdd(AbilityList.instance.basicAttack);
        w.ability[0].character = w;
        w.ability[0].damage = wolfDamage;
        w.ability[0].castTime = wolfAttackSpeed;
        w.name = $"Wolf";
    }
}
