using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManagement : MonoBehaviour,IDataPersistence
{
    public static TimeManagement instance;
    public List<string> Day = new List<string> {"None", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    public List<string> Week = new List<string> {"None", "First", "Second", "Third", "Fourth" };
    public List<string> Month = new List<string> {"None", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    public int minute;
    public int hour;
    public int day;
    public int week;
    public int month;
    public int year;
    public List<TMP_Text> timeDisplay;
    private void Awake()
    {
        instance = this;
    }

    public void ConvertTime()
    {        
        int addHour=0;
        int addDay = 0;
        int addWeek = 0;
        int addMonth = 0;
        int addYear = 0;
        while (minute >= 60)
        {
            minute -= 60;
            addHour++;
        }
        hour += addHour;
        while (hour >= 24)
        {
            hour -= 24;
            addDay++;
        }
        day += addHour;
        while (day > 7)
        {
            day -= 7;
            addWeek++;
        }
        week += addWeek;
        while (week > 4)
        {
            week -= 4;
            addMonth++;
        }
        month += addMonth;
        while (month > 12)
        {
            month -= 12;
            addYear++;
        }
        year += addYear;        
        UpdateTimeDisplay();
        UpdateEvents();
    }

    public void UpdateEvents()
    {        
        foreach (Event e in EventList.instance.events)
        {
            if(e.eventStatus!= EventStatus.Over)
            {
                e.CheckAvailable();
                if (e.eventStatus == EventStatus.Triggered) EventManager.instance.triggeredEvents.Add(e);
                e.CheckExpire();
            }
        } 
        if (EventManager.instance.triggeredEvents.Count > 0) EventManager.instance.Events();
    }

    public void MinuteAdd(int x)
    {
        minute += x;
            ConvertTime();
    }
    public void HourAdd(int x)
    {
        hour += x;
        ConvertTime();
    }
    public void DayAdd(int x)
    {
        day += x;
        ConvertTime();
    }
    public void WeekAdd(int x)
    {
        week += x;
        ConvertTime();
    }
    public void MonthAdd(int x)
    {
        month += x;
        ConvertTime();
    }
    public void YearAdd(int x)
    {
        year += x;
        ConvertTime();
    }
    public string MinuteDisplay()
    {
        if (minute <= 9) return ("0" + minute.ToString());
        else return minute.ToString();
    }
    public string HourDisplay()
    {
        if (hour <= 12) return (hour <= 9) ? ("0" + hour.ToString()):hour.ToString();
        else return (hour <= 21)? ("0" + (hour-12).ToString()):(hour-12).ToString();
    }
    public string DayDisplay()=> Day[day];
    public string WeekDisplay()=> Week[week];
    public string MonthDisplay() => Month[month];
    public string MonthDisplay(int x) => Month[x];
    public int DayOfMonth() => (week-1) * 7 + day;
    public string TimeOfDayDisplay()
    {
        string ampm = (hour <= 11) ? "A.M." : "P.M.";
        return $"{HourDisplay()}:{MinuteDisplay()} {ampm}";
    }
    public string DateDisplay()
    {
        return $"{MonthDisplay()} {(week-1) * 7 + day}, {year}";
    }
    public string DayOfWeekDisplay()
    {
        return $"It is {DayDisplay()}, the {WeekDisplay()} week of {MonthDisplay()}, {year}";
    }
    public void TimeSet(int m,int h,int d, int w, int mo, int y)
    {
        minute = m;
        hour = h;
        day = d;
        week = w;
        month = mo;
        year = y;
        ConvertTime();
    }
    public void NewDay(int days)
    {
        day += days;
        minute = 0;
        hour = 7;
        ConvertTime();
    }

    public void UpdateTimeDisplay()
    {
        foreach(TMP_Text t in timeDisplay)
        {
            string ampm = (hour <= 11) ? "A.M." : "P.M.";
            t.text = $"{HourDisplay()}:{MinuteDisplay()} {ampm}  {DayDisplay()}, {MonthDisplay()} {(week - 1) * 7 + day}, {year}";
        }
    }
    public void LoadData(GameData data)
    {
        this.day = data.day;
        this.week = data.week;
        this.month = data.month;
        this.year = data.year;
        UpdateTimeDisplay();
    }
    public void SaveData(GameData data)
    {
        data.day = day;
        data.week = week;
        data.month = month;
        data.year = year;
    }
}
