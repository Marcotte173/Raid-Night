using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Net;

public enum Selected { NONE, All, DPS, TANK, SUPPORT, SELECT };
public enum Orders { NONE, Red, Blue, Green, Yellow, Close,Focus,Stop,Resume,HazardMove,ToBoss,FromBoss };
public class Encounter : MonoBehaviour
{
    [HideInInspector]
    public int id;
    [HideInInspector]
    public int attempt;
    public List<Character> bossSummon;
    [HideInInspector]
    public List<Character> boss;
    [HideInInspector]
    public List<Character> player;
    [HideInInspector]
    public List<Character> playerMinionSummons;
    [HideInInspector]
    public List<Character> bossMinionSummons;
    [HideInInspector]
    public List<Tile> tileList;
    [HideInInspector]
    public List<Tile> occupied;
    public int arenaSizeX;
    public int arenaSizeY;
    public float cameraY;
    public float cameraSize;
    public int dropAmount;
    public List<ItemSO> possibleDrops;
    [HideInInspector]
    public List<ItemSO> drops;
    [HideInInspector]
    public List<Hazard> enemyHazards;
    [HideInInspector]
    public List<Hazard> playerHazards;   
    public int pullRange;
    [HideInInspector]
    public List<GameObject> objects;    
    public int pullTime;
    public int bossFightTime;
    public List<Vector2> bossLocation;
    [HideInInspector]
    public PreEncounter preEncounter;
    [HideInInspector]
    public Character tank;
    [HideInInspector]
    public Character offTank;
    [HideInInspector]
    public Character healer;
    [HideInInspector]
    public Character offHealer;
    [HideInInspector]
    public List<Tile> characterMoveTiles;
    [HideInInspector]
    public List<Tile> possibleFlagTiles;
    [HideInInspector]
    public List<Tile> redFlagTiles;
    [HideInInspector]
    public List<Tile> greenFlagTiles;
    [HideInInspector]
    public List<Tile> blueFlagTiles;
    [HideInInspector]
    public List<Tile> yellowFlagTiles;
    public int howManyFlags;
    [HideInInspector]
    public Selected selected;
    [HideInInspector]
    public Orders orders;
    [HideInInspector]
    public Character selectedTarget;
    [HideInInspector]
    public List<Character> selectedCharacters;
    [HideInInspector]
    public float orderWaitLength;
    [HideInInspector]
    public float targetOverrideLength;
    public float orderTextLength;
    [HideInInspector]
    public string selectText;
    [HideInInspector]
    public string orderText;
    [HideInInspector]
    public string fullOrderText;
    [HideInInspector]
    public float orderCooldownTimer;
    [HideInInspector]
    public float orderTimer;

    public void Awake()
    {
        preEncounter = GetComponent<PreEncounter>();
    }

    public void EncounterUpdate()
    {
        selectText = (orderCooldownTimer > 0) ? $"Orders Cooldown: {Math.Round(orderCooldownTimer, 2)}" : (selected == Selected.NONE) ? "" : (selected == Selected.All) ? "Selected: All" : (selected == Selected.DPS) ? "Selected: DPS" : (selected == Selected.SUPPORT) ? "Selected: Support" : (selected == Selected.TANK) ? "Selected: Tank" : $"Selected: {UserControl.instance.selectedCharacter.characterName}";
        orderText = (selected == Selected.NONE) ? "" : (orders == Orders.NONE) ? "Orders: None" : (orders == Orders.Red) ? "Orders: Run To Red Flag" : (orders == Orders.Blue) ? "Orders: Run To Blue Flag" : (orders == Orders.Green) ? "Orders: Run To Green Flag" : (orders == Orders.Yellow) ? "Orders: Run To Yellow Flag" : (orders == Orders.Focus && selectedTarget == null) ? "Select a Target to Focus" : (orders == Orders.Focus && selectedTarget != null) ? $"Orders: Focus on {selectedTarget.characterName}" : (orders == Orders.Close) ? "Orders: Run to Nearest Flag" : (orders == Orders.Stop) ? "Orders: Stop" : "Orders: Resume";
        if (selected == Selected.NONE) orders = Orders.NONE;
        if (orderCooldownTimer > 0)
        {
            orderCooldownTimer -= Time.deltaTime;
        }
        if (orderTimer > 0)
        {
            orderTimer -= Time.deltaTime;
            if (orderTimer <= 0) fullOrderText = "";
        }
    }
    public List<Character> Characters()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in boss) if (!a.ko)newList.Add(a);
        foreach (Character a in player) if (!a.ko) newList.Add(a);
        foreach (Character a in playerMinionSummons) if (!a.ko) newList.Add(a);
        foreach (Character a in bossMinionSummons) if (!a.ko) newList.Add(a);
        return newList;
    }
    public List<Character> CharactersWhoAreNotMe(Character Character)
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in boss) if (!a.ko && a != Character) newList.Add(a);
        foreach (Character a in player) if (!a.ko && a != Character) newList.Add(a);
        foreach (Character a in playerMinionSummons) if (!a.ko && a != Character) newList.Add(a);
        foreach (Character a in bossMinionSummons) if (!a.ko && a != Character) newList.Add(a);
        return newList;
    }
    public List<Character> Player()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (!a.ko) newList.Add(a);
        return newList;
    }
    public List<Character> PlayerMinion()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in playerMinionSummons) if (!a.ko) newList.Add(a);
        return newList;
    }
    public List<Character> PlayerAndMinion()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (!a.ko) newList.Add(a);
        foreach (Character a in playerMinionSummons) if (!a.ko) newList.Add(a);
        return newList;
    }
    public List<Character> BossMinion()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in bossMinionSummons) if (!a.ko) newList.Add(a);
        return newList;
    }
    public List<Character> Bard()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (a.GetComponent<Bard>()) newList.Add(a);
        return newList;
    }
    public List<Character> Mage()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (a.GetComponent<Mage>()) newList.Add(a);
        return newList;
    }
    public List<Character> Beserker()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (a.GetComponent<Beserker>()) newList.Add(a);
        return newList;
    }
    public List<Character> Shieldbearer()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (a.GetComponent<ShieldBearer>()) newList.Add(a);
        return newList;
    }
    public List<Character> Rogue()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (a.GetComponent<Rogue>()) newList.Add(a);
        return newList;
    }
    public List<Character> Druid()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (a.GetComponent<Druid>()) newList.Add(a);
        return newList;
    }
    public List<Character> KOPlayer()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in player) if (a.ko) newList.Add(a);
        return newList;
    }
    public List<Character> Boss()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in boss) if (!a.ko) newList.Add(a);
        return newList;
    }
    public List<Character> BossAndMinion()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in boss) if (!a.ko) newList.Add(a);
        foreach (Character a in bossMinionSummons) if (!a.ko) newList.Add(a);
        return newList;
    }
    public List<Character> KOBoss()
    {
        List<Character> newList = new List<Character> { };
        foreach (Character a in boss) if (a.ko) newList.Add(a);
        return newList;
    }
    public virtual void CreateArena()
    {
        for (int y = 0; y < arenaSizeY; y++) for (int x = 0; x < arenaSizeX; x++) tileList.Add(TileCreate(x, y));
        foreach (Tile l in tileList)
        {
            foreach (Tile loc in tileList)
            {
                if (loc.x == l.x + 1 && loc.y == l.y) if (!l.neighbor.Contains(loc)) l.neighbor.Add(loc);
                if (loc.x == l.x - 1 && loc.y == l.y) if (!l.neighbor.Contains(loc)) l.neighbor.Add(loc);
                if (loc.x == l.x && loc.y == l.y + 1) if (!l.neighbor.Contains(loc)) l.neighbor.Add(loc);
                if (loc.x == l.x && loc.y == l.y - 1) if (!l.neighbor.Contains(loc)) l.neighbor.Add(loc);

                if (loc.x == l.x + 1 && loc.y == l.y + 1) if (!l.neighbor.Contains(loc)) l.neighbor.Add(loc);
                if (loc.x == l.x - 1 && loc.y == l.y + 1) if (!l.neighbor.Contains(loc)) l.neighbor.Add(loc);
                if (loc.x == l.x + 1 && loc.y == l.y - 1) if (!l.neighbor.Contains(loc)) l.neighbor.Add(loc);
                if (loc.x == l.x - 1 && loc.y == l.y - 1) if (!l.neighbor.Contains(loc)) l.neighbor.Add(loc);
            }
        }        
        Camera.main.transform.position = new Vector3(arenaSizeX / 2, cameraY, -10);
        Camera.main.orthographicSize = cameraSize;
    }    

    public virtual void ResetEncounter()
    {
        foreach (Slider b in EncounterUI.instance.playerHBarUI) Utility.instance.TurnOff(b.gameObject);
        foreach (Slider b in EncounterUI.instance.enemyHBarUI) Utility.instance.TurnOff(b.gameObject);
        ClearAllLists();
        foreach (Character a in player.ToList())
        {
            a.ko = false;
            DungeonManager.instance.LoadInDungeon(a.player);
        }
        player.Clear();
        attempt++;
        PopulateBosses();
        PopulatePlayers();
        if (attempt == 1) BeginPreEncounter();
        else BeginPreSetup();
        TimeManagement.instance.UpdateTimeDisplay();
        ResetOrders();
        EncounterUI.instance.TurnOffBackgrounds();
        UserControl.instance.control = Control.PlayerChoice;
        UIManager.instance.PreEncounter();
    }

    public virtual void PopulatePlayers()
    {
        for (int i = 0; i < DungeonManager.instance.currentDungeon.agentsInDungeon.Count; i++)
        {
            DungeonManager.instance.PutInArena(DungeonManager.instance.currentDungeon.agentsInDungeon[i].player, new Vector2(2, 10-i));
        }
        DungeonManager.instance.currentDungeon.agentsInDungeon.Clear();
        foreach (Character a in player)
        {
            a.ko = false;
            a.AbilityPopulate();
            foreach (Ability ab in a.ability) ab.StartBattle();
            a.ResetCurrentInfo();
            Class c = a.GetComponentInChildren<Class>();
            c.StartEncounter();
            c.SpriteOn();
            Utility.instance.TurnOff(c.playerUI.outline);
            if (!a.GetComponent<Beserker>()) c.mana = c.maxMana.value;
            c.health = c.maxHealth.value;
            a.move.prevPosition = a.transform.position;
        }
    }
    public virtual void CreateDrops()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            drops.Add(possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)]);
        }
    }

    public void BeginPreEncounter()
    {
        PreEncounter();
        EncounterUI.instance.timeText.text = TimeManagement.instance.TimeOfDayDisplay();
        preEncounter.Ready();
    }

    public void BeginPreSetup()
    {
        PreSetup();
        preEncounter.PreSetup();
    }
    public void PreSetup()
    {
        DungeonManager.instance.raidMode = RaidMode.PreSetup;
    }

    public void PreEncounter()
    {
        EncounterUI.instance.currentEncounter = this;
        DungeonManager.instance.raidMode = RaidMode.PreDungeon;
    }

    public virtual void PopulateBosses()
    {
        for (int i = 0; i < bossSummon.Count; i++)
        {
            Character b = Instantiate(bossSummon[i], EncounterUI.instance.charactersGameObject.transform);
            DungeonManager.instance.PutInArena(b, new Vector2(bossLocation[i].x, bossLocation[i].y));
        }
        foreach (Character a in BossAndMinion())
        {
            a.GetAgentBody();
            a.playerUI = a.GetComponent<CharacterUI>();
            a.GetComponent<Boss>().Create();
            a.SpriteOn();
            a.GetComponent<Boss>().AbilityPopulate();
            Utility.instance.TurnOff(a.GetComponent<Boss>().playerUI.outline);
        }
    }
    public void ClearAllLists()
    {
        ClearList(boss);
        ClearList(playerMinionSummons);
        ClearList(bossMinionSummons);
    }
    public void ClearList(List<Character> list)
    {
        if (list.Count > 0)
        {
            foreach (Character a in list) Destroy(a.gameObject);
            list.Clear();
        }
    }

    internal void AddFlags()
    {
        List<Tile> noGo = FindTile.instance.TilesInRange(pullRange, FindTile.instance.Location(bossLocation[0]));
        Debug.Log(noGo.Count);
        foreach(Tile t in tileList) if (t.id == 0&& !noGo.Contains(t)) characterMoveTiles.Add(t);
        foreach (Tile t in tileList) if (t.x > 0 && t.x < arenaSizeX - 1 && t.y > 0 && t.y < arenaSizeX - 1) if (t.id == 0) possibleFlagTiles.Add(t);
        for (int i = 0; i < howManyFlags; i++)
        {
            Tile t = TileCreate(-1, 1+i);
            t.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.flagGroundColor[i * 2];
            Flag f = Instantiate(GameObjectList.instance.flag, EncounterUI.instance.arenaGameObject.transform);
            EncounterUI.instance.flags.Add(f);
            f.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.flagColor[i];
            f.id = i;
            f.home = t;
            Utility.instance.TurnOff(f.gameObject);
        }        
    }

    public Tile TileCreate(int x, int y)
    {
        Tile l = Instantiate(GameObjectList.instance.tile, EncounterUI.instance.arenaGameObject.transform);
        l.x = x;
        l.y = y;        
        l.transform.position = new Vector2(l.x, l.y);
        l.name = $"{l.x} , {l.y}";
        return l;
    }
    
    //*********************
    //Selecting Characters
    //*********************
    public void Select(List<Character> list,Selected s)
    {
        selected = s;
        selectedCharacters = list.ToList();
        GiveOrders();        
    }
    

    public List<Character> FindDPS()
    {
        List<Character> dps = new List<Character> { };
        foreach (Character c in player)
        {
            Class p = c.GetComponent<Class>();
            if (p.spec == Spec.Focused || p.spec == Spec.Explosive || p.spec == Spec.Wrathful) dps.Add(c);
        }
        return dps;
    }
    public List<Character> FindTank()
    {
        List<Character> tank = new List<Character> { };
        foreach (Character c in player)
        {
            Class p = c.GetComponent<Class>();
            if (p.spec == Spec.Stalwart) tank.Add(c);
        }
        return tank;
    }

    public List<Character> FindSupport()
    {
        List<Character> support = new List<Character> { };
        foreach (Character c in player)
        {
            Class p = c.GetComponent<Class>();
            if (p.spec == Spec.Inspiring || p.spec == Spec.Tranquil || p.spec == Spec.Redemptive) support.Add(c);
            if (p.spec == Spec.Stalwart && tank != p) support.Add(c);
        }
        return support;
    }

    //**************************************************************************************

    //*********************
    //Giving the Order
    //*********************

   
   

    public void Order(int x)
    {
        string b = (selected == Selected.DPS) ? "DPS" : (selected == Selected.TANK) ? "Tanks" : "Support";
        switch (x)
        {
            //Blue Flag
            case 1:
                orders = Orders.Blue;
                FlagRun(blueFlagTiles, null, null, null);
                break;
            //Red Flag
            case 2:
                orders = Orders.Red;
                FlagRun(redFlagTiles, null, null, null);
                break;
            //Green Flag
            case 3:
                orders = Orders.Green;
                FlagRun(greenFlagTiles, null, null, null);
                break;
            //Yellow Flag
            case 4:
                orders = Orders.Yellow;
                FlagRun(yellowFlagTiles, null, null, null);
                break;
            //Closest Flag
            case 5:
                orders = Orders.Close;
                FlagRun(blueFlagTiles, redFlagTiles, greenFlagTiles, yellowFlagTiles);
                break;
            //Stop What you are doing
            case 6:
                orders = Orders.Stop;
                foreach (Character c in selectedCharacters.ToList()) CharacterWait(c);
                break;
            //Resume
            case 7:
                orders = Orders.Resume;
                foreach (Character c in selectedCharacters.ToList()) CharacterResume(c);
                break;
            //Target someone
            case 8:
                orders = Orders.Focus;
                selectedTarget = null;
                break;
            //Everyone Stop
            case 9:
                foreach (Character c in player)
                {
                    c.waitTimer = orderWaitLength;
                    c.state = DecisionState.Wait;
                }
                orderTimer = orderTextLength;
                fullOrderText = "Orders: Everyone Stop";
                break;
            //Everyone Resume
            case 10:
                foreach (Character c in player)
                {
                    c.waitTimer = 0;
                    c.stopMoving = false;
                    c.state = DecisionState.Decision;
                }
                orderTimer = orderTextLength;
                fullOrderText = "Orders: Everyone Resume";
                break;
            //Everyone Target Something
            case 11:
                selected = Selected.All;
                orders = Orders.Focus;
                selectedTarget = null;
                break;
            //Run out of the Hazard
            case 12:
                orders = Orders.HazardMove;
                fullOrderText = (selected == Selected.All) ? $"Orders: Everyone Get Out Of Hazard" : (selected == Selected.SELECT) ? $"Orders: {UserControl.instance.selectedCharacter.characterName} Get Out Of Hazard" : $"Orders: {b} Get Out Of Hazard";
                foreach (Character p in selectedCharacters) p.Run(FindTile.instance.FindClosestTileNotInHazard(p.move.currentTile));
                break;
            //Run To the Boss
            case 13:
                orders = Orders.ToBoss;
                Run(selectedCharacters, FindTile.instance.FindTilesAroundBoss(Boss()[0].move.currentTile), (selected == Selected.All) ? $"Orders: Everyone Run To {Boss()[0].characterName}" : (selected == Selected.SELECT) ? $"Orders: {UserControl.instance.selectedCharacter.characterName} Run To {Boss()[0].characterName}" : $"Orders: {b} Run To {Boss()[0].characterName}");
                break;
            //Run AWAY from the boss
            case 14:
                orders = Orders.FromBoss;
                Run(selectedCharacters, FindTile.instance.FindBackWallTiles(), (selected == Selected.All) ? $"Orders: Everyone Run From {Boss()[0].characterName}" : (selected == Selected.SELECT) ? $"Orders: {UserControl.instance.selectedCharacter.characterName} Run From {Boss()[0].characterName}" : $"Orders: {b} Run From {Boss()[0].characterName}");
                break;
        }
        ResetOrders();
        orderCooldownTimer = Guild.instance.orderCooldown;
    }   


    //**************************************************************************************

    //*********************
    //Run
    //*********************

    public void FlagRun(List<Tile> list, List<Tile> list1, List<Tile> list2, List<Tile> list3)
    {
        List<Tile> tiles = new List<Tile> { };
        foreach (Tile t in list) if (t.OccupiedBy() == null) tiles.Add(t);
        if (list1 != null) foreach (Tile t in list1) if (t.OccupiedBy() == null) tiles.Add(t);
        if (list2 != null) foreach (Tile t in list2) if (t.OccupiedBy() == null) tiles.Add(t);
        if (list3 != null) foreach (Tile t in list3) if (t.OccupiedBy() == null) tiles.Add(t);
        orderTimer = orderTextLength;        
        string a = (orders == Orders.Red) ? "Red Flag" : (orders == Orders.Blue) ? "Blue Flag" : (orders == Orders.Green) ? "Green Flag" : (orders == Orders.Yellow) ? "Yellow Flag" : "Closest Flag";
        string b = (selected == Selected.DPS) ? "DPS" : (selected == Selected.TANK) ? "Tanks" : "Support";
        Run(selectedCharacters, tiles,(selected == Selected.All) ? $"Orders: Everyone Run to {a}":(selected == Selected.SELECT)? $"Orders: {UserControl.instance.selectedCharacter.characterName} Run to {a}" : $"Orders: {b} Run to {a}");
    }
    public void Run(List<Character> chars, List<Tile> tiles,string orderToGiveText)
    {
        fullOrderText = orderToGiveText;
        foreach (Character c in chars)
        {
            if (tiles.Count > 0)
            {
                c.Run(tiles);
                if (tiles.Count <= 0) break;
            }
            else break;
        }
    }

    //**************************************************************************************

    //*********************
    //Wait,Resume,Focus
    //*********************
    

    private void CharacterResume(Character c)
    {
        c.waitTimer = 0;
        c.stopMoving = false;
        c.state = DecisionState.Decision;
        fullOrderText = $"Orders: {c.characterName} Resume";
    }

    public void CharacterWait(Character c)
    {
        c.waitTimer = orderWaitLength;
        c.state = DecisionState.Wait;
        orderTimer = orderTextLength;
        fullOrderText = $"Orders: {c.characterName} Stop";
    }
    
    public void TargetCharacter()
    {
        orderTimer = orderTextLength;
        if (selected == Selected.All)
        {
            orderTimer = orderTextLength;
            fullOrderText = $"Orders: Everyone Target {selectedTarget.characterName}";
            foreach (Character c in FindDPS().ToList()) Target(c);
            foreach (Character c in FindTank().ToList()) Target(c);
        }
        else if (selected == Selected.SELECT)
        {
            orderTimer = orderTextLength;
            fullOrderText = $"Orders: {UserControl.instance.selectedCharacter.characterName} Target {selectedTarget.characterName}";
            Target(UserControl.instance.selectedCharacter);
        }
        else
        {
            string b = (selected == Selected.DPS) ? "DPS" : (selected == Selected.TANK) ? "Tanks" : "Support";
            fullOrderText = $"Orders: {b} Target {selectedTarget.characterName}";
            foreach (Character c in selectedCharacters.ToList()) Target(c);
        }
        ResetOrders();
    }
    public void Target(Character c)
    {
        if (player.Contains(selectedTarget)) c.primaryHealTarget = selectedTarget;
        else
        {
            c.target = selectedTarget;
            c.targetOverrideTimer = targetOverrideLength;
        }       
    }
    //**************************************************************************************

    //*********************
    //Utility.
    //*********************
    public void ResetOrders()
    {
        selected = Selected.NONE;
        orders = Orders.NONE;
        selectedCharacters.Clear();
        EncounterUI.instance.OrderToolTipOff();
    }
    private void SoftResetOrders()
    {
        EncounterUI.instance.OrderToolTipOff();
    }
    public void GiveOrders()
    {
        EncounterUI.instance.OrderToolTipOff();
        orders = Orders.NONE;
        Utility.instance.TurnOff(EncounterUI.instance.runFromHazardButton.gameObject);
        if (RunFromHazardButtonCheck()) Utility.instance.TurnOn(EncounterUI.instance.runFromHazardButton.gameObject);
    }
    private bool RunFromHazardButtonCheck()
    {
        foreach (Character p in selectedCharacters)
        {
            foreach (Hazard h in enemyHazards)
            {
                if (h.tiles.Contains(p.move.currentTile))
                {
                    return true;
                }
            }
        }
        return false;
    }   
}