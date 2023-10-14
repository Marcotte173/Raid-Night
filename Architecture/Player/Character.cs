using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum Species {Human, LizardFolk,Elf,Dwarf,Orc }
public enum DecisionState { Downtime, Decision, Attack1, Attack2, Attack3, Attack4, Attack5, Attack6,StopDPS,StopMoving,Move,Wait,Asleep,Cast,DashCast,Adjust,Knockback,Reading}
public class Character : MonoBehaviour
{
    public int id;
    public bool ko;
    public Species species;
    public CharacterUI playerUI;
    [HideInInspector]
    public Head head;
    [HideInInspector]
    public Chest chest;
    [HideInInspector]
    public Legs legs;
    [HideInInspector]
    public Feet feet;
    [HideInInspector]
    public Trinket trinket;
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public OffHand offHand;
    public Character target;
    [HideInInspector]
    public Character targetFromTaunt;
    [HideInInspector]
    public Character orderTarget;
    public string characterName;
    [HideInInspector]
    public float resPriority;    
    [Header("------------------------------------------------------")]
    [Header("Starting Values")]    
    public CharacterAttribute maxHealth;    
    public CharacterAttribute maxMana;
    public CharacterAttribute defence;
    public CharacterAttribute attackPower;
    public CharacterAttribute spellpower;
    public CharacterAttribute damage;
    public CharacterAttribute crit;
    public CharacterAttribute vamp;
    public CharacterAttribute movement;
    public CharacterAttribute haste;
    public CharacterAttribute thorns;
    public CharacterAttribute manaRegenTime;
    public CharacterAttribute manaRegenValue;
    [Header("------------------------------------------------------")]
    [Header("Current Values")]
    public float health;
    public float mana;
    [Header("------------------------------------------------------")]
    [Header("Modifiers")]
    public CharacterAttribute physicalDamageMod;
    public CharacterAttribute magicDamageMod;
    public CharacterAttribute manaRegenMod;
    public CharacterAttribute damageTakenMod;
    public CharacterAttribute healingMod;
    public CharacterAttribute energyCostMod;
    [HideInInspector]
    public float manaRegenTimer;
    [HideInInspector]
    public bool untargetable;
    [HideInInspector]
    public float castTimer;    
    [HideInInspector]
    public float decisionTimer;
    [HideInInspector]
    public float downTimer;
    [Header("------------------------------------------------------")]
    [Header("Control")]
    public float decisionTime;
    [HideInInspector]
    public float waitTimer;
    public float downTime;
    [HideInInspector]
    public float readTimer;
    public float readTime;
    public float waitTime = 3;
    [HideInInspector]
    public float damageDone;
    [HideInInspector]
    public float damageTaken;
    public DecisionState state;
    [HideInInspector]
    public List<Hazard> hazards;
    public Move move;
    public Player player;
    [HideInInspector]
    public string action;
    public float actionCast;
    [Header("------------------------------------------------------")]
    [Header("Buff/Debuff")]
    public List<Effect> buff;
    public List<Effect> debuff;
    public bool canCastSpells;
    [HideInInspector]
    public bool thornsAoe;
    [HideInInspector]
    public bool protectionDash;
    [HideInInspector]
    public Character dashCastTarget;
    [HideInInspector]
    public Character primaryHealTarget;
    public List<Ability> ability;
    public Tile moveTile;
    [HideInInspector]
    public bool moveWait;
    [HideInInspector]
    public bool stopMoving;
    [HideInInspector]
    public float stopMovingTimer;
    [HideInInspector]
    public bool dash;
    [HideInInspector]
    public bool dashDirect;
    [HideInInspector]
    public Tile knockbackTile;
    [HideInInspector]
    public float knockbackAmount;
    [HideInInspector]
    public bool knockBack;
    [HideInInspector]
    public Tile knockBackSource;
    [HideInInspector]
    public Tile knockBackDestination;
    [HideInInspector]
    public float targetOverrideTimer;
    [HideInInspector]
    public float characterTree;


    public virtual void CoreStats()
    {
        movement.baseValue = 1;
        haste.baseValue = 0;
        thorns.baseValue = 0;
        physicalDamageMod.baseValue = 1;
        magicDamageMod.baseValue = 1;
        manaRegenMod.baseValue = 1;
        damageTakenMod.baseValue = 1;
        healingMod.baseValue = 1;
        energyCostMod.baseValue = 1;
    }
    public void GetAgentBody()
    {
        head = GetComponentInChildren<Head>();
        chest= GetComponentInChildren<Chest>();
        legs = GetComponentInChildren<Legs>();
        feet = GetComponentInChildren<Feet>();
        trinket = GetComponentInChildren<Trinket>();
        weapon  = GetComponentInChildren<Weapon>();
        offHand = GetComponentInChildren<OffHand>();
        head.character = this;
        chest.character = this;
        legs.character = this;
        feet.character = this;
        trinket.character = this;
        weapon.character = this;
        offHand.character = this;
    }

    public void Awake()
    {
        GetAgentBody();              
    }

    public virtual void AbilityPopulate()
    {
        
    }
    
    public virtual void UpdateStuff()
    {
        if (stopMoving)
        {
            stopMovingTimer -= Time.deltaTime;
            if (stopMovingTimer <= 0)
            {
                stopMoving = false;
            }
        }
        for (int i = 0; i < ability.Count; i++) Cooldown(ability[i]);
        manaRegenTimer -= UnityEngine.Time.deltaTime;
        if (manaRegenTimer <= 0)
        {
            ManaGain(manaRegenValue.value);
            manaRegenTimer = manaRegenTime.value;
        }
        if (GetComponent<Class>()) if (GetComponent<Class>().startTimeInFight) GetComponent<Class>().timeInFight+= UnityEngine.Time.deltaTime;
    }

    private void Cooldown(Ability ability)
    {
        if (ability.cooldownTimer > 0) ability.cooldownTimer = (ability.cooldownTimer - Time.deltaTime >= 0) ? ability.cooldownTimer -= Time.deltaTime : 0;
    }

    public void Run(List<Tile> tiles)
    {
        moveTile = FindClosestTile(tiles);
        state = DecisionState.Move;
        moveWait = true;
        tiles.Remove(moveTile);
    }
    public void Run(Tile tile)
    {
        moveTile = tile;
        state = DecisionState.Move;
        moveWait = true;
    }
    public virtual void Create()
    {

    }
    public virtual void StartEncounter()
    {
        canCastSpells = true;
    }
    public void State()
    {
        if(targetOverrideTimer>0)targetOverrideTimer -= Time.deltaTime;
        if (UserControl.instance.control == Control.UserControl && UserControl.instance.controlledCharacter == this) { }
        else
        {
            if (state == DecisionState.Asleep) { }
            else if (ShareTile())
            {
                this.move.nextTile = null;
                this.move.nextTile = FindTile.instance.FindUnoccupiedTileAdjacentToTarget(this.move.currentTile);
                if (this.move.nextTile != null) state = DecisionState.Adjust;
                else
                {
                    waitTimer = 3;
                    state = DecisionState.Wait;
                }
            }
            else if (state == DecisionState.Adjust)
            {
                if (MoveManager.instance.IsAtTile(this.move, this.move.nextTile)) state = DecisionState.Decision;
                else MoveManager.instance.Move(this.move);
            }
            else if (state == DecisionState.Wait) Wait();
            else if (state == DecisionState.StopDPS) StopDPS();
            else if (target != null && target.ko)
            {
                if (DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion().Count == 0 || DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion().Count == 0) state = DecisionState.Downtime;
                else
                {
                    GetTarget();
                    state = DecisionState.Decision;
                }
            }
            else if (state == DecisionState.Knockback)
            {
                if (!knockBack)
                {
                    action = $"Knocked Back";
                    movement.AddModifier(new StatModifier(10, StatModType.Flat, this));
                    knockBack = true;
                    knockBackDestination = null;
                }
                else
                {
                    if (knockbackAmount > 0)
                    {                        
                        Knockback();
                    }
                    else
                    {
                        movement.RemoveAllModifiersFromSource(this);
                        knockBack = false;
                        state = DecisionState.Downtime;
                        knockBackDestination = null;
                    }
                }                
            }
            //else if (!GetComponent<Summon>() && GetComponent<Player>() && player.AggroHigh()) player.AggroInterupt();
            //else if (!GetComponent<Summon>() && GetComponent<Player>() && hazards.Count > 0) player.HazardInterupt();            
            else if (state == DecisionState.Downtime)
            {
                action = $"Downtime";
                move.isMoving = false;
                downTimer -= Time.deltaTime;
                if (downTimer <= 0)
                {
                    if (characterTree == 1) state = DecisionState.Reading;
                    else state = DecisionState.Decision;
                    downTimer = downTime;
                }
            }
            else if (state == DecisionState.Reading)
            {
                if (DungeonManager.instance.raidMode == RaidMode.Combat)
                {
                    action = $"Reading";
                    readTimer -= Time.deltaTime;
                    if (readTimer <= 0)
                    {                        
                        readTimer = readTime;
                        state = DecisionState.Decision;
                    }
                }

            }
            else if (state == DecisionState.Decision)
            {
                if (DungeonManager.instance.raidMode == RaidMode.Combat)
                {
                    action = $"Thinking";
                    decisionTimer -= Time.deltaTime;
                    if (decisionTimer <= 0)
                    {                        
                        decisionTimer = decisionTime;
                        GetTarget();
                        Decision();
                    }
                }
            }
        }
        if (state == DecisionState.Move) Move();
        else if (state == DecisionState.DashCast)
        {
            if (!dash)
            {
                action = $"Dashing towards {dashCastTarget.characterName}";
                movement.AddModifier(new StatModifier(10, StatModType.Flat, this));
                dash = true;
            }           
            DashCast();
        }
        else if (state == DecisionState.Cast) Cast();
        else if (state == DecisionState.Attack1) Attack(0);
        else if (state == DecisionState.Attack2) Attack(1);
        else if (state == DecisionState.Attack3) Attack(2);
        else if (state == DecisionState.Attack4) Attack(3);
        else if (state == DecisionState.Attack5) Attack(4);
        else if (state == DecisionState.Attack6) Attack(5);
    }

    private void Knockback()
    {
        if(knockBackDestination != null)
        {
            if (move.currentTile== knockBackDestination)
            {                
                knockbackAmount--;
                if (knockbackAmount > 0)
                {
                    knockBackDestination = FindTile.instance.Otherside(knockBackSource, move.currentTile);
                    knockBackSource = move.currentTile;
                }
            }
            else MoveManager.instance.MoveAgentDirect(move, knockBackDestination);
        }        
        else
        {
            knockBackDestination = FindTile.instance.Otherside(knockBackSource, move.currentTile);
            knockBackSource = move.currentTile;
        }
    }

    public void OutlineOn(int x)
    {
        Utility.instance.TurnOn(playerUI.outline);
        playerUI.outline.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.outline[x];
    }
    public void OutlineOff()
    {
        Utility.instance.TurnOff(playerUI.outline);
    }
    public bool Enemy(Character character, Character target)
    {
        if (DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion().Contains(character))
        {
            if (DungeonManager.instance.currentDungeon.currentEncounter.BossAndMinion().Contains(target)) return true;
            else return false;
        }
        else
        {
            if (DungeonManager.instance.currentDungeon.currentEncounter.PlayerAndMinion().Contains(target)) return true;
            else return false;
        }
    }
    public void AbilityLoad(List<Ability> list)
    {
        if (ability.Count > 0)
        {
            for (int i = 0; i < ability.Count; i++) Destroy(ability[i].gameObject);
            ability.Clear();            
        }
        AbilityAdd(AbilityList.instance.basicAttack);
        foreach (Ability ab in list) AbilityAdd(ab);
    }
    public void AbilityAdd(Ability ab)
    {
        Ability e = Instantiate(ab, transform);
        e.name = ab.abilityName;
        ability.Add(e);
        e.character = this;
    }
    public void TargetOff()
    {
        if(target != null)
        {
            if (target == this) playerUI.outline.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.outline[3];
            else Utility.instance.TurnOff(target.playerUI.outline);
            target = null;
        }        
    }
    public void Attack(int x)
    {
        AbilityUse(x);
    }
    public void AbilityUse(int x)
    {
        if (!ability[x].passive) ability[x].Use();
    }
    private bool ShareTile()
    {
        foreach (Character a in  DungeonManager.instance.currentDungeon.currentEncounter.CharactersWhoAreNotMe(this))
        {
            if (this.move.currentTile == a.move.currentTile&& !a.move.isMoving && !this.move.isMoving) return true;
        }
        return false;
    }

    public void Cast()
    {
        for (int i = 0; i < ability.Count; i++) if (ability[i].cast) ability[i].Cast();
    }

    public void CastOff()
    {
        for (int i = 0; i < ability.Count; i++) ability[i].cast = false;
    }
    private void Wait()
    {
        waitTimer -= UnityEngine.Time.deltaTime;
        if (waitTimer <= 0)
        {
            waitTimer = waitTime;
            state = DecisionState.Downtime;
        }
    }

    public virtual void StopDPS()
    {
        action = "Waiting";
        waitTimer = 3;
        state = DecisionState.Wait;
    }

    public virtual void Move()
    {
        if (MoveManager.instance.IsAtTile(move, moveTile))
        {
            action = "";
            if (moveWait)
            {
                state = DecisionState.Wait;
                moveWait = false;
                waitTimer = 4;
            }
            else state = DecisionState.Downtime;
        }
        else
        {
            action = "Moving";
            MoveManager.instance.MoveAgentDirect(move, moveTile);
        }        
    }
    public void DashCast()
    {
        if (FindTile.instance.IsAdjacent(move.currentTile, dashCastTarget.move.currentTile))
        {
            for (int i = 0; i < ability.Count; i++) if (ability[i].dash) ability[i].DashTrigger();
            movement.RemoveAllModifiersFromSource(this);
            dash = false;
        }
        else
        {            
            if(dashDirect) MoveManager.instance.MoveAgentDirect(move, dashCastTarget.move.currentTile);
            else MoveManager.instance.MoveAgent(move, dashCastTarget.move.currentTile);
        }        
    }

    public virtual void Decision()
    {
        
    }

    public virtual void GetTarget()
    {
        
    }

    public float Health() => health;
    public float Mana() => mana;
    public float Damage() => damage.value + attackPower.value/10;   
    public float Score() => head.score + chest.score + legs.score + feet.score + trinket.score + weapon.score + offHand.score;
    public void TakeDamage(Character attacker, float damage, float aggroNumber, bool physical, string ability)
    {
        //Apply Damage Mods
        if (physical) damage *= attacker.physicalDamageMod.value;
        else damage *= attacker.magicDamageMod.value;        
        //Start Time in Fight
        if (attacker.GetComponent<Class>())
        {
            Class c = (Class)attacker;
            if (!c.startTimeInFight) c.startTimeInFight = true;
        }
        //If Physical
        //Determine crit and armor mitigation    
        bool crit = false;
        if (physical)
        {
            if (UnityEngine.Random.Range(1, 101) < attacker.crit.value)
            {
                damage *= 2;
                crit = true;
                //If beserker, check if a crit triggers raging endurance
                if (GetComponent<Beserker>()) RagingEnduranceCheck();
            }            
        }
        //Thorns
        if (thorns.value > 0) attacker.TakeDamage(this, thorns.value, 0, true, "Thorns: ");
        if (physical) damage *= (GameManager.instance.defModifier / (GameManager.instance.defModifier + defence.value));
        //Apply Damage Mitigation mod
        damage *= damageTakenMod.value;
        //If Beserker, Check if it mitigates
        if (GetComponent<Beserker>() && GetComponent<Beserker>().spec == Spec.Stalwart)
        {
            Beserker b = GetComponent<Beserker>();
            RagingEndurance r = (RagingEndurance)b.ability[3];
            if (r.mitigate)
            {
                damage *= r.mitigationAmount;
                r.mitigate = false;
            }
        }
        //Apply Shield
        if (AbilityCheck.instance.ShieldValue(this) > 0)
        {
            float damageBlocked = (damage <= AbilityCheck.instance.ShieldValue(this)) ? damage : AbilityCheck.instance.ShieldValue(this);
            AbilityCheck.instance.MyShield(this).ShieldDamage(damageBlocked);
            Utility.instance.DamageNumber(GetComponent<Class>(), "Shield: " + Convert.ToInt32(damageBlocked).ToString(), SpriteList.instance.health);
            damage -= damageBlocked;            
        }
        if (damage > 0)
        {
            health -= damage;
            //If Last Stand is active, Character cannot drop below 1 HP
            if (AbilityCheck.instance.LastStandCheck(this))
            {
                if (health <= 0)
                {
                    health = 1;
                    Utility.instance.DamageNumber(GetComponent<Class>(), "Last Stand", SpriteList.instance.shieldBearer);
                }                
            }
            //If Beserker, Check for Raging Endurance. Add energy based on damage Taken
            if (GetComponent<Beserker>()&& GetComponent<Beserker>().spec == Spec.Stalwart)
            {
                if (Health() / maxHealth.value <= GetComponent<Beserker>().ability[3].damage) RagingEnduranceCheck();
                mana += Mathf.Round(damage * GetComponent<Beserker>().energyGainOnReceiveHit);
            }
            //Heal the player for vamp
            if (attacker.vamp.value > 0) attacker.Heal(damage * attacker.vamp.value / 100f,false,null);
            //Record Damage Taken
            damageTaken += damage;
            //Record Damage Done
            attacker.damageDone += damage;
            //If the defender is a boss, get aggro
            //Also, do damage Numbers
            if (GetComponent<Boss>())
            {
                Utility.instance.Aggro(GetComponent<Boss>(), attacker, aggroNumber);
                Utility.instance.DamageNumber(GetComponent<Boss>(),ability + Convert.ToInt32(damage).ToString(), (crit) ? SpriteList.instance.critColor : (physical) ? SpriteList.instance.damageColor : SpriteList.instance.magicColor);
            }
            else Utility.instance.DamageNumber(GetComponent<Class>(), ability + Convert.ToInt32(damage).ToString(), (crit) ? SpriteList.instance.critColor : (physical) ? SpriteList.instance.damageColor : SpriteList.instance.magicColor);
            //Check for Death
            if (Health() <= 0)
            {
                Death();
            }
        }        
    }

    private void RagingEnduranceCheck()
    {
        Beserker b = GetComponent<Beserker>();
        if (b.spec == Spec.Stalwart && b.ability[3].cooldownTimer <= 0) b.ability[3].Use();        
    }

    public virtual void TakeDamage(Character attacker, float damage, bool physical, string ability)
    {
        TakeDamage(attacker, damage, 0, physical, ability);
    }

    internal void Heal(float x, bool aggro, Character healer)
    {        
        x *= healingMod.value;
        float healing = (Health() + x <= maxHealth.value) ? x : maxHealth.value - Health();
        health += healing;
        if (aggro) foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Boss()) Utility.instance.Aggro(a.GetComponent<Boss>(), healer, healing * GameManager.instance.healThreat / 100);
        if (!ko) Utility.instance.DamageNumber(this, Convert.ToInt32(x).ToString(),  SpriteList.instance.healColor);
    }

    public void ManaGain(float x)
    {
        x *= manaRegenMod.value;
        float manaGaining = (Mana() + x <= maxMana.value)?x:maxMana.value - Mana();
        mana += manaGaining;
    }
    public virtual void Death()
    {
        ResetCurrentInfo();        
        Utility.instance.DamageNumber(this, "KO", SpriteList.instance.rage);
        if (GetComponent<Summon>())
        {
            DungeonManager.instance.currentDungeon.currentEncounter.playerMinionSummons.Remove(this);
            Destroy(gameObject);
        }
        else
        {
            ko = true;
            SpriteOff();
            Utility.instance.TurnOff(playerUI.outline);
            mana = 0;
            if (DungeonManager.instance.currentDungeon.currentEncounter.Player().Count == 0 || DungeonManager.instance.currentDungeon.currentEncounter.Boss().Count == 0)
            {                
                DungeonManager.instance.raidMode = RaidMode.Resolve;
                string x = "";
                foreach (GameObject g in DungeonManager.instance.currentDungeon.currentEncounter.objects.ToList())
                {                    
                    if (g != null)
                    {
                        x += $"{g.name},";
                        if (g.GetComponent<Effect>()) g.GetComponent<Effect>().EffectEnd();
                        else Destroy(g);
                    }
                }
                Debug.Log(x);
                DungeonManager.instance.currentDungeon.currentEncounter.objects.Clear();
                EncounterUI.instance.UpdateSmallUI();
                EndMatch.instance.FindWinner();
            }
        }        
    }

    public void ResetCurrentInfo()
    {
        move.nextTile = null;
        move.path.Clear();
        target = null;
        action = "";
        foreach (Effect e in buff.ToList()) e.go = false;
        foreach (Effect e in debuff.ToList()) e.go = false;
        foreach (Effect e in buff.ToList()) e.EffectEnd(); 
        foreach (Effect e in debuff.ToList()) e.EffectEnd();
        buff.Clear();
        debuff.Clear();
        state = DecisionState.Downtime;
        CastOff();
    }

    public void SpriteOff()
    {
        Utility.instance.TurnOff(playerUI.gameObject);
       
    }
    public virtual void SpriteOn()
    {
        Utility.instance.TurnOn(playerUI.gameObject);
        playerUI.characterNameText.text = characterName;
        playerUI.head.sprite = head.pic;
        playerUI.chest.sprite = chest.pic;
        playerUI.legs.sprite= legs.pic;
        playerUI.feet.sprite = feet.pic;
        playerUI.weapon.sprite = weapon.pic;
        playerUI.offHand.sprite  = offHand.pic;        
    }
    public float CastTimer(float timer)
    {        
        return timer *= (GameManager.instance.hasteModifier / (GameManager.instance.hasteModifier + haste.value));
    }
    public float CooldownTimer(float timer)
    {
        return timer *= (GameManager.instance.cooldownModifier / (GameManager.instance.cooldownModifier + haste.value));
    }
    public List<Character> InRange(Character c, float range,List<Character> list)
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in list) if (Vector2.Distance(c.transform.position, a.transform.position) <= range) newList.Add(a);
        return newList;
    }
    public Character FindAdjacentEnemy(List<Tile> tiles, List<Character> list)
    {
        foreach (Tile t in tiles)
        {
            foreach (Tile n in t.neighbor)
            {
                foreach (Character a in list)
                {
                    if (a.move.currentTile == n && !tiles.Contains(n))
                    {
                        tiles.Add(n);
                        return a;
                    }
                }
            }
        }
        return null;
    }
    public List<Tile> TileRadius(Tile tile,int radius)
    {
        List<Tile> list = new List<Tile> { };
        list.Add(tile);
        for (int i = 0; i < radius; i++) foreach (Tile t in list.ToList()) foreach (Tile n in t.neighbor) if (!list.Contains(n)) list.Add(n);
        return list;
    }
    public void GetPrimaryHealTarget()
    {
        if (DungeonManager.instance.currentDungeon.currentEncounter.Player().Count == 1) primaryHealTarget = this;
        else
        {
            List<Character> potential = new List<Character> { };
            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Player())
            {
                Class c = (Class)a;
                if (a != this) if (c.spec == Spec.Stalwart) potential.Add(a);
            }
            if (potential.Count == 0) foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Player()) if (a != this) if (a.GetComponentInChildren<Bard>() || a.GetComponentInChildren<Rogue>()) potential.Add(a);
            if (potential.Count == 0) foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.Player()) if (a != this) potential.Add(a);
            primaryHealTarget = potential[0];
            if (potential.Count > 1) for (int i = 1; i < potential.Count; i++) if (potential[i].maxHealth.value > primaryHealTarget.maxHealth.value) primaryHealTarget = potential[i];
        }
    }

    public Tile FindClosestTile(List<Tile> list)
    {
        Tile target = list[0];
        //Check each other in the list
        foreach (Tile c in list)
        {
            //If the distance to them is shorter
            if (Vector3.Distance(transform.position, c.transform.position) < Vector3.Distance(transform.position, target.transform.position)) target = c;
        }
        return target;        
    }
}
