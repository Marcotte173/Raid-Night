using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneShackles : Ability
{
    public float tendrilCount;
    public float tendrilDuration;
    public float attackSpeedDown;
    public float movementSpeedDown;
    public override void Effect()
    {
        if (AbilityCheck.instance.ArcaneTendril(character.target) == null) ArcaneShacklesDot();
        else AbilityCheck.instance.ArcaneTendril(character.target).ResetTimer();
        float dam = character.spellpower.value * damage / 100;
        for (int i = 0; i < tendrilCount; i++) target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), false, "Arcane Shackles: ");
    }
    public void ArcaneShacklesDot()
    {
        ArcaneTendrils r = Instantiate(GameObjectList.instance.arcaneTendrils, target.transform);
        r.attacker = character;
        r.timer = tendrilDuration;
        r.target = character.target;
        r.hasteChange = attackSpeedDown;
        r.moveChange = movementSpeedDown;
        r.Timer();
    }
}
