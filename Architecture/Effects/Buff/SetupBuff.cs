using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBuff : Effect
{
    public int count;
    public List<Sprite> pics;
    private void Start()
    {
        Flavor();
        target.buff.Add(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public void Flavor()
    {
        flavor.Clear();
        flavor.Add("Setup");
        flavor.Add($"Combo points can be used by other abilities");
        flavor.Add($"{count} stacks");
        flavor.Add("");
    }

    public void AddCount(int x)
    {
        count = (count + x >= 5) ? 5 : count + x;
        ResetTimer();      
        Pic(count);
        Flavor();
    }
    public void SubtractCount(int x)
    {
        count -= x;
        if (count <= 0) EffectEnd();
        else Pic(count);
        Flavor();
    }

    public void Pic(int x)
    {
        sprite = pics[x - 1];
    }

    public override void EffectEnd()
    {
        target.buff.Remove(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        Destroy(gameObject);
    }
}
