using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Encounter", order = 1)]
public class EncounterSO : ScriptableObject
{
    public int id;
    public List<Character> bossSummon;
    public int arena;
    public int arenaSizeX;
    public int arenaSizeY;
    public float cameraY;
    public int dropAmount;
    public List<ItemSO> drops;
    public string flavor;
    public float maximumYPlace;
    public List<Vector2> bossLocation;
    public int pullTime;
    public int bossFightTime;
    public int preEncounter;
}
