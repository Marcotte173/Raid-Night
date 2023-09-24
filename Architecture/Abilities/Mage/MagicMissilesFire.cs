using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissilesFire : Channel
{
    public float aoeDamage;
    public float explosionRange;
    public float effectScale;
    public override void Projectile()
    {
        Projectile p = Instantiate(GameObjectList.instance.playerProjectile, character.transform);
        p.transform.position = character.transform.position;
        p.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.arcaneAoeMissile;
        p.effectAnimator = GameObjectList.instance.explosion;
        p.speed = 7f;
        p.effectScale = effectScale;
        p.damage = character.spellpower.value * damage / 100;
        p.aggro = Utility.instance.Threat(p.damage, threat);
        p.aoe = true;
        p.aoeDamage = character.spellpower.value * aoeDamage / 100;
        p.range = explosionRange;
        p.target = target;
        p.projectileName = "Missile";
        p.attacker = character;
    }
}
