using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Projectile
{
    public override void ProjectileEffect()
    {
        if (target.GetComponent<Class>()) target.GetComponent<Class>().TakeDamage(attacker, damage, false,"");
        if (aoe)
        {
            List<Character> targets = new List<Character> { };
            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Characters()) if (a.GetComponent<Class>() && a != target) targets.Add(a);
            if (targets.Count > 0) foreach (Character a in targets) if (Vector2.Distance(a.transform.position, target.transform.position) < range) a.GetComponent<Class>().TakeDamage(attacker, damage, false, "");
        }
        if (effectAnimator != null)
        {
            Animator r = Instantiate(GameObjectList.instance.animator);
            r.GetComponent<DestroyOverTime>().time = 0.5f;
            r.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);
            r.runtimeAnimatorController = effectAnimator;
            r.transform.localScale = new Vector3(range * 3, range * 3, range * 3);
        }
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
