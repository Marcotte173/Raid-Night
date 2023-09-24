using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum Spec { Stalwart, Focused, Explosive, Redemptive, Wrathful,Tranquil, Inspiring };
public class Class : Character
{
    public int specNumber;
    public Spec spec;
    public float timeInFight;
    public bool startTimeInFight;
    public List<ItemSO> headSets;
    public List<ItemSO> chestSets;
    public List<ItemSO> legSets;
    public List<ItemSO> feetSets;
    public List<ItemSO> trinketSets;
    public List<ItemSO> weaponSets;
    public List<ItemSO> offHandSets;
    public List<AggroBelief> aggroBelief;
    public override void Create()
    {
        aggroBelief = new List<AggroBelief>();
        specNumber = (id > 0 && id % 2 == 1) ? 1 : 0;
    }
    public virtual void FindSpec()
    {
        
    }
    public void ChangeSpec()
    {
        if (specNumber == 0) specNumber = 1;
        else specNumber = 0;
        FindSpec();
        CoreStats();
        Equipment();
    }    

    public void SpecEquip()
    {
        Equip.instance.Item((headSets.Count==1)?headSets[0]:headSets[specNumber], head);
        Equip.instance.Item(((chestSets.Count==1))? chestSets[0] : chestSets[specNumber], chest);
        Equip.instance.Item(((legSets.Count==1))? legSets[0] : legSets[specNumber], legs);
        Equip.instance.Item(((feetSets.Count==1))? feetSets[0] : feetSets[specNumber], feet);
        Equip.instance.Item(((trinketSets.Count==1))? trinketSets[0] : trinketSets[specNumber], trinket);
        Equip.instance.Item(((weaponSets.Count==1))? weaponSets[0] : weaponSets[specNumber], weapon);
        Equip.instance.Item(((offHandSets.Count == 1)) ? offHandSets[0] : offHandSets[specNumber], offHand);
    }

    public override void GetTarget()
    {
        if (target == null || target.ko || targetOverrideTimer <= 0)target = Target.instance.Closest(this, DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion());
        foreach (Ability a in ability) a.target = target;
    }

    public override void Decision()
    {
        
    }
    public override void CoreStats()
    {
        base.CoreStats();
    }
    public virtual void Equipment()
    {

    }   
    public override void UpdateStuff()
    {
        base.UpdateStuff();
        if (aggroBelief != null)
        {
            if (aggroBelief.Count != 0)
            {
                foreach (AggroBelief a in aggroBelief.ToList()) if (a.boss.ko) aggroBelief.Remove(a);
                if (aggroBelief.Count > 1) Utility.instance.SortAggro(aggroBelief);
            }
        }                
    }
    public void CreateAggroBelief(Character attacker, float aggroNumber)
    {
        AggroBelief a = Instantiate(GameObjectList.instance.aggroBelief, transform);
        a.name = $"{attacker.characterName}: {aggroNumber}";
        a.boss = attacker;
        a.aggroBelief = aggroNumber;
        aggroBelief.Add(a);
    }
    public void ClearAggroBelief()
    {
        aggroBelief.Clear();
    }
}
