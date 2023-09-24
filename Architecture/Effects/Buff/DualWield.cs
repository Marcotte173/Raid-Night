using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWield : Effect
{
    void Start()
    {
        flavor.Add("Dual Wield");
        flavor.Add("Attack Speed increased by " + damage + " percent");
        flavor.Add("");
        flavor.Add("");
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    // Update is called once per frame
    public override void EffectEnd()
    {
        target.buff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
