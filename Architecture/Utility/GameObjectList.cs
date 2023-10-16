using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectList : MonoBehaviour
{
    public static GameObjectList instance;
    public Tile tile;
    public GameObject agent;
    public Encounter encounter;
    public Aggro aggro;
    public Wolf wolf;
    public Player player;
    public Projectile simpleProjectile;
    public ProjectilePlayer playerProjectile;
    public ProjectileEnemy enemyProjectile;
    public CleansingFlame orcHazard;
    public ClawsFromTheDeepHazard clawsFromTheDeep;
    public ShieldWallInspireHazard shieldWallInspire;
    public Animator animator;
    public EssenceBurnBuff essenceBurn;
    public EssenceRecovery essenceRecovery;
    public RotDot rot;
    public SnakeBiteDot snakeBite;
    public SavageBlowDot savageBlowDot;
    public TauntDebuff taunt;
    public ArcaneTendrils arcaneTendrils;
    public TooAngryToDieBuff tooAngryToDie;
    public WarcryBuff warcry;
    public InspiringTuneBuff inspiringTune;
    public InTuneBuff inTune;
    public TimeDecayBuff timeDecayBuff;
    public TimeDecayDebuff timeDecayDebuff;
    public Crescendo1 crescendo1;
    public Crescendo2 crescendo2;
    public LastStandBuff lastStand;
    public Shield shield;
    public ShieldSpikeBuff shieldSpike;
    public RedoubtBuff redoubt;
    public RecklessSwingDefenceNerf recklessSwingDefenceNerf;
    public GreaterHealHot greaterHealHot;
    public HibernateBuff hibernate;
    public SoothingMelodyHot soothingMelodyHot;
    public UpliftHot upliftHot;
    public DirgeOfDemiseBuff dirgeOfDemiseBuff;
    public FinaleBuff finaleBuff;
    public DualWield dualWield;
    public FinaleInspiring finaleInspire;
    public DamageNumbers damageNumbers;
    public SetupBuff setup;
    public RaggedWoundBleed raggedWound;
    public CaltropsDebuff caltrops;
    public CaltropHazard caltropHazard;
    public HemorageBleed hemorage;
    public BloodyMessBleed bloodyMess;
    public Animator outlineAnimator;
    public RegenerationHOT regeneration;
    public BurnDot burn;
    public RuntimeAnimatorController disintigrate;
    public RuntimeAnimatorController explosion;
    public RuntimeAnimatorController fireball;
    public RuntimeAnimatorController caltrop;
    public List<PreEncounter> preEncounter;
    public FuryOfBladesBuff furyOfBlades;
    public Flag flag;
    public AggroBelief aggroBelief;
    public List<Trait> traits;
    public RaidRoster raidRoster;
    public Codex entry;
    private void Awake()
    {
        instance = this;
    }
}
