using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildUpgrade : MonoBehaviour
{
    public string upgradeName;
    public int id;
    public int cost;
    public float renownRequirement;
    public List<GuildUpgrade> preReq; 
    public float effect;
    public List<float> effects;
    public Sprite pic;
    public List<string> flavor;
    public string description;
    public bool immediate;
    public bool weekly;
    public Player target;

    public virtual void Effect()
    {
        
    }
}
