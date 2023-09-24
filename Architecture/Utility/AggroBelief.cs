using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroBelief : MonoBehaviour
{
    public Character boss;
    public float aggroBelief;
    public void ChangeAggro(float x)
    {
        aggroBelief += x;
        name = $"{boss.characterName}: {aggroBelief}";
    }
}
