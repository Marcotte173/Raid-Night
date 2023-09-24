using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : Ability
{
    public float dotTime;
    public override void Effect()
    {        
        if (AbilityCheck.instance.SetupCount(character) > 0) AbilityCheck.instance.MySetup(character).AddCount(1);
        else SetupBuff(1);
        float dam = character.Damage() * damage / 100;
        target.GetComponent<Boss>().TakeDamage(character, dam, Utility.instance.Threat(dam, threat), true, "Setup: ");
    }
    public void SetupBuff(int x)
    {
        SetupBuff r = Instantiate(GameObjectList.instance.setup, transform);
        r.timer = dotTime;
        r.Timer();
        r.target = character;
        r.AddCount(x);
    }
}
