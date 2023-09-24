using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumbers : MonoBehaviour
{
    public float moveSpeed;
    public string message;
    public Text displayNumber;
    public float time;
    private void Awake()
    {
        displayNumber = GetComponentInChildren<Text>();
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Add(gameObject);
    }
    public void UpdateNumbers()
    {
        displayNumber.text = message;
        if (DungeonManager.instance.raidMode == RaidMode.Combat)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * UnityEngine.Time.deltaTime), transform.position.z);
            time -= UnityEngine.Time.deltaTime;
            if (time <= 0)
            {
                DungeonManager.instance.currentDungeon.currentEncounter.objects.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}