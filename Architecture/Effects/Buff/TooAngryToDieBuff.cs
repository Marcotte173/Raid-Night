using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooAngryToDieBuff : Effect
{
    // Start is called before the first frame update
    void Start()
    {
        flavor.Add("Too Angry to die");
        flavor.Add("Damage done heals target");
        flavor.Add("");
        flavor.Add("");
        target.buff.Add(this);
        target.vamp.AddModifier(new StatModifier(damage, StatModType.Flat, this, "Too Angry to Die"));
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }

    public override void EffectEnd()
    {
        target.vamp.RemoveAllModifiersFromSource(this);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.buff.Remove(this);
        Destroy(gameObject);
    }
}
