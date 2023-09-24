using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScharAbility : Ability
{
    public int abilityChoice = 0;
    public List<Vector2> spots;
    public float hotTime;
    private void Start()
    {
        cooldownTimer = 8;
    }
    public override void Effect()
    {
        if(abilityChoice == 0)
        {
            character.target= target = null;
            verb = "Flurry Of Blades";
            character.transform.position = spots[Random.Range(0,spots.Count)];
            abilityChoice = 1;
        }
        else
        {
            verb = "Shadowstalk";
            FuryOfBladesBuff f = Instantiate(GameObjectList.instance.furyOfBlades, character.transform);
            f.attacker = f.target = character;
            f.timer = hotTime;
            f.damage = damage;
            f.Timer();
            abilityChoice = 0;
        }        
    }
}
