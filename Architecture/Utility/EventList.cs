using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventList : MonoBehaviour
{
    public static EventList instance;
    public List<Event> events;
    public RosterEvent rosterEvent;
    public List<RosterEvent> rosterEvents;
    private void Awake()
    {
        instance = this;       
    }

    public void MakeRosterEvent()
    {
        RosterEvent r = Instantiate(rosterEvent,transform);
        r.name = $"Roster Event {rosterEvents.Count+1}";
        rosterEvents.Add(r);
    }

    internal void MakeEvents()
    {
        for (int i = 0; i < events.Count; i++)
        {
            Event e = Instantiate(events[i], transform);
            e.name = events[i].eventName;
            events[i] = e;
        }
    }
    internal List<Event> CalendarEvents(int day, int week, int month, int year)
    {
        List<Event> list = new List<Event> { };
        foreach(Event e in events) if (e.available[0] == day && e.available[1] == week && e.available[2] == month && e.available[3] == year) list.Add(e);
        return list;
    }
}
