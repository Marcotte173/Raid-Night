using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaladinOfAkalosSummonAbility : Ability
{
    public float howManyminions;
    public List<Vector2> summonPosition;
    public float minionHp;
    public float minionDamage;
    public float minionDefence;
    public float minionCrit;
    public float moveSpeed;
    public float minionAttackSpeed;
    private void Start()
    {
        cooldownTimer = 2;
    }
    public override void Effect()
    {
        Utility.instance.DamageNumber(character, "To Me!", SpriteList.instance.bad);
        for (int i = 0; i < howManyminions; i++) SummonMinion(i);
    }

    public void SummonMinion(int pos)
    {
        //Summon Ability
        PaladinOfAkalos b = (PaladinOfAkalos)character;
        Minion w = Instantiate(b.minion, b.transform);
        w.move = w.GetComponent<Move>();
        w.transform.SetParent(DungeonManager.instance.currentDungeon.currentEncounter.transform);
        w.move.character = w;
        DungeonManager.instance.currentDungeon.currentEncounter.bossMinionSummons.Add(w);
        w.transform.SetParent(DungeonManager.instance.currentDungeon.currentEncounter.transform);
        //Tile t = FindTile.instance.FindClosestUnoccupiedTileAdjacentToTarget(target.move.currentTile, character.move.currentTile, summonTiles);
        //summonTiles.Add(t);
        w.transform.position = FindTile.instance.Location(summonPosition[pos]).transform.position;
        w.move.CurrentTile();
        w.move.on = true;
        w.playerUI = w.GetComponent<CharacterUI>();
        w.AbilityAdd(AbilityList.instance.basicAttack);
        w.ability[0].character = w;
        w.characterName = "Minion " + (pos+1).ToString();
        w.move.prevPosition = w.transform.position;
        w.health = w.maxHealth.baseValue = w.GetComponent<Minion>().maxHealth.baseValue = minionHp;
        w.movement.baseValue = moveSpeed;
        w.defence.baseValue = minionDefence;
        w.damage.baseValue = minionDamage;
        w.crit.baseValue = minionCrit;
        w.ability[0].castTime = minionAttackSpeed;
        w.name = $"Minion";
    }
}
