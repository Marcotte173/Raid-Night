using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blight : Ability
{
    public float blightRotDamage;
    public float blightSnakeBiteDamage;
    public override void Effect()
    {
        float dotBonus = 0;
        if (AbilityCheck.instance.MyRot(target, character) != null)
        {
            RotDot r = AbilityCheck.instance.MyRot(target, character);
            target.debuff.Remove(r);
            DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(r.gameObject);
            Destroy(r.gameObject.gameObject);
            dotBonus += blightRotDamage;
        }
        if (AbilityCheck.instance.MySnakeBite(target, character) != null)
        {
            SnakeBiteDot r = AbilityCheck.instance.MySnakeBite(target, character);
            target.debuff.Remove(r);
            DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(r.gameObject);
            Destroy(r.gameObject.gameObject);
            dotBonus += blightSnakeBiteDamage;
        }
        float dam = character.spellpower.value * (damage + dotBonus) / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), false, "Blight: ");
    }
}
