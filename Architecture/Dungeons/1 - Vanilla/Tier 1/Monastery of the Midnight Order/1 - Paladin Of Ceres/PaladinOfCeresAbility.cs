using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaladinOfCeresAbility : Ability
{
    public float width;
    public float length;
    public float timer;
    public float fireBlastDamage;
    public int fireBlasts;
    public List<float> threshhold;
    public float abilityChoice;
    public float test;
    
    private void Start()
    {
        abilityChoice = 1;
        cooldownTimer = 0;
    }


    public override void Effect()
    {
        if (abilityChoice == 0)
        {
            character.target = target = null;
            verb = "Cleansing Flame";
            Utility.instance.DamageNumber(character, "Fire Blast", SpriteList.instance.bad);
            //FireBlast Ability
            int maxBlast = (DungeonManager.instance.currentDungeon.currentEncounter.Player().Count >= fireBlasts) ? fireBlasts : DungeonManager.instance.currentDungeon.currentEncounter.Player().Count;
            List<Character> targets = new List<Character> { };
            while (maxBlast > 0)
            {
                Character a = DungeonManager.instance.currentDungeon.currentEncounter.Player()[Random.Range(0, DungeonManager.instance.currentDungeon.currentEncounter.Player().Count)];
                if (!targets.Contains(a))
                {
                    targets.Add(a);
                    maxBlast--;
                }
            }
            foreach (Character a in targets)
            {
                Projectile p = Instantiate(GameObjectList.instance.enemyProjectile, character.transform);
                p.transform.position = character.transform.position;
                p.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.fireblast;
                p.speed = 7f;
                p.damage = fireBlastDamage;
                p.target = a;
                p.projectileName = "Fire Blast";
                p.attacker = character;
            }
            abilityChoice = 1;
        }
        else
        {
            Utility.instance.DamageNumber(character, "Cleansing Flame", SpriteList.instance.bad);
            verb = "FireBlast";
            //Circle of Flames Ability
            OrcHazard r = Instantiate(GameObjectList.instance.orcHazard, DungeonManager.instance.currentDungeon.currentEncounter.transform);
            r.transform.position = FindTile.instance.TileClosestToMostTargets(DungeonManager.instance.currentDungeon.currentEncounter.tileList, DungeonManager.instance.currentDungeon.currentEncounter.player,test).transform.position;
            r.timer = timer;
            r.threshHold = threshhold.ToList();
            r.damage = damage;
            r.attacker = character;
            r.threat = threat;
            r.transform.localScale = new Vector3(width, length);
            r.Timer();
            abilityChoice = 0;
        }
    }
}
