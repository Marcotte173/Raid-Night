using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : Channel
{
    public override void Projectile()
    {
        Projectile p = Instantiate(GameObjectList.instance.playerProjectile,character.transform);
        p.transform.position = character.transform.position;
        p.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.arcaneMissile;
        p.speed = 7f;
        p.damage = character.spellpower.value * damage / 100;
        p.aggro = Utility.instance.Threat(p.damage, threat);
        p.target = target;
        p.projectileName = "Missile";
        p.attacker = character;
    }
}
