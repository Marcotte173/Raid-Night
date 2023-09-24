using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float time;
    private void Start()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public void UpdateDestroy()
    {
        if (DungeonManager.instance.raidMode == RaidMode.Combat)
        {
            time -= UnityEngine.Time.deltaTime;
            if (time <= 0)
            {
                DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}