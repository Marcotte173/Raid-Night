using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EventStatus {Unavailable,Available,Triggered,Over};
public class Event : MonoBehaviour
{
    public int[] available;
    public int[] expire;
    public int id;
    public string eventName;
    public List<string> flavor;
    public EventStatus eventStatus;
    public virtual void CheckAvailable()
    {
        int current = TimeManagement.instance.day + TimeManagement.instance.week * 7 + TimeManagement.instance.month * 28 + (TimeManagement.instance.year -2021)* 336;
        int avail = available[0] + available[1] * 7 + available[2] * 28 + available[3] * 336;
        
        if (current >= avail) eventStatus = EventStatus.Available;
        CheckTrigger();
    }
    public virtual void CheckTrigger()
    {
        if(eventStatus == EventStatus.Available) Trigger();
    }
    public virtual void CheckExpire()
    {
        int current = TimeManagement.instance.day + TimeManagement.instance.week * 7 + TimeManagement.instance.month * 28 + (TimeManagement.instance.year - 2021) * 336;
        int avail = expire[0] + expire[1] * 7 + expire[2] * 28 + expire[3] * 336;
        if (current >= avail) Expire();
        
    }

    public virtual void Expire()
    {
        eventStatus = EventStatus.Over;
    }
    public virtual void Go()
    {
        foreach (TMP_Text t in EventManager.instance.eventText) t.text = "";
        EventManager.instance.ButtonsOff();
    }   

    public virtual void Trigger()
    {        
        eventStatus = EventStatus.Triggered;
    }
    public virtual void ButtonOne()
    {
        EventManager.instance.Load();
    }

    public virtual void ButtonTwo()
    {
        EventManager.instance.Load();
    }
    public virtual void ButtonThree()
    {
        EventManager.instance.Load();
    }
    internal void CurrentDayAvailable()
    {
        available[0] = TimeManagement.instance.day;
        available[1] = TimeManagement.instance.week;
        available[2] = TimeManagement.instance.month;
        available[3] = TimeManagement.instance.year-2021;
    }
    internal void CurrentDayExpire()
    {
        expire[0] = TimeManagement.instance.day;
        expire[1] = TimeManagement.instance.week;
        expire[2] = TimeManagement.instance.month;
        expire[3] = TimeManagement.instance.year-2021;
    }
    internal void ExpireMatchAvailable()
    {
        expire[0] = available[0];
        expire[1] = available[1];
        expire[2] = available[2];
        expire[3] = available[3];
    }
}