using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public static CalendarManager instance;
    public int displayYear;
    public int displayMonth;
    
    private void Start()
    {
        instance = this;
    }
    public void ViewCalendar()
    {
        displayYear = TimeManagement.instance.year;
        displayMonth = TimeManagement.instance.month;
        CalendarShow();
    }
    private void CalendarShow()
    {
        Utility.instance.TurnOn(GuildUI.instance.calendar);
        GuildUI.instance.calendarInfo[0].text = TimeManagement.instance.MonthDisplay(displayMonth);
        GuildUI.instance.calendarInfo[1].text = displayYear.ToString();
        //Show Events
        for (int i = 0; i < GuildUI.instance.guildDays.Count; i++)
        {
            GuildUI.instance.guildDays[i].GetComponent<ItemSprite>().flavor[1].text = "";
            if (i == 0 || i == 7 || i == 14 || i == 21) GuildUI.instance.guildDays[i].GetComponent<ItemSprite>().flavor[1].text = "Reset Dungeons";
            int gDay = i + 1;
            int gWeek = 1;
            while (gDay > 7)
            {
                gDay -= 7;
                gWeek++;
            }
            if (EventList.instance.CalendarEvents(gDay, gWeek, displayMonth, displayYear - 2021).Count > 0)
            {
                foreach (Event e in EventList.instance.CalendarEvents(gDay, gWeek, displayMonth, displayYear - 2021))
                {
                    if (e.id == 11)
                    {
                        RosterEvent r = (RosterEvent)e;
                        GuildUI.instance.guildDays[i].GetComponent<ItemSprite>().flavor[1].text += r.roster.dungeon.dungeonName;
                    }
                }
            }
        }
        if (displayYear > TimeManagement.instance.year || (displayYear == TimeManagement.instance.year && displayMonth > TimeManagement.instance.month)) foreach (Image i in GuildUI.instance.guildDays) i.GetComponent<ItemSprite>().flavor[0].color = SpriteList.instance.ahead;
        else
        {
            for (int i = 0; i < GuildUI.instance.guildDays.Count; i++)
            {
                if (i + 1 == TimeManagement.instance.day + (TimeManagement.instance.week - 1) * 7) GuildUI.instance.guildDays[i].GetComponent<ItemSprite>().flavor[0].color = SpriteList.instance.today;
                else if (i + 1 > TimeManagement.instance.day + (TimeManagement.instance.week - 1) * 7) GuildUI.instance.guildDays[i].GetComponent<ItemSprite>().flavor[0].color = SpriteList.instance.ahead;
                else GuildUI.instance.guildDays[i].GetComponent<ItemSprite>().flavor[0].color = SpriteList.instance.behind;
            }
        }
    }
    public void CloseCalendar()
    {
        Utility.instance.TurnOff(GuildUI.instance.calendar);
    }

    public void ViewCalendarForward()
    {
        displayMonth++;
        if (displayMonth > 12)
        {
            displayMonth -= 12;
            displayYear++;
        }
        CalendarShow();
    }
    public void ViewCalendarBackward()
    {
        if (!(displayYear == TimeManagement.instance.year && displayMonth == TimeManagement.instance.month))
        {
            displayMonth--;
            if (displayMonth <= 0)
            {
                displayMonth += 12;
                displayYear--;
            }
        }
        CalendarShow();
    }
}
