using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static Target instance;
    private void Awake()
    {
        instance = this;
    }

    public Character Closest(Character a, List<Character> list)
    {
        Character target = list[0];
        //Calculate Distance to target
        float distance = FindTile.instance.Distance(a.transform.position, target.transform.position);
        //Check each other in the list
        foreach (Character c in list)
        {
            if (c.untargetable) continue;
            //If the distance to them is shorter
            if (FindTile.instance.Distance(a.transform.position, c.transform.position) < FindTile.instance.Distance(a.transform.position, target.transform.position)) target = c;
        }
        return target;
    }
    public Character Farthest(Character a,List<Character> list)
    {
        Character target = list[0];
        //Check each other in the list
        foreach (Character c in list)
        {      
            if (c.untargetable) continue;
            //If the distance to them is shorter
            if (FindTile.instance.Distance(a.transform.position, c.transform.position) > FindTile.instance.Distance(a.transform.position, target.transform.position)) target = c;
        }
        return target;
    }
    public Character HighestAggro(List<Aggro> list)
    {
        Aggro target = list[0];
        //Calculate Distance to target
        float aggro = target.aggro;
        //Check each other in the list
        foreach (Aggro c in list)
        {
            //If the distance to them is shorter
            if (c.aggro > aggro)
            {
                //They are the target, new distance
                target = c;
                aggro =c.aggro;
            }
        }
        return target.agent;
    }
    public Character LowHealth(List<Character> list)
    {
        Character target = list[0];
        float health = target.Health();
        //Check each other in the list
        foreach (Character c in list)
        {
            if (c.untargetable) continue;
            //If the distance to them is shorter
            if (c.Health() < health)
            {
                //They are the target, new health
                target = c;
                health = c.Health();
            }
        }
        return target;
    }

    internal Character NoAggroRandom(Character character, List<Character> player)
    {
        Boss b = (Boss)character;
        List<Character> potential = player.ToList();
        if (b.aggro.Count>0) potential.Remove(b.aggro[0].agent);
        return potential[UnityEngine.Random.Range(0, potential.Count)];
    }
}