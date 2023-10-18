using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyodelOfTheMammothRiderAbility : Ability
{
    public float minionHp;
    public float minionDamage;
    public float minionDefence;
    public float minionCrit;
    public float moveSpeed;
    public float minionAttackSpeed;
    public List<Character> affectedPlayer;
    public int secondPhaseDamage;
    private void Start()
    {
        cooldownTimer = 6;
    }
    public override void Effect()
    {
        Character c = Target.instance.Farthest(character, DungeonManager.instance.currentDungeon.currentEncounter.Player());
        affectedPlayer.Add(character.target);
        affectedPlayer.Add(c);
        target = character.target = character.dashCastTarget = c;
        cast = false;
        ProtectionDash();
    }
    private void ProtectionDash()
    {
        character.state = DecisionState.DashCast;
        dash = true;
        character.dashDirect = true;
    }
    public override void DashTrigger()
    {
        dash = false;
        character.dashDirect = false;
        foreach(Character c in affectedPlayer) c.TakeDamage(character, damage, true, "Charge!");
        if(!affectedPlayer[0].ko)character.target = affectedPlayer[0];
        character.state = DecisionState.Downtime;
    }

    public void SummonMinion()
    {
        //Summon Ability
        AyodelOfTheMammothRiders a = (AyodelOfTheMammothRiders)character;
        a.phase = 1;
        a.damage.baseValue = secondPhaseDamage;
        a.GetComponent<SpriteRenderer>().sprite = a.pic[a.phase];
        Utility.instance.DamageNumber(character, "Dismount!", SpriteList.instance.bad);
        AminarisGiridak w = Instantiate(a.minion, a.transform);
        w.move = w.GetComponent<Move>();
        w.transform.SetParent(DungeonManager.instance.currentDungeon.currentEncounter.transform);
        w.move.character = w;
        DungeonManager.instance.currentDungeon.currentEncounter.bossMinionSummons.Add(w);
        w.transform.SetParent(DungeonManager.instance.currentDungeon.currentEncounter.transform);
        Tile t = FindTile.instance.FindClosestUnoccupiedTileAdjacentToTarget(target.move.currentTile, character.move.currentTile);
        w.transform.position = t.transform.position;
        w.move.CurrentTile();
        w.move.on = true;
        w.AbilityPopulate();
        w.playerUI = w.GetComponent<CharacterUI>();
        w.AbilityAdd(AbilityList.instance.basicAttack);
        w.ability[0].character = w;
        w.characterName = "Aminaris Giridak";
        w.move.prevPosition = w.transform.position;
        w.health = w.maxHealth.baseValue = w.GetComponent<Boss>().maxHealth.baseValue = minionHp;
        w.movement.baseValue = moveSpeed;
        w.defence.baseValue = minionDefence;
        w.damage.baseValue = minionDamage;
        w.crit.baseValue = minionCrit;
        w.ability[0].castTime = minionAttackSpeed;
        w.name = "Aminaris Giridak";
    }
}
