using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Character
{
    public List<Aggro> aggro;
    public int startX;
    public int startY;
    public float attackSpeed;
    public string flavor;

    public override void AbilityPopulate()
    {
        AbilityLoad(AbilityList.instance.boss);        
    }
    public override void Create()
    {
        move = GetComponent<Move>();
        move.character = this;
        Equipment();
    }
    public virtual List<Ability> AbilityListReturn() { return null; }
    public override void Decision()
    {
       
    }
    public override void UpdateStuff()
    {
        base.UpdateStuff();
        foreach (Aggro a in aggro.ToList()) if (a.agent.ko) aggro.Remove(a);
        if (aggro.Count > 1) Utility.instance.SortAggro(aggro);
    }

    public override void GetTarget()
    {
        if (targetFromTaunt != null) target = targetFromTaunt;
        else if (aggro.Count > 1)
        {
            target = aggro[0].agent;
        }
        else
        {
            target = Target.instance.Closest(this,DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion());
        }
        foreach (Ability a in ability) a.target = target;
    }    
    
    public virtual void Equipment()
    {
        Equip.instance.Item(ItemList.instance.noItem, head);
        Equip.instance.Item(ItemList.instance.noItem, chest);
        Equip.instance.Item(ItemList.instance.noItem, legs);
        Equip.instance.Item(ItemList.instance.noItem, feet);
        Equip.instance.Item(ItemList.instance.noItem, trinket);
        Equip.instance.Item(ItemList.instance.noItem, weapon);
        Equip.instance.Item(ItemList.instance.noItem, offHand);
    }
    public void NewPosition(float newX, float newY)
    {
        transform.position = new Vector2(newX, newY);
    }

    public override void Death()
    {
        base.Death();
    }

    public void CreateAggro(Character attacker, float aggroNumber)
    {
        Aggro a = Instantiate(GameObjectList.instance.aggro, transform);
        a.name = $"{attacker.characterName}: {aggroNumber}";
        a.agent = attacker;
        a.aggro = aggroNumber;
        aggro.Add(a);
    }
    public void ClearAggro()
    {
        aggro.Clear();
    }
}
