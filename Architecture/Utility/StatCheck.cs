using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCheck : MonoBehaviour
{
    public static StatCheck instance;
    private void Awake()
    {
        instance = this;
    }
    public void Summary(Character target)
    {
        string mods = target.characterName + " : ";
        mods += target.movement.value + " Movement, ";
        mods += target.defence.value + " Defence, ";
        mods += target.attackPower.value + " Attack Power, ";
        mods += target.spellpower.value + " Spell Power, ";
        mods += target.crit.value + "% Crit, ";
        mods += target.damage.value + " Damage, ";
        mods += target.vamp.value + "% Vamp, ";
        mods += target.haste.value + "% Haste, ";
        mods += target.energyCostMod.value * 100 + "% Energy Cost, ";
        mods += target.physicalDamageMod.value * 100 + "% Physical Damage, ";
        mods += target.magicDamageMod.value * 100 + "% Magic Damage, ";
        mods += target.manaRegenMod.value * 100 + "% Mana Regen, ";
        mods += target.damageTakenMod.value * 100 + "% Damage Taken ";
        mods += target.healingMod.value * 100 + "% Healing";
        mods += target.thorns.value * 100 + " Thorns";
        Debug.Log(mods);
    }

    public void CritCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.crit.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.crit.value + " Crit";
        Debug.Log(mods);
    }
    public void DefenceCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.defence.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.defence.value + " Defence";
        Debug.Log(mods);
    }
    public void AttackPowerCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.attackPower.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.attackPower.value + " Attack Power";
        Debug.Log(mods);
    }
    public void SpellPowerCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.spellpower.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.spellpower.value + " SpellPower";
        Debug.Log(mods);
    }
    public void VampCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.vamp.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.vamp.value + " Vamp";
        Debug.Log(mods);
    }
    public void HasteCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.haste.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.haste.value + " Haste";
        Debug.Log(mods);
    }
    public void MovementCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.movement.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.movement.value + " Movement";
        Debug.Log(mods);
    }
    public void DamageCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.damage.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.damage.value + " Damage";
        Debug.Log(mods);
    }
    public void PhysicalDamageCHeck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.physicalDamageMod.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.physicalDamageMod.value + " Physical Damage Mod";
        Debug.Log(mods);
    }
    public void MagicDamageCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.magicDamageMod.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.magicDamageMod.value + " Magic Damage Mod";
        Debug.Log(mods);
    }
    public void ManaRegenCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.manaRegenMod.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.manaRegenMod.value + " Mana Regen Mod";
        Debug.Log(mods);
    }
    public void DamageTakenCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.damageTakenMod.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.damageTakenMod.value + " Damage Taken Mod";
        Debug.Log(mods);
    }
    public void HealingCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.healingMod.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.healingMod.value + " Healing Mod";
        Debug.Log(mods);
    }
    public void EnergyCostCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.energyCostMod.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.energyCostMod.value + " Energy Cost Mod";
        Debug.Log(mods);
    }
    public void ThornsCheck(Character target)
    {
        string mods = target.characterName + " : ";
        foreach (StatModifier s in target.thorns.statModifiers) mods += " " + s.identifier + " - " + s.value + ",";
        mods += " Total: " + target.thorns.value + " Energy Cost Mod";
        Debug.Log(mods);
    }
}
