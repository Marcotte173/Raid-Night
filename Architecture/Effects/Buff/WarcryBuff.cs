using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarcryBuff : Effect
{
    // Start is called before the first frame update
    void Start()
    {
        flavor.Add("Warcry");
        flavor.Add("Critical chance increased by " + damage + " percent");
        flavor.Add("");
        flavor.Add("");
        target.crit.AddModifier(new StatModifier(damage, StatModType.Percent, this, "Warcry"));
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    // Update is called once per frame
    public override void EffectEnd()
    {
        target.crit.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.buff.Remove(this);
        Destroy(gameObject);
    }
    
}
