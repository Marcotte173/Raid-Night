using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSpecs : MonoBehaviour
{
    public static StatSpecs instance;
    private void Awake()
    {
        instance = this;
    }
    [Header("------------------------------------------------------")]
    [Header("Beserker")]
    public List<float> beserkerMaxHealth;
    public List<float> beserkerMaxMana;
    public List<float> beserkerDefence;
    public List<float> beserkerSpellpower;
    public List<float> beserkerDamage;
    public List<float> beserkerCrit;
    public List<float> beserkerManaRegenValue;
    public List<float> beserkerManaRegenTime;
    public List<float> beserkerBasicAttackCooldownTime;
    public List<float> beserkerBasicAttackCastTime;
    public List<float> beserkerBasicAttackRangeRequired;
    public List<float> beserkerBasicAttackThreat;
    [Header("------------------------------------------------------")]
    [Header("Druid")]
    public List<float> druidMaxHealth;
    public List<float> druidMaxMana;
    public List<float> druidDefence;
    public List<float> druidSpellpower;
    public List<float> druidDamage;
    public List<float> druidCrit;
    public List<float> druidManaRegenValue;
    public List<float> druidManaRegenTime;
    public List<float> druidBasicAttackCooldownTime;
    public List<float> druidBasicAttackCastTime;
    public List<float> druidBasicAttackRangeRequired;
    public List<float> druidBasicAttackThreat;
    [Header("------------------------------------------------------")]
    [Header("Rogue")]
    public List<float> rogueMaxHealth;
    public List<float> rogueMaxMana;
    public List<float> rogueDefence;
    public List<float> rogueSpellpower;
    public List<float> rogueDamage;
    public List<float> rogueCrit;
    public List<float> rogueManaRegenValue;
    public List<float> rogueManaRegenTime;
    public List<float> rogueBasicAttackCooldownTime;
    public List<float> rogueBasicAttackCastTime;
    public List<float> rogueBasicAttackRangeRequired;
    public List<float> rogueBasicAttackThreat;
    [Header("------------------------------------------------------")]
    [Header("Mage")]
    public List<float> mageMaxHealth;
    public List<float> mageMaxMana;
    public List<float> mageDefence;
    public List<float> mageSpellpower;
    public List<float> mageDamage;
    public List<float> mageCrit;
    public List<float> mageManaRegenValue;
    public List<float> mageManaRegenTime;
    public List<float> mageBasicAttackCooldownTime;
    public List<float> mageBasicAttackCastTime;
    public List<float> mageBasicAttackRangeRequired;
    public List<float> mageBasicAttackThreat;    
    [Header("------------------------------------------------------")]
    [Header("Bard")]
    public List<float> bardMaxHealth;
    public List<float> bardMaxMana;
    public List<float> bardDefence;
    public List<float> bardSpellpower;
    public List<float> bardDamage;
    public List<float> bardCrit;
    public List<float> bardManaRegenValue;
    public List<float> bardManaRegenTime;
    public List<float> bardBasicAttackCooldownTime;
    public List<float> bardBasicAttackCastTime;
    public List<float> bardBasicAttackRangeRequired;
    public List<float> bardBasicAttackThreat;
    [Header("------------------------------------------------------")]
    [Header("Shield Bearer")]
    public List<float> shieldBearerMaxHealth;
    public List<float> shieldBearerMaxMana;
    public List<float> shieldBearerDefence;
    public List<float> shieldBearerSpellpower;
    public List<float> shieldBearerDamage;
    public List<float> shieldBearerCrit;
    public List<float> shieldBearerManaRegenValue;
    public List<float> shieldBearerManaRegenTime;
    public List<float> shieldBearerBasicAttackCooldownTime;
    public List<float> shieldBearerBasicAttackCastTime;
    public List<float> shieldBearerBasicAttackRangeRequired;
    public List<float> shieldBearerBasicAttackThreat;
}
