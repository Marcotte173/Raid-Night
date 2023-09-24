using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteList : MonoBehaviour
{
    public static SpriteList instance;
    [Header("------------------------------------------------------")]
    [Header("Class Colors")]
    [Header("")]
    public Color32 mage = new Color32(0, 237, 255,255);
    public Color32 druid = new Color32(197, 196, 196, 255);
    public Color32 beserker = new Color32(243, 15, 15, 255);
    public Color32 shieldBearer = new Color32(227, 74, 248, 255);
    public Color32 rogue = new Color32(255, 217, 0, 255);
    public Color32 bard = new Color32(74, 229, 71, 255);
    [Header("------------------------------------------------------")]   
    [Header("Rarity Colors")]
    [Header("")]
    public Color32 uncommon = new Color32(243, 15, 15, 255);
    public Color32 rare = new Color32(227, 74, 248, 255);
    public Color32 epic = new Color32(255, 217, 0, 255);
    public Color32 legendary = new Color32(74, 229, 71, 255);
    public Color32 playerName = new Color32(255, 217, 0, 255);
    [Header("------------------------------------------------------")]
    [Header("Stat Colors")]
    [Header("")]
    public Color32 health = new Color32(74, 229, 71, 255);
    public Color32 mana = new Color32(74, 229, 71, 255);
    public Color32 rage = Color.red;
    public Color32 energy = Color.yellow;
    [Header("------------------------------------------------------")]    
    [Header("Calendar Colors")]
    [Header("")]
    public Color32 today = new Color32(227, 74, 248, 255);
    public Color32 ahead = new Color32(227, 74, 248, 255);
    public Color32 behind = new Color32(227, 74, 248, 255);
    [Header("------------------------------------------------------")]
    [Header("Combat Colors")]
    [Header("")]
    public Color32 damageColor = new Color32(74, 229, 71, 255);
    public Color32 magicColor = new Color32(74, 229, 71, 255);
    public Color32 critColor = new Color32(74, 229, 71, 255);
    public Color32 healColor = new Color32(74, 229, 71, 255);
    [Header("------------------------------------------------------")]
    [Header("Misc Colors")]
    [Header("")]
    public Color32 bad = new Color32(74, 229, 71, 255);
    public Color32 green = new Color32(227, 74, 248, 255);
    public Color32 red = new Color32(227, 74, 248, 255);
    public Color32 darkGreen = new Color32(227, 74, 248, 255);    
    public Color32 grey = new Color32(74, 229, 71, 255);
    [Header("------------------------------------------------------")]
    [Header("")]
    public List<Sprite> pveBackGround;
    public List<Sprite> greyArena;
    public List<Sprite> grass;
    public Sprite arcaneMissile;
    public Sprite fireblast;
    public Sprite arcaneAoeMissile;
    public Sprite menuBackGround;
    public Sprite none;
    public Sprite rewardBackGround;
    public Sprite placeTile;
    public Sprite dead;
    public List<Sprite> flagGroundColor;
    public List<Sprite> flagColor;
    public List<Sprite> outline;
    public List<Sprite> characters;
    public Sprite arcanosPillar;
    public List<Sprite> roleImages;
    private void Awake()
    {
        instance = this;
    }
}