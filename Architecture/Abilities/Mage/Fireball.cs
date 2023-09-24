using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    public float aoeDamage;
    public float explosionRange;
    public float effectScale;
    public override void Effect()
    {
        Projectile p = Instantiate(GameObjectList.instance.playerProjectile, character.transform);
        p.transform.position = character.transform.position;
        p.GetComponent<Animator>().runtimeAnimatorController = GameObjectList.instance.fireball;
        p.effectAnimator = GameObjectList.instance.explosion;
        p.effectScale = effectScale;
        p.speed = 7f;
        p.damage = character.spellpower.value * damage / 100;
        p.aoeDamage = character.spellpower.value * aoeDamage / 100;
        p.aggro = Utility.instance.Threat(p.damage, threat);
        p.aoe = true;
        p.range = explosionRange;
        p.target = target;
        p.attacker = character;
    }
}