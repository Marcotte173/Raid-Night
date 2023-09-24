using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Caltrops : AbilityHazard
{    
    public List<float> threshhold;
    public float caltropsLength;
    public float caltropsDebuffLength;
    public float moveChange;

     public override void TriggerHazard()
    {
        CaltropHazard cal = Instantiate(GameObjectList.instance.caltropHazard);
        cal.transform.position = hazardPosition;
        cal.moveChange = moveChange;
        cal.threshHold = threshhold.ToList();
        cal.timer = caltropsLength;
        cal.caltropLength = caltropsDebuffLength;
        cal.Timer();
        cal.transform.localScale = new Vector3(width, length);
        cal.damage = damage * character.Damage() / 100;
        cal.threat = threat;
        cal.attacker = character;
    }
}