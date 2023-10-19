using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Character target;
    public float speed;
    public float damage;
    public float aoeDamage;
    public float aggro;
    public Character attacker;
    public RuntimeAnimatorController projectileAnimator;
    public RuntimeAnimatorController effectAnimator;
    public float effectScale;
    public bool aoe;
    public string projectileName;
    public float range;
    public AudioClip soundClip;
    private void Start()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public void UpdateProjectile()
    {
        if (target != null && !target.ko && DungeonManager.instance.raidMode == RaidMode.Combat)
        {
            transform.right = target.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * UnityEngine.Time.deltaTime);
            if (FindTile.instance.Distance(transform.position, target.transform.position) < 0.3f)
            {
                SoundManager.instance.PlayEffect(soundClip);
                ProjectileEffect();
            }
        }
        else
        {
            DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void ProjectileEffect()
    {
        if (target.GetComponent<Boss>()) target.GetComponent<Boss>().TakeDamage(attacker, damage, aggro,false, projectileName + ": ");
        else if (target.GetComponent<Class>()) target.GetComponent<Class>().TakeDamage(attacker, damage,false,"");
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
