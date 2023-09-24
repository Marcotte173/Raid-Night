using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {Head,Chest,Legs,Feet,Weapon,OffHand,Trinket};
public enum Rarity { Uncommon,Rare, Epic, Legendary };
public enum ArmorType { None, Cloth, Leather, Plate };
public enum WeaponType {None,Dagger,Sword,Staff,Wand,Axe,Mace};
public enum OffHandType {None,Orb,Source,Book,Dagger,Shield,Relic};
public enum Hands { One, Two, None };
public enum Role { Tank, PDPS,MDPS, Heal, Support};
[CreateAssetMenu(fileName = "Data", menuName = "Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite pic;
    public Rarity rarity;
    public ItemType type;
    public ArmorType armorType;
    public WeaponType weaponType;
    public OffHandType offHandType;
    public Hands hands;
    public Role role;
    public string itemTypeString;
    public int health;    
    public int defence;
    public int crit;
    public int attackPower;
    public int spellpower;
    public int mana;
    public int damage;
    public float attackSpeed;
    public float range;
    public int score;
    public int value;
    public int vamp;
    public int haste;
    public int thorns;
    public int movement;
    public int physicalDamageMod;
    public int magicDamageMod;
    public int manaRegenMod;
    public int damageTakenMod;
    public int energyCostMod;
    public int healingMod;
    public List<string> flavor;
}
