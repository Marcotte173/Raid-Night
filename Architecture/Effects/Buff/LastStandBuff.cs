using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStandBuff : Effect
{
    private void Start()
    {
        flavor.Add("Last Stand");
        flavor.Add($"Health cannot drop below 1");
        flavor.Add("");
        flavor.Add("");
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.buff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
