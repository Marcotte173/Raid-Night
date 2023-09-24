using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : Projectile
{
    public override void ProjectileEffect()
    {
        if (target.GetComponent<Boss>()) target.GetComponent<Boss>().TakeDamage(attacker, damage, aggro, false, projectileName + ": ");        
        if (aoe)
        {
            List<Character> targets = new List<Character> { };
            foreach(Character a in DungeonManager.instance.currentDungeon.currentEncounter.Characters()) if (a.GetComponent<Boss>() && a != target) targets.Add(a);
            if (targets.Count > 0) foreach (Character a in targets) if (Vector2.Distance(a.transform.position, target.transform.position) < range) a.GetComponent<Boss>().TakeDamage(attacker, aoeDamage, aggro, false, projectileName + ": ");
        }
        if (effectAnimator != null && DungeonManager.instance.raidMode == RaidMode.Combat)
        {
            Animator r = Instantiate(GameObjectList.instance.animator);
            DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(r.gameObject);
            r.GetComponent<DestroyOverTime>().time = 0.7f;
            r.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);
            r.runtimeAnimatorController = effectAnimator;
            r.transform.localScale = new Vector3(effectScale, effectScale, effectScale);
        }
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
