using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AbilityCheck : MonoBehaviour
{
    public static AbilityCheck instance;    

    public void Shield(Character source, Character target, float amount, float time)
    {
        Shield r = Instantiate(GameObjectList.instance.shield, target.transform);
        r.attacker = source;
        r.target = target;
        if (ShieldValue(target) > 0)
        {
            if (ShieldValue(target) > amount) MyShield(target).ResetTimer();
            else MyShield(target).ShieldReplace(amount, time);
        }
        else
        {
            Shield shield = Instantiate(GameObjectList.instance.shield, target.transform);
            shield.attacker = source;
            shield.target = target;
            shield.NewShield(amount, time, 1);
        }
    }    

    internal Effect BleedCheck(Character target)
    {
        foreach (Effect e in target.debuff)
        {
            if (e.type1 == EffectType.Bleed || e.type2 == EffectType.Bleed || e.type3 == EffectType.Bleed) return e;
        }
        return null;
    }

    internal float RemainingBleed(Character target)
    {
        foreach (Effect e in target.debuff)
        {
            if(e.type1 == EffectType.Bleed|| e.type2 == EffectType.Bleed || e.type3 == EffectType.Bleed) return ((e.threshHold.Count - 1 - e.check) * e.damage);
        }
        return 0;
    }

    public Effect ArcaneTendril(Character target)
    {
        foreach (Effect e in target.debuff) if (e.GetType() == typeof(ArcaneTendrils)) return e;
        return null;
    }
    internal DualWield DualWield(Character target)
    {
        foreach (Effect e in target.debuff) if (e.GetType() == typeof(DualWield)) return (DualWield)e;
        return null;
    }

    public void ShieldStack(Character source, Character target, float time,List <float>stackAmount)
    {
        if (ShieldValue(target) > 0)
        {
            if (ShieldStacks(target) > 0)
            {
                if (ShieldValue(target) > stackAmount[ShieldStacks(target)] || ShieldStacks(target)>4) MyShield(target).ResetTimer();
                else MyShield(target).ShieldStack(stackAmount[ShieldStacks(target)+1], 1);
            }
        }
        else
        {
            Debug.Log($"Cast Shield on {target.characterName}");
            Shield shield = Instantiate(GameObjectList.instance.shield, target.transform);
            shield.attacker = source;
            shield.target = target;
            shield.NewShield(stackAmount[1], time, 1);
        }
    }

    internal BloodyMessBleed BloodyMessCheck(Character target)
    {
        foreach (Effect e in target.debuff) if (e.GetType() == typeof(BloodyMessBleed)) return (BloodyMessBleed)e;
        return null;
    }

    public void Awake()
    {
        instance = this; 
    }
    public bool LostAggro(Character boss,Character tank)
    {
        if (boss.target != tank) return true;
        return false;
    }
    public int ShieldStacks(Character target)
    {
        foreach (Effect e in target.buff)
        {
            if (e.GetType() == typeof(Shield))
            {
                Shield s = (Shield)e;
                return s.stack;
            }
        }
        return 0;
    }
    public float ShieldValue(Character target)
    {
        foreach (Effect e in target.buff)
        {
            if (e.GetType() == typeof(Shield))
            {
                Shield s = (Shield)e;
                return s.shieldRemaining;
            }
        }
        return 0;
    }
    public Shield MyShield(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(Shield)) return (Shield)e;
        return null;
    }
    public Character SecondaryHealTarget(float threshold, Character agent)
    {
        if (DungeonManager.instance.currentDungeon.currentEncounter.Player().Count > 1)
        {
            List<Character> potential = new List<Character> { };
            Class c = (Class)agent;
            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Player()) if (a != agent && c.spec != Spec.Stalwart && a.Health() / a.maxHealth.value < threshold / 100) potential.Add(a);
            if (potential.Count > 0)
            {
                Character target = potential[0];
                if (potential.Count > 1) for (int i = 1; i < potential.Count; i++) if (potential[i].Health() < target.Health()) target = potential[i];
                return target;
            }
        }
        return null;
    }
    public Effect UpliftHotCheck(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(UpliftHot)) return e;
        return null;
    }
    public Effect SoothingMelodyHotCheck(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(SoothingMelodyHot)) return e;
        return null;
    }
    public Effect InspiringTune(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(InspiringTuneBuff)) return e;
        return null;
    }
    public Effect InTune(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(InTuneBuff)) return e;
        return null;
    }
    public Effect DirgeOfDemiseBuffCheck(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(DirgeOfDemiseBuff)) return e;
        return null;
    }

    internal int AmountOfActiveShields(List<Character> list)
    {
        int x = 0;
        foreach(Character a in list)
        {
            foreach(Effect e in a.buff)
            {
                if(e.GetType() == typeof(Shield))
                {
                    x++;
                    break;
                }
            }
        }
        return x;
    }

    public SoothingMelodyHot MySoothingMelody(Character target, Character agent)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(SoothingMelodyHot) && e.attacker == agent) return (SoothingMelodyHot)e;
        return null;
    }
    public UpliftHot MyUplift(Character target, Character agent)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(UpliftHot) && e.attacker == agent) return (UpliftHot)e;
        return null;
    }
    public InspiringTuneBuff MyInspiringTune(Character target, Character agent)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(InspiringTuneBuff) && e.attacker == agent) return (InspiringTuneBuff)e;
        return null;
    }
    public InTuneBuff MyInTune(Character target, Character agent)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(InTuneBuff) && e.attacker == agent) return (InTuneBuff)e;
        return null;
    }
    public Crescendo1 MyCrescendo(Character target, Character agent)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(Crescendo1) && e.attacker == agent) return (Crescendo1)e;
        return null;
    }
    public int SetupCount(Character target)
    {
        foreach (Effect e in target.buff)
        {
            if (e.GetType() == typeof(SetupBuff))
            {
                SetupBuff s = (SetupBuff)e;
                return s.count;
            }
        }
        return 0;
    }
    public HemorageBleed HemorageCheck(Character target)
    {
        foreach (Effect e in target.debuff) if (e.GetType() == typeof(HemorageBleed)) return (HemorageBleed)e;
        return null;
    }
    public RaggedWoundBleed RaggedWoundCheck(Character target)
    {
        foreach (Effect e in target.debuff)
        {
            if (e.GetType() == typeof(RaggedWoundBleed))
            {
                return (RaggedWoundBleed)e;
            }
        }
        return null;
    }
    public SetupBuff MySetup(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(SetupBuff)) return (SetupBuff)e;
        return null;
    }
    public bool RotCheck(Character character, Character agent)
    {
        if (MyRot(character,agent) == null || MyRot(character,agent).timer < 1f) return true;
        return false;
    }
    public bool SnakeBiteCheck(Character character, Character agent)
    {
        if (MySnakeBite(character,agent) == null || MySnakeBite(character,agent).timer < 1f) return true;
        return false;
    }

    public bool CastingRot(Character character)
    {
        foreach (Character e in DungeonManager.instance.currentDungeon.currentEncounter.Druid()) if (character.state == DecisionState.Attack2) return true;
        return false;
    }

    public bool CastingSnakeBite(Character character)
    {
        foreach (Character e in DungeonManager.instance.currentDungeon.currentEncounter.Druid()) if (character.state == DecisionState.Attack3) return true;
        return false;
    }

    public Character FindRessurectTarget()
    {
        return DungeonManager.instance.currentDungeon.currentEncounter.KOPlayer()[0];
    }

    public Effect GreaterHealHotCheck(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(GreaterHealHot)) return e;
        return null;
    }

    public RotDot MyRot(Character target, Character agent)
    {
        foreach (Effect e in target.debuff) if (e.GetType() == typeof(RotDot) && e.attacker == agent) return (RotDot)e;
        return null;
    }
    public SnakeBiteDot MySnakeBite(Character target, Character agent)
    {
        foreach (Effect e in target.debuff) if (e.GetType() == typeof(SnakeBiteDot) && e.attacker == agent) return (SnakeBiteDot)e;
        return null;
    }
    public float TotalHotBuffs(Character target)
    {
        float timeRemaining=0;
        foreach (Effect e in target.buff)
        {
            if(e.type1 == EffectType.Hot|| e.type2 == EffectType.Hot|| e.type3 == EffectType.Hot) timeRemaining += e.timeRemaining;
        }
        return timeRemaining;
    }

    public bool LastStandCheck(Character target)
    {
        foreach (Effect e in target.buff) if (e.GetType() == typeof(LastStandBuff)) return true;
        return false;
    }

    internal CaltropsDebuff CaltropCheck(Character currentCharacter)
    {
        foreach (Effect e in currentCharacter.debuff) if (e.GetType() == typeof(CaltropsDebuff)) return (CaltropsDebuff)e;
        return null;
    }
    internal RedoubtBuff Redoubt(Character character)
    {
        foreach (Effect e in character.buff) if (e.GetType() == typeof(RedoubtBuff)) return (RedoubtBuff)e;
        return null;
    }
    internal ShieldSpikeBuff ShieldSpike(Character character)
    {
        foreach (Effect e in character.buff) if (e.GetType() == typeof(ShieldSpikeBuff)) return (ShieldSpikeBuff)e;
        return null;
    }
    public SavageBlowDot SavageBlowDotCheck(Character character)
    {
        foreach (Effect e in character.debuff) if (e.GetType() == typeof(SavageBlowDot)) return (SavageBlowDot)e;
        return null;
    }
}
