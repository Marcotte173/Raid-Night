using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType {None,Hot,Damage,Buff,Debuff,Shield,Bleed}

public class Effect : MonoBehaviour
{
    public EffectType type1;
    public EffectType type2;
    public EffectType type3;
    public Character attacker;
    public Character target;
    public float threat;
    public float timer;
    public float damage;
    public Sprite sprite;
    public float timeRemaining;
    public List<string> flavor;
    public List<float> threshHold = new List<float> { };
    public int check;
    public List<float> timeThreshHold = new List<float> { };
    public int timeCheck;
    public bool go;
    public virtual void UpdateEffect()
    {
        if (DungeonManager.instance.raidMode == RaidMode.Combat&& go)
        {
            timer -= UnityEngine.Time.deltaTime;
            if (timer <= timeThreshHold[timeCheck])
            {
                timeRemaining = timeThreshHold[timeCheck];
                flavor[3] = $"{timeRemaining} seconds remaining";
                timeCheck++;
            }
            if (timer <= 0f)
            {
                EffectEnd();
            }
            else if (threshHold.Count >0 && timer <= threshHold[check])
            {                
                EffectTick();
                check++;
            }
        }
    }
    public void Timer()
    {
        timeThreshHold.Clear();
        timeCheck = 0;
        for (float i = timer; i >= 0; i--)
        {
            timeThreshHold.Add(i);
        }
        if (timeThreshHold[timeThreshHold.Count-1]!=0) timeThreshHold.Add(0);
        go = true;
    }
    public void ResetTimer()
    {
        timer = timeThreshHold[0];
        Timer();
    }
    public virtual void EffectTick()
    {

    }
    public virtual void EffectEnd()
    {

    }
}
