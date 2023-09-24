using System;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    public Character agent;
    public float aggro;
    
    public void ChangeAggro(float x)
    {
        aggro += x;
        name = $"{agent.characterName}: {aggro}";
        agent.player.aggroBelief += x * UnityEngine.Random.Range(.8f,1.2f);
    }
}
