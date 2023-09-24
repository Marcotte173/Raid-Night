using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Effect
{
    public float shieldRemaining;
    public int stack;
    public List<Sprite> pics;
    public void NewShield(float shield,float time,int stackAmount)
    {        
        shieldRemaining = shield;
        timer = time;
        Timer();
        Utility.instance.TurnOn(target.playerUI.shield);
        flavor.Add("Shield");
        stack = stackAmount;
        flavor.Add($"Protects target from {Mathf.Round(shieldRemaining)} Damage");
        flavor.Add((stack == 0)?"": $"{stack} stacks");
        flavor.Add("");        
        target.buff.Add(this);
        sprite = pics[stack];
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public void ShieldAdd(float shield)
    {
        Utility.instance.TurnOn(target.playerUI.shield);
        shieldRemaining += shield;
        ResetTimer();
        flavor[1] = $"Protects target from {Mathf.Round(shieldRemaining)} Damage";
        flavor[2] = (stack == 0) ? "" : $"{stack} stacks";
        sprite = pics[stack];
    }
    public void ShieldStack(float shield,int stackAdd)
    {
        shieldRemaining = shield;
        stack += stackAdd;
        ResetTimer();
        sprite = pics[stack];
        flavor[1] = $"Protects target from {Mathf.Round(shieldRemaining)} Damage";
        flavor[2] = (stack == 0) ? "" : $"{stack} stacks";
    }
    public void ShieldReplace(float shield,float time)
    {
        shieldRemaining = shield;
        timer = time;
        Timer();
        stack = 0;
        Utility.instance.TurnOn(target.playerUI.shield);
        flavor.Clear();
        flavor.Add("Shield");
        flavor.Add($"Protects target from {Mathf.Round(shieldRemaining)} Damage");
        flavor.Add((stack == 0) ? "" : $"{stack} stacks");
        flavor.Add("");
        sprite = pics[stack];
    }
    public void ShieldDamage(float x)
    {
        shieldRemaining -= x;
        if (shieldRemaining <= 0) EffectEnd();
        else flavor[1] = $"Protects target from {Mathf.Round(shieldRemaining)} Damage";
        flavor[2] = (stack == 0) ? "" : $"{stack} stacks";
        sprite = pics[stack];
    }

    public override void EffectEnd()
    {
        Utility.instance.TurnOff(target.playerUI.shield);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
        target.targetFromTaunt = null;
        target.buff.Remove(this);
        Destroy(gameObject);
    }
}
