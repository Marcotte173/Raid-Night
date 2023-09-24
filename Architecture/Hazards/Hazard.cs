using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public List<Character> playersAndMinions;
    public List<Character> enemies;
    public List<Tile> tiles;
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
    public List<float> threshHold = new List<float> { };
    public int check;
    public List<float> timeThreshHold = new List<float> { };
    public int timeCheck;
    public bool go;

    private void Start()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public void UpdateHazard()
    {
        timer -= UnityEngine.Time.deltaTime;
        if (timer <= timeThreshHold[timeCheck])
        {
            timeRemaining = timeThreshHold[timeCheck];
            timeCheck++;
        }
        if (timer <= 0f)
        {
            EffectEnd();
        }
        else if (threshHold.Count > 0 && timer <= threshHold[check])
        {
            enemies.Clear();
            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion()) if (tiles.Contains(a.GetComponent<Move>().currentTile)) enemies.Add(a);
            playersAndMinions.Clear();
            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion()) if (tiles.Contains(a.move.currentTile)) playersAndMinions.Add(a);
            EffectTick();
            check++;
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
        if (timeThreshHold[timeThreshHold.Count - 1] != 0) timeThreshHold.Add(0);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Tile>())
        {
            tiles.Add(collision.GetComponent<Tile>());
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Tile>())
        {
            tiles.Remove(collision.GetComponent<Tile>());
        }
    }
}
