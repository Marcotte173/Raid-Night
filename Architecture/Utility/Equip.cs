using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public static Equip instance;

    private void Awake()
    {
        instance = this;
    }
    public void Item(ItemSO give, Item receive)
    {
        receive.id = give.id;
        receive.pic = give.pic;
        receive.itemName = give.itemName;
        receive.rarity = give.rarity;
        receive.type = give.type;
        receive.armorType = give.armorType;
        receive.hands = give.hands;
        receive.score = give.score;
        receive.value = give.value;
        receive.weaponType = give.weaponType;
        receive.offHandType = give.offHandType;
        receive.itemTypeString = give.itemTypeString;
        receive.role = give.role;
        //Health
        receive.health = give.health;
        receive.Health();
        //Mana
        receive.mana = give.mana;
        receive.Mana();
        //Defence
        receive.defence = give.defence;
        receive.Defence();
        //Crit
        receive.crit = give.crit;
        receive.Crit();
        //Attack Power
        receive.attackPower = give.attackPower;
        receive.AttackPower();
        //Spell Power
        receive.spellpower = give.spellpower;
        receive.SpellPower();
        //Vamp
        receive.vamp = give.vamp;
        receive.Vamp();
        //Haste
        receive.haste = give.haste;
        receive.Haste();
        //Movement
        receive.movement = give.movement;
        receive.Movement();
        //Damage
        receive.damage = give.damage;
        receive.Damage();
        //Physical Damage
        receive.physicalDamageMod = give.physicalDamageMod;
        receive.PhysicalDamage();
        //Magic Damage
        receive.magicDamageMod = give.magicDamageMod;
        receive.MagicDamage();
        //Mana Regen
        receive.manaRegenMod = give.manaRegenMod;
        receive.ManaRegen();
        //Damage Taken
        receive.damageTakenMod = give.damageTakenMod;
        receive.DamageTaken();
        //Healing
        receive.healingMod = give.healingMod;
        receive.Healing();
        //Energy Cost
        receive.energyCostMod = give.energyCostMod;
        receive.EnergyCost();
        //Energy Cost
        receive.thorns = give.thorns;
        receive.Thorns();
        //Cooldown
        receive.attackSpeed = give.attackSpeed;
        //Range
        receive.range = give.range;
        //Flavor
        receive.flavor.Clear();
        for (int i = 0; i < give.flavor.Count; i++) receive.flavor.Add(give.flavor[i]);
        if (receive.GetComponent<Weapon>()) WeaponAdjust(receive);
        if (receive.GetComponent<OffHand>()) OffHandAdjust(receive);
       

    }

    public void OffHandAdjust(Item item)
    {
        item.character.playerUI.offHand.transform.localRotation = new Quaternion(0, 0, 0, 0);
        if (item.offHandType == OffHandType.Dagger) 
        {
            item.character.playerUI.offHand.transform.localPosition = new Vector2(0.18f, 0.017f);
            item.character.playerUI.offHand.transform.Rotate(0, 180, 11.45f);
        }
        else
        {
            item.character.playerUI.offHand.transform.localPosition = new Vector2(0.167f, 0f);
            if (item.offHandType == OffHandType.Shield) item.character.playerUI.offHand.transform.localScale = new Vector2(.7f, .7f);
        }

    }
        

    public void WeaponAdjust(Item item)
    {
        item.character.playerUI.weapon.transform.localRotation = new Quaternion(0, 0, 0, 0);
        if (item.hands == Hands.One)
        {
            item.character.playerUI.weapon.transform.Rotate(0, 0, 11.45f);
            if (item.weaponType == WeaponType.Sword) item.character.playerUI.weapon.transform.localPosition = new Vector2(-0.209f, 0.111f);
            if (item.weaponType == WeaponType.Dagger || item.weaponType == WeaponType.Axe || item.weaponType == WeaponType.Mace || item.weaponType == WeaponType.Wand) item.character.playerUI.weapon.transform.localPosition = new Vector2(-0.187f, 0.017f);
        }       
        else if (item.weaponType == WeaponType.Staff) item.character.playerUI.weapon.transform.localPosition = new Vector2(-0.186f, 0.092f);
        else 
        {
            item.character.playerUI.weapon.transform.localPosition = new Vector2(0.275f, 0.058f);
            item.character.playerUI.weapon.transform.Rotate(0, 0, 289.968f);
        }        
    }
}