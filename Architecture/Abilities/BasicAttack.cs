using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability
{
    public bool dual;
    public override void Effect()
    {
        if (character.GetComponent<Class>())
        {
            target.TakeDamage((character.GetComponent<Summon>()) ? character.GetComponent<Summon>().summoner : character, character.Damage(), Utility.instance.Threat(character.Damage() * character.physicalDamageMod.value, threat), true, "");
        }
        else
        {
            character.target.TakeDamage(character, character.Damage(), true, "");
        }
        cast = false;
        character.state = DecisionState.Downtime;
        if(character.GetComponent<Beserker>()) character.ManaGain(character.GetComponent<Beserker>().energyGainOnHit);
        cooldownTimer = character.CastTimer(cooldownTime);
    }

    public override void StartBattle()
    {
        if (character.GetComponent<Class>() && !character.GetComponent<Summon>())
        {
            castTime = character.weapon.attackSpeed* (100/(100+ character.offHand.attackSpeed));
            rangeRequired = character.weapon.range;
            pic = character.weapon.pic;
            threat = (character.GetComponent<Class>().spec == Spec.Stalwart) ? 500 : 100;
            if (dual && AbilityCheck.instance.DualWield(character)!= null)
            {
                DualWield r = Instantiate(GameObjectList.instance.dualWield, character.transform);
                r.attacker = character;
                r.damage = character.offHand.attackSpeed;
                r.target = character;
            }
        }        
    }
}
