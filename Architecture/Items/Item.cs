using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
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
    public int healingMod;
    public int energyCostMod;
    public Character character;
    public float attackSpeed;
    public float range;
    public List<string> flavor;
    public void Health()
    {
        character.maxHealth.RemoveAllModifiersFromSource(this);
        character.maxHealth.AddModifier(new StatModifier(health, StatModType.Flat, this,type.ToString()));
    }
    public void Mana()
    {
        character.maxMana.RemoveAllModifiersFromSource(this);
        character.maxMana.AddModifier(new StatModifier(mana, StatModType.Flat, this, type.ToString()));
    }
    public void AttackPower()
    {
        character.attackPower.RemoveAllModifiersFromSource(this);
        character.attackPower.AddModifier(new StatModifier(attackPower, StatModType.Flat, this, type.ToString()));
    }
    public void SpellPower()
    {
        character.spellpower.RemoveAllModifiersFromSource(this);
        character.spellpower.AddModifier(new StatModifier(spellpower, StatModType.Flat, this, type.ToString()));
    }
    public void Crit()
    {
        character.crit.RemoveAllModifiersFromSource(this);
        character.crit.AddModifier(new StatModifier(crit, StatModType.Flat, this, type.ToString()));
    }
    public void Defence()
    {
        character.defence.RemoveAllModifiersFromSource(this);
        character.defence.AddModifier(new StatModifier(defence, StatModType.Flat, this, type.ToString()));
    }
    public void Vamp()
    {
        character.vamp.RemoveAllModifiersFromSource(this);
        character.vamp.AddModifier(new StatModifier(vamp, StatModType.Flat, this, type.ToString()));
    }
    public void Haste()
    {
        character.haste.RemoveAllModifiersFromSource(this);
        character.haste.AddModifier(new StatModifier(haste, StatModType.Flat, this, type.ToString()));
    }
    public void Movement()
    {
        character.movement.RemoveAllModifiersFromSource(this);
        character.movement.AddModifier(new StatModifier((float)movement/100, StatModType.Percent, this, type.ToString()));
    }
    public void Damage()
    {
        character.damage.RemoveAllModifiersFromSource(this);
        character.damage.AddModifier(new StatModifier(damage, StatModType.Flat, this, type.ToString()));
    }
    public void PhysicalDamage()
    {
        character.physicalDamageMod.RemoveAllModifiersFromSource(this);
        character.physicalDamageMod.AddModifier(new StatModifier(physicalDamageMod, StatModType.Flat, this, type.ToString()));
    }
    public void MagicDamage()
    {
        character.magicDamageMod.RemoveAllModifiersFromSource(this);
        character.magicDamageMod.AddModifier(new StatModifier(magicDamageMod, StatModType.Flat, this, type.ToString()));
    }
    public void ManaRegen()
    {
        character.manaRegenMod.RemoveAllModifiersFromSource(this);
        character.manaRegenMod.AddModifier(new StatModifier(-(float)manaRegenMod, StatModType.Percent, this, type.ToString()));
    }
    public void DamageTaken()
    {
        character.damageTakenMod.RemoveAllModifiersFromSource(this);
        character.damageTakenMod.AddModifier(new StatModifier(damageTakenMod, StatModType.Flat, this, type.ToString()));
    }
    public void EnergyCost()
    {
        character.energyCostMod.RemoveAllModifiersFromSource(this);
        character.energyCostMod.AddModifier(new StatModifier(energyCostMod, StatModType.Flat, this, type.ToString()));
    }
    public void Healing()
    {
        character.healingMod.RemoveAllModifiersFromSource(this);
        character.healingMod.AddModifier(new StatModifier(energyCostMod, StatModType.Flat, this, type.ToString()));
    }
    public void Thorns()
    {
        character.thorns.RemoveAllModifiersFromSource(this);
        character.thorns.AddModifier(new StatModifier(energyCostMod, StatModType.Flat, this, type.ToString()));
    }
}
