using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disintigrate : Ability
{
    public override void Effect()
    {
        Animator r = Instantiate(GameObjectList.instance.animator, target.transform);
        r.GetComponent<DestroyOverTime>().time = 0.5f;
        r.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);
        r.runtimeAnimatorController = GameObjectList.instance.disintigrate;
        float dam = character.spellpower.value * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), false, "Disintigrate: ");
    }
}
