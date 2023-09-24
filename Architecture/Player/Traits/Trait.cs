using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trait : MonoBehaviour
{
    public string traitName;
    public int id;
    public float effect;
    public List<float> effects;
    public Sprite pic;
    public List<string> flavor;
    public bool immediate;
    public bool weekly;
    public Player player;

    public virtual void Effect()
    {
        
    }
}
