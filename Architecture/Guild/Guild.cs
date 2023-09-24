using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ManageState {Recruit, View,Assign, Schedule,Upgrades}
public class Guild : MonoBehaviour, IDataPersistence
{
    public static Guild instance;
    public List<Player> roster;
    public List<Player> availableToday;
    public float guildRenown;
    public float guildCurrency;
    public float orderCooldown;
    public Player targetPlayer;
    public ManageState manageState;
    public bool seeHidden;
    public Sprite unknown;
    public bool itemToolTipHold;
    public bool guildToolTipHold;
    public int scheduleDay;
    public bool showPlayer;
    public List<ItemSO> itemList;       
    public RosterEvent nextRoster;
    public List<GuildUpgrade> availableUpgrades;
    public List<GuildUpgrade> currentUpgrades;
    public GuildUpgrade targetUpgrade;
    public ItemSO itemSOToView;
    public Item itemToView;
    private void Start()
    {
        instance = this;
        EventList.instance.MakeRosterEvent();            
    }
   
    /// <summary>
    /// TOOLTIPS
    /// </summary>
    internal void ItemTooltipOff()
    {
        Utility.instance.TurnOff(GuildUI.instance.itemTooltip.gameObject);
    }

    public void ShowItemToolTip(Item i)
    {
        Utility.instance.TurnOn(GuildUI.instance.itemTooltip.gameObject);
        GuildUI.instance.ItemTooltipInfo[0].text = i.itemName;
        GuildUI.instance.ItemTooltipInfo[1].text = i.health.ToString();
        GuildUI.instance.ItemTooltipInfo[2].text = i.mana.ToString();
        GuildUI.instance.ItemTooltipInfo[3].text = i.damage.ToString();
        GuildUI.instance.ItemTooltipInfo[4].text = i.crit.ToString();
        GuildUI.instance.ItemTooltipInfo[5].text = i.defence.ToString();
        GuildUI.instance.ItemTooltipInfo[6].text = i.spellpower.ToString();
        GuildUI.instance.ItemTooltipInfo[7].text = i.range.ToString();
        GuildUI.instance.ItemTooltipInfo[8].text = i.haste.ToString();
        GuildUI.instance.ItemTooltipInfo[9].text = i.vamp.ToString();
        GuildUI.instance.ItemTooltipInfo[10].text = i.movement.ToString();
        GuildUI.instance.ItemTooltipInfo[11].text = i.score.ToString();
        GuildUI.instance.ItemTooltipInfo[12].text = i.flavor[0];
        GuildUI.instance.ItemTooltipInfo[13].text = i.flavor[1];
        GuildUI.instance.ItemTooltipInfo[14].text = i.flavor[2];
        GuildUI.instance.ItemTooltipInfo[15].text = i.itemTypeString;
        GuildUI.instance.itemTooltipRoleImage[0].sprite = (i.id != 999) ? SpriteList.instance.roleImages[(int)i.role] : SpriteList.instance.none;
        itemToView = i;
    }
    public void ShowItemToolTip(ItemSO i)
    {
        Utility.instance.TurnOn(GuildUI.instance.itemTooltip.gameObject);
        GuildUI.instance.ItemTooltipInfo[0].text = i.itemName;
        GuildUI.instance.ItemTooltipInfo[1].text = i.health.ToString();
        GuildUI.instance.ItemTooltipInfo[2].text = i.mana.ToString();
        GuildUI.instance.ItemTooltipInfo[3].text = i.damage.ToString();
        GuildUI.instance.ItemTooltipInfo[4].text = i.crit.ToString();
        GuildUI.instance.ItemTooltipInfo[5].text = i.defence.ToString();
        GuildUI.instance.ItemTooltipInfo[6].text = i.spellpower.ToString();
        GuildUI.instance.ItemTooltipInfo[7].text = i.range.ToString();
        GuildUI.instance.ItemTooltipInfo[8].text = i.haste.ToString();
        GuildUI.instance.ItemTooltipInfo[9].text = i.vamp.ToString();
        GuildUI.instance.ItemTooltipInfo[10].text = i.movement.ToString();
        GuildUI.instance.ItemTooltipInfo[11].text = i.score.ToString();
        GuildUI.instance.ItemTooltipInfo[12].text = i.flavor[0];
        GuildUI.instance.ItemTooltipInfo[13].text = i.flavor[1];
        GuildUI.instance.ItemTooltipInfo[14].text = i.flavor[2];
        GuildUI.instance.ItemTooltipInfo[15].text = i.itemTypeString;
        GuildUI.instance.itemTooltipRoleImage[0].sprite = (i.id != 999)?SpriteList.instance.roleImages[(int)i.role] : SpriteList.instance.none;
        itemSOToView = i;
    }

    internal void ItemTooltipOn(int id)
    {
        Item i = null;
        if (id == 0) i = targetPlayer.currentClass.head;
        else if (id == 1) i = targetPlayer.currentClass.chest;
        else if (id == 2) i = targetPlayer.currentClass.legs;
        else if (id == 3) i = targetPlayer.currentClass.feet;
        else if (id == 4) i = targetPlayer.currentClass.weapon;
        else if (id == 5)
        {
            if (targetPlayer.currentClass.weapon.hands == Hands.Two) i = targetPlayer.currentClass.weapon;
            i = targetPlayer.currentClass.offHand;
        }
        else if (id == 6) i = targetPlayer.currentClass.trinket;
        ShowItemToolTip(i);
    }
    internal void DropItemTooltipOn(int id)
    {
        ShowItemToolTip(itemList[id]);
    }
    internal void GuildTooltipOff()
    {
        Utility.instance.TurnOff(GuildUI.instance.guildTooltip.gameObject);
    }

    internal void GuildTooltipOn(int id, Vector2 position)
    {
        if (id == 1) 
        {
            if (targetPlayer.traits.Count > 1) GuildTooltipOn(targetPlayer.traits[0].flavor,position);
            else GuildTooltipOn(new List<string> { "Player has no traits" }, position);
        }
        else if (id == 2) GuildTooltipOn(targetPlayer.traits[1].flavor, position);
        else if (id == 3) GuildTooltipOn(targetPlayer.traits[2].flavor, position);
    }
    internal void GuildTooltipOn(List<string> flavor, Vector2 position)
    {
        Utility.instance.TurnOn(GuildUI.instance.guildTooltip.gameObject);
        GuildUI.instance.guildTooltip.transform.position =new Vector2(position.x+200,position.y);
        foreach (TMP_Text t in GuildUI.instance.guildTooltipInfo) t.text = "";
        for (int i = 0; i < flavor.Count; i++) GuildUI.instance.guildTooltipInfo[i].text = flavor[i];
    }
    /// <summary>
    /// BOSS INFO
    /// </summary>
    /// <param</param>
    public void BossInfo(int x)
    {
        itemList.Clear();
        if (!GuildUI.instance.raidInfoBox.activeSelf ) OpenBossInfo();
        GuildUI.instance.raidName.text = nextRoster.roster.dungeon.dungeonName;
        GuildUI.instance.bossInfoText[2].text = $"{nextRoster.roster.dungeon.encountersCompleted}/{nextRoster.roster.dungeon.encounter.Count}";
        GuildUI.instance.bossInfoText[3].text = $"{nextRoster.roster.roster.Count}/{nextRoster.roster.dungeon.maxPlayers}";
        GuildUI.instance.bossInfoText[4].text = nextRoster.roster.dungeon.dungeonCompleted > 0 ? nextRoster.roster.dungeon.dungeonCompleted.ToString() : "";
        GuildUI.instance.bossInfoText[5].text = nextRoster.roster.dungeon.recommendedGearScore.ToString();
        GuildUI.instance.bossAbilityImages[4].sprite = nextRoster.roster.dungeon.dungeonCompleted > 0 ? GuildUI.instance.roleSprites[4] : GuildUI.instance.roleSprites[5];
        if (x == 99)
        {
            nextRoster.roster.dungeon.description[0]= nextRoster.roster.dungeon.description[0].Replace("\\n", "\n");
            GuildUI.instance.bossInfoText[0].text = nextRoster.roster.dungeon.description[0];
            GuildUI.instance.bossInfoText[1].text = "";
            GuildUI.instance.bossInfoText[10].text = "Select a boss to see their abilities";
            foreach (Encounter e in nextRoster.roster.dungeon.encounter) foreach (ItemSO i in e.possibleDrops) if (!itemList.Contains(i)) itemList.Add(i);
            Utility.instance.TurnOff(GuildUI.instance.abilities);
        }
        else
        {
            Boss b = (Boss)nextRoster.roster.dungeon.encounter[x].bossSummon[0];
            Utility.instance.TurnOn(GuildUI.instance.abilities);
            b.flavor =b.flavor.Replace("\\n", "\n");
            GuildUI.instance.bossInfoText[0].text = b.flavor;
            GuildUI.instance.bossInfoText[1].text = b.characterName;
            GuildUI.instance.bossInfoText[10].text = "";
            foreach(Image i in GuildUI.instance.bossAbilityImages) Utility.instance.TurnOff(i.gameObject);
            Utility.instance.TurnOn(GuildUI.instance.bossAbilityImages[4].gameObject);
            GuildUI.instance.bossInfoText[6].text =   "";
            GuildUI.instance.bossInfoText[7].text =   "";
            GuildUI.instance.bossInfoText[8].text =   "";
            GuildUI.instance.bossInfoText[9].text = "";
            List < Ability > list = b.AbilityListReturn();
            if(list!= null)
            {
                List<string> abilityNames= new List<string> { };
                List<Sprite> abilityPics = new List<Sprite> { };
                for (int i = 0; i < list.Count; i++)
                {
                    abilityNames.Add(list[i].abilityName);
                    abilityPics.Add(list[i].pic);
                    if (list[i].abilityNameTwo != "")
                    {
                        abilityNames.Add(list[i].abilityNameTwo);
                        abilityPics.Add(list[i].picTwo);
                    }
                }                    
                for (int i = 0; i < abilityNames.Count; i++)
                {
                    Utility.instance.TurnOn(GuildUI.instance.bossAbilityImages[i].gameObject);
                    GuildUI.instance.bossAbilityImages[i].sprite = abilityPics[i];
                    GuildUI.instance.bossInfoText[i + 6].text = abilityNames[i];
                }
                foreach (ItemSO i in nextRoster.roster.dungeon.encounter[x].possibleDrops) if (!itemList.Contains(i)) itemList.Add(i);
            }
            else
            {
                GuildUI.instance.bossInfoText[10].text = "No abilities to speak of. Have At!";
            }
        }           
        foreach (Image i in GuildUI.instance.bossDropSprite) Utility.instance.TurnOff(i.gameObject);
        for (int i = 0; i < itemList.Count; i++)
        {
            Utility.instance.TurnOn(GuildUI.instance.bossDropSprite[i].gameObject);
            GuildUI.instance.bossDropSprite[i].GetComponent<ItemSprite>().pic.sprite = itemList[i].pic;
        }
    }
    public void OpenBossInfo()
    {
        Utility.instance.TurnOn(GuildUI.instance.raidInfoBox);
        Utility.instance.TurnOn(GuildUI.instance.bossInfoButton[0].gameObject);
        GuildUI.instance.bossInfoButton[0].GetComponent<ItemSprite>().button.GetComponent<Image>().sprite = nextRoster.roster.dungeon.dungeonPic;
        for (int i = 1; i < GuildUI.instance.bossInfoButton.Count; i++) Utility.instance.TurnOff(GuildUI.instance.bossInfoButton[i].gameObject);
        for (int i = 1; i < nextRoster.roster.dungeon.encounter.Count + 1; i++)
        {
            Utility.instance.TurnOn(GuildUI.instance.bossInfoButton[i].gameObject);
            GuildUI.instance.bossInfoButton[i].GetComponent<ItemSprite>().button.GetComponent<Image>().sprite = nextRoster.roster.dungeon.encounter[i - 1].bossSummon[0].GetComponent<SpriteRenderer>().sprite;
        }
    }

    /// <summary>
    /// MANAGE
    /// </summary>

    public void Manage()
    {
        foreach (TMP_Text t in GuildUI.instance.playerButtonNames) t.fontSize = 24;
        GuildUI.instance.raidInfo[0].text = "";
        GuildUI.instance.recruitViewInstruction.text = "";
        Utility.instance.TurnOff(GuildUI.instance.upgradeInformationObject);
        Utility.instance.TurnOff(GuildUI.instance.raidInformationObject);
        Utility.instance.TurnOff(GuildUI.instance.upgradeInformationObject);
        Utility.instance.TurnOff(GuildUI.instance.calendar);
        Utility.instance.TurnOff(GuildUI.instance.playerInformationObject);
        Utility.instance.TurnOff(GuildUI.instance.characterInformationObject);
        Utility.instance.TurnOff(GuildUI.instance.scheduleDropdown.gameObject);
        //Utility.instance.TurnOff(GuildUI.instance.raidDropdown.gameObject);
        Utility.instance.TurnOff(GuildUI.instance.taskObject);
        Utility.instance.TurnOff(GuildUI.instance.characterButton[0].gameObject);
        //GET NAMES FOR BUTTONS
        List<string> rec = new List<string> { "View Roster", "Assign Tasks", "Schedule Raid", "Guild Upgrades" };
        List<string> vie = new List<string> { "Recruit", "Assign Tasks", "Schedule Raid", "Guild Upgrades" };
        List<string> ass = new List<string> { "Recruit", "View Roster", "Schedule Raid", "Guild Upgrades" };
        List<string> sched = new List<string> { "Recruit", "View Roster", "Assign Tasks", "Guild Upgrades" };
        List<string> up = new List<string> { "Recruit", "View Roster", "Assign Tasks", "Schedule Raid" };
        List<string> cod =roster.Count>0? new List<string> { "View Roster", "Assign Tasks", "Schedule Raid", "Guild Upgrades" } :new List<string> { "Recruit", "Assign Tasks", "Schedule Raid", "Guild Upgrades" };
        List<string> buttonNameList = manageState == ManageState.Recruit ? rec : manageState == ManageState.View ? vie : manageState == ManageState.Assign ? ass : manageState == ManageState.Schedule ? sched : manageState == ManageState.Upgrades ? up : cod;
        for (int i = 0; i < buttonNameList.Count; i++) GuildUI.instance.guildButton[i].GetComponentInChildren<TMP_Text>().text = buttonNameList[i];
        //Generic Stuff
        SortId(CharacterList.instance.freeAgents);
        SortId(roster);
        GuildInfoShow();
        //Manage based on what state
        if (manageState == ManageState.Recruit || manageState == ManageState.View)
        {
            Utility.instance.TurnOff(GuildUI.instance.raidInfoBox);
            Utility.instance.TurnOn(GuildUI.instance.characterButton[0].gameObject);
            foreach (GameObject g in GuildUI.instance.backgroundObject) Utility.instance.TurnOn(g);
            List<Player> list = manageState == ManageState.Recruit ? CharacterList.instance.freeAgents : roster;
            ButtonStuff(list);
            for (int i = 0; i < list.Count; i++) GuildUI.instance.playerButtonCost[i].text = guildCurrency >= Mathf.Floor(list[i].currentSkill) ? $"<color={Utility.instance.TextColor(SpriteList.instance.darkGreen)}>{Mathf.Floor(list[i].currentSkill)}</color>" : $"<color={Utility.instance.TextColor(SpriteList.instance.red)}>{Mathf.Floor(list[i].currentSkill)}</color>";
            for (int i = 0; i < list.Count; i++) GuildUI.instance.playerButtonClasses[i].color = Utility.instance.ClassColor(list[i].currentClass);
            if (targetPlayer == null)
            {
                Utility.instance.TurnOff(GuildUI.instance.playerBox);
                GuildUI.instance.recruitViewInstruction.text = "Please select a Character to view";
            }
            else
            {
                GuildUI.instance.recruitViewInstruction.text = "";
                Utility.instance.TurnOn(GuildUI.instance.playerBox);
                DisplayPlayerInfo();
                DisplayCharacterInfo();
            }
        }
        else if (manageState == ManageState.Assign)
        {
            Utility.instance.TurnOff(GuildUI.instance.raidInfoBox);
            foreach (GameObject g in GuildUI.instance.backgroundObject) Utility.instance.TurnOn(g);
            List<Player> list = manageState == ManageState.Recruit ? CharacterList.instance.freeAgents : roster;
            ButtonStuff(list);
            for (int i = 0; i < list.Count; i++) GuildUI.instance.playerButtonCost[i].text = roster[i].task.ToString();
            for (int i = 0; i < list.Count; i++) GuildUI.instance.playerButtonClasses[i].color = Utility.instance.ClassColor(list[i].currentClass);
            if (targetPlayer == null) Utility.instance.TurnOff(GuildUI.instance.playerBox);
            else
            {
                Utility.instance.TurnOn(GuildUI.instance.playerBox);
                DisplayPlayerInfo();
                Utility.instance.TurnOn(GuildUI.instance.taskObject);
            }
        }    
        else if (manageState == ManageState.Schedule)
        {
            availableToday.Clear();
            Utility.instance.TurnOn(GuildUI.instance.scheduleDropdown.gameObject);
            //Utility.instance.TurnOn(GuildUI.instance.raidDropdown.gameObject);
            foreach (GameObject g in GuildUI.instance.backgroundObject) Utility.instance.TurnOn(g);
            if (nextRoster != null)
            {
                foreach (Player p in roster) if (p.availableDays.Contains(scheduleDay)) availableToday.Add(p);
                ButtonStuff(availableToday);
                for (int i = 0; i < availableToday.Count; i++) GuildUI.instance.playerButtonCost[i].text = nextRoster.roster.roster.Contains(availableToday[i].currentClass) ? $"<color={Utility.instance.TextColor(SpriteList.instance.darkGreen)}>Yes</color>" : $"<color={Utility.instance.TextColor(SpriteList.instance.red)}>No</color>";
                for (int i = 0; i < availableToday.Count; i++) GuildUI.instance.playerButtonClasses[i].color = Utility.instance.ClassColor(availableToday[i].currentClass);
            }
            else foreach (Button b in GuildUI.instance.playerButton) Utility.instance.TurnOff(b.gameObject);
            Raid();            
        }
        else if (manageState == ManageState.Upgrades)
        {
            Utility.instance.TurnOff(GuildUI.instance.raidInfoBox);
            Utility.instance.TurnOn(GuildUI.instance.upgradeInformationObject);            
            foreach (GameObject g in GuildUI.instance.backgroundObject) Utility.instance.TurnOn(g);
            foreach (Button b in GuildUI.instance.playerButton) Utility.instance.TurnOff(b.gameObject);
            foreach (Image i in GuildUI.instance.guildUpgradeImages) Utility.instance.TurnOff(i.gameObject);
            //Upgrade Buttons
            foreach (TMP_Text t in GuildUI.instance.playerButtonNames)t.fontSize = 20;
            for (int i = 0; i < availableUpgrades.Count; i++)
            {
                Utility.instance.TurnOn(GuildUI.instance.playerButton[i].gameObject);                
                GuildUI.instance.playerButtonNames[i].text = availableUpgrades[i].upgradeName;
                GuildUI.instance.playerButtonClasses[i].text = UpgradeAvailable(availableUpgrades[i]) ? "Available" : "Unavailable";
                GuildUI.instance.playerButtonClasses[i].color = UpgradeAvailable(availableUpgrades[i]) ? SpriteList.instance.today : SpriteList.instance.behind;
            }
            for (int i = 0; i < availableUpgrades.Count; i++) GuildUI.instance.playerButtonCost[i].text = UpgradeAvailable(availableUpgrades[i]) ? $"<color={Utility.instance.TextColor(SpriteList.instance.darkGreen)}>{availableUpgrades[i].cost}</color>" : $"<color={Utility.instance.TextColor(SpriteList.instance.red)}>{availableUpgrades[i].cost}</color>";
            Utility.instance.TurnOn(GuildUI.instance.playerBox);
            DisplayUpgrade();
        }
    }    

    private void DisplayUpgrade()
    {
        Utility.instance.TurnOn(GuildUI.instance.playerBox);
        Utility.instance.TurnOn(GuildUI.instance.upgradeInformationObject);
        //Right Side
        for (int i = 0; i < currentUpgrades.Count; i++)
        {
            Utility.instance.TurnOn(GuildUI.instance.guildUpgradeImages[i].gameObject);
            GuildUI.instance.guildUpgradeImages[i].GetComponent<ItemSprite>().flavor[0].text = currentUpgrades[i].upgradeName;
        }
        //Left Side
        if(targetUpgrade != null)
        {
            Utility.instance.TurnOn(GuildUI.instance.upgradeLeftSide);
            GuildUI.instance.upgradeInstruction.text = "";
            GuildUI.instance.upgradeLeftText[0].text = targetUpgrade.upgradeName;
            targetUpgrade.description = targetUpgrade.description.Replace("\\n", "\n");
            GuildUI.instance.upgradeLeftText[1].text = targetUpgrade.description;
            GuildUI.instance.upgradeLeftText[2].text = (guildRenown >= targetUpgrade.renownRequirement) ? $"<color={Utility.instance.TextColor(SpriteList.instance.green)}>{targetUpgrade.renownRequirement} Renown</color>" : $"<color={Utility.instance.TextColor(SpriteList.instance.red)}>{targetUpgrade.renownRequirement} Renown</color>";
            for (int i = 0; i < targetUpgrade.preReq.Count; i++) GuildUI.instance.upgradeLeftText[2].text += (currentUpgrades.Contains(targetUpgrade.preReq[i])) ? $", <color={Utility.instance.TextColor(SpriteList.instance.green)}>{targetUpgrade.preReq[i].upgradeName}</color>" : $", <color={Utility.instance.TextColor(SpriteList.instance.red)}>{targetUpgrade.preReq[i].upgradeName}</color>";
            GuildUI.instance.upgradeLeftText[3].text = UpgradeAvailable(targetUpgrade) ? $"<color={Utility.instance.TextColor(SpriteList.instance.darkGreen)}>{targetUpgrade.cost} Currency</color>" : $"<color={Utility.instance.TextColor(SpriteList.instance.red)}>{targetUpgrade.cost} Currency</color>";
        }
        else
        {
            Utility.instance.TurnOff(GuildUI.instance.upgradeLeftSide);
            GuildUI.instance.upgradeInstruction.text = "Please select an Upgrade";
        }
    }
    
    public void RaidButtonPush(int x)
    {
        List<Dungeon> list = new List<Dungeon> { };
        foreach (Dungeon d in DungeonManager.instance.pve) if (DungeonManager.instance.expansion == d.expansion) list.Add(d);
        nextRoster.roster.dungeon = list[x];
        DisplayRaids();
    }

    private void GetTheRightDungeonsForDisplay()
    {
        List<Dungeon> list = new List<Dungeon> { };
        foreach(Image i in GuildUI.instance.raidSelectionButtons) Utility.instance.TurnOff(i.gameObject);
        foreach (Dungeon d in DungeonManager.instance.pve)
        {
            if (DungeonManager.instance.expansion == d.expansion) list.Add(d);
            for (int i = 0; i < list.Count; i++)
            {
                Utility.instance.TurnOn(GuildUI.instance.raidSelectionButtons[i].gameObject);
                GuildUI.instance.backgroundColor[i].color = list[i].tier == 0 ? SpriteList.instance.uncommon : list[i].tier ==1?SpriteList.instance.rare: list[i].tier == 3 ? SpriteList.instance.legendary: SpriteList.instance.epic;
                GuildUI.instance.dungeonPic[i].sprite = list[i].dungeonPic;
                GuildUI.instance.dungeonName[i].text = list[i].dungeonName ;
                GuildUI.instance.dungeonBossNumber[i].text = list[i].encounter.Count.ToString() ;
                GuildUI.instance.gearScoreNumber[i].text = list[i].recommendedGearScore.ToString();
                GuildUI.instance.dungeonCompletePic[i].sprite = list[i].dungeonCompleted > 0 ? GuildUI.instance.roleSprites[4] : GuildUI.instance.roleSprites[5];
            }
        }
    }

    private void ExpansionButtonDisplay()
    {
        if(DungeonManager.instance.expansion == Expansion.Vanilla)
        {
            Utility.instance.TurnOff(GuildUI.instance.vanilla.gameObject);
            Utility.instance.TurnOff(GuildUI.instance.undead.gameObject);
            Utility.instance.TurnOff(GuildUI.instance.beyond.gameObject);
        }
        else if (DungeonManager.instance.expansion == Expansion.Undead)
        {
            Utility.instance.TurnOn(GuildUI.instance.vanilla.gameObject);
            Utility.instance.TurnOn(GuildUI.instance.undead.gameObject);
            Utility.instance.TurnOff(GuildUI.instance.beyond.gameObject);
        }
        else if (DungeonManager.instance.expansion == Expansion.Beyond)
        {
            Utility.instance.TurnOn(GuildUI.instance.vanilla.gameObject);
            Utility.instance.TurnOn(GuildUI.instance.undead.gameObject);
            Utility.instance.TurnOn(GuildUI.instance.beyond.gameObject);
        }
    }

    public void RaidDisplay()
    {
        Utility.instance.TurnOff(GuildUI.instance.raidSelect);
        Utility.instance.TurnOn(GuildUI.instance.raidDisplay);
    }
    public void RaidSelect()
    {
        ExpansionButtonDisplay();
        GetTheRightDungeonsForDisplay();
        Utility.instance.TurnOn(GuildUI.instance.raidSelect);
        Utility.instance.TurnOff(GuildUI.instance.raidDisplay);
    }
    public void RaidSchedule()
    {
        Utility.instance.TurnOff(GuildUI.instance.raidSelect);
        Utility.instance.TurnOff(GuildUI.instance.raidDisplay);
    }
    private void Raid()
    {
        Utility.instance.TurnOn(GuildUI.instance.playerBox);
        Utility.instance.TurnOn(GuildUI.instance.raidInformationObject);
        if (nextRoster != null)
        {
            if (nextRoster.roster.dungeon != null) ScheduleRaids();
            else RaidSelect();
        }
        else ScheduleRaids();
    }
    public void DisplayRaids()
    {
        OpenBossInfo();
        RaidDisplay();
        BossInfo(99);
    }

    private void ScheduleRaids()
    {
        RaidSchedule();
        if (nextRoster != null)
        {
            ////////////////
            //RIGHT SIDE
            ////////////////
            GuildUI.instance.guildInfo[2].text = nextRoster.roster.dungeon.dungeonName;
            //Load Player Info
            for (int i = 0; i < 7; i++)
            {
                if (nextRoster.roster.roster.Count > i)
                {
                    GuildUI.instance.raidInfo[i * 2 + 2].text = nextRoster.roster.roster[i].characterName;
                    GuildUI.instance.raidInfo[i * 2 + 3].text = Utility.instance.Class(nextRoster.roster.roster[i]);
                }
                else
                {
                    GuildUI.instance.raidInfo[i * 2 + 2].text = "";
                    GuildUI.instance.raidInfo[i * 2 + 3].text = "";
                }
            }
            GuildUI.instance.raidInfo[16].text = $"{nextRoster.roster.dungeon.encountersCompleted}/{nextRoster.roster.dungeon.encounter.Count}";
            GuildUI.instance.raidInfo[17].text = $"{nextRoster.roster.roster.Count}/{nextRoster.roster.dungeon.maxPlayers}";
            GuildUI.instance.raidInfo[18].text = nextRoster.roster.dungeon.dungeonCompleted > 0 ? nextRoster.roster.dungeon.dungeonCompleted.ToString() : "";
            GuildUI.instance.raidInfo[19].text = nextRoster.roster.dungeon.recommendedGearScore.ToString();
            GuildUI.instance.raidRoleImages[7].sprite = nextRoster.roster.dungeon.dungeonCompleted > 0 ? GuildUI.instance.roleSprites[4] : GuildUI.instance.roleSprites[5];
            //Load Player Role Pic
            for (int i = 0; i < 7; i++) Utility.instance.TurnOff(GuildUI.instance.raidRoleImages[i].gameObject);
            for (int i = 0; i < 7; i++)
            {
                if (nextRoster.roster.roster.Count > i)
                {
                    Utility.instance.TurnOn(GuildUI.instance.raidRoleImages[i].gameObject);
                    GuildUI.instance.raidRoleImages[i].sprite = Utility.instance.ReturnSpec(nextRoster.roster.roster[i]) == Spec.Stalwart ? GuildUI.instance.roleSprites[0] : (Utility.instance.ReturnSpec(nextRoster.roster.roster[i]) == Spec.Redemptive || Utility.instance.ReturnSpec(nextRoster.roster.roster[i]) == Spec.Tranquil) ? GuildUI.instance.roleSprites[1] : Utility.instance.ReturnSpec(nextRoster.roster.roster[i]) == Spec.Inspiring ? GuildUI.instance.roleSprites[3] : GuildUI.instance.roleSprites[2];
                }
            }
            ////////////////
            //LEFT SIDE
            ////////////////
            if (targetPlayer != null)
            {
                if (showPlayer) DisplayPlayerInfo();
                else DisplayCharacterInfo();
                Utility.instance.TurnOn(GuildUI.instance.raidButtons[0].gameObject);
                Utility.instance.TurnOn(GuildUI.instance.raidButtons[1].gameObject);
                GuildUI.instance.raidButtons[0].GetComponentInChildren<TMP_Text>().text = showPlayer ? "Character" : "Player";
                GuildUI.instance.raidButtons[1].GetComponentInChildren<TMP_Text>().text = nextRoster.roster.roster.Contains(targetPlayer.currentClass) ? "Remove from Raid" : "Add To Raid";
                GuildUI.instance.raidInfo[0].text = "";
            }
            else
            {
                Utility.instance.TurnOff(GuildUI.instance.raidButtons[0].gameObject);
                Utility.instance.TurnOff(GuildUI.instance.raidButtons[1].gameObject);
                GuildUI.instance.raidInfo[0].text = availableToday.Count == 0 ? "No One is Available Today\n\nSelect another day or Recruit more players" : "Please select a player to bring to the dungeon";
            }
        }
        else
        {
            GuildUI.instance.raidInfo[0].text = "All of your raids have been schduled for the week";
        }
    }
    private void DisplayCharacterInfo()
    {
        Utility.instance.TurnOn(GuildUI.instance.characterInformationObject);
        if (manageState == ManageState.Recruit || manageState == ManageState.View) GuildUI.instance.characterInformationObject.transform.localPosition = new Vector3(0, 0, 0);
        else GuildUI.instance.characterInformationObject.transform.localPosition = new Vector3(-570, 0, 0);
        GuildUI.instance.characterInfo[0].text = Utility.instance.Class(targetPlayer.currentClass);
        GuildUI.instance.characterInfo[1].text = Math.Floor(targetPlayer.currentSkill).ToString();
        GuildUI.instance.characterInfo[2].text = Utility.instance.SpecNameShort(targetPlayer.currentClass);
        GuildUI.instance.characterInfo[3].text = seeHidden || manageState != ManageState.Recruit ? $"{targetPlayer.currentClass.maxHealth.value}/{targetPlayer.currentClass.maxHealth.value}" : "???";
        GuildUI.instance.characterInfo[4].text = seeHidden || manageState != ManageState.Recruit ? $"{targetPlayer.currentClass.maxMana.value}/{targetPlayer.currentClass.maxMana.value}" : "???";
        GuildUI.instance.characterInfo[5].text = seeHidden || manageState != ManageState.Recruit ? targetPlayer.currentClass.damage.value.ToString() : "?";
        GuildUI.instance.characterInfo[6].text = seeHidden || manageState != ManageState.Recruit ? targetPlayer.currentClass.crit.value.ToString() : "?";
        GuildUI.instance.characterInfo[7].text = seeHidden || manageState != ManageState.Recruit ? targetPlayer.currentClass.defence.value.ToString() : "?";
        GuildUI.instance.characterInfo[8].text = seeHidden || manageState != ManageState.Recruit ? targetPlayer.currentClass.spellpower.value.ToString() : "?";
        int gs = targetPlayer.currentClass.head.score + targetPlayer.currentClass.chest.score + targetPlayer.currentClass.legs.score + targetPlayer.currentClass.feet.score + targetPlayer.currentClass.trinket.score + targetPlayer.currentClass.weapon.score + targetPlayer.currentClass.offHand.score;
        GuildUI.instance.characterInfo[9].text = seeHidden || manageState != ManageState.Recruit ? gs.ToString() : "???";
        GuildUI.instance.paperDollImages[0].sprite = !seeHidden && manageState == ManageState.Recruit ? unknown : targetPlayer.currentClass.head.pic;
        GuildUI.instance.paperDollImages[1].sprite = !seeHidden && manageState == ManageState.Recruit ? unknown : targetPlayer.currentClass.chest.pic;
        GuildUI.instance.paperDollImages[2].sprite = !seeHidden && manageState == ManageState.Recruit ? unknown : targetPlayer.currentClass.legs.pic;
        GuildUI.instance.paperDollImages[3].sprite = !seeHidden && manageState == ManageState.Recruit ? unknown : targetPlayer.currentClass.feet.pic;
        GuildUI.instance.paperDollImages[4].sprite = !seeHidden && manageState == ManageState.Recruit ? unknown : targetPlayer.currentClass.weapon.pic;
        GuildUI.instance.paperDollImages[5].sprite = !seeHidden && manageState == ManageState.Recruit ? unknown : targetPlayer.currentClass.weapon.hands == Hands.Two ? targetPlayer.currentClass.weapon.pic : targetPlayer.currentClass.offHand.pic;
        GuildUI.instance.paperDollImages[6].sprite = !seeHidden && manageState == ManageState.Recruit ? unknown : targetPlayer.currentClass.trinket.pic;
    }   

    private void DisplayPlayerInfo()
    {
        Utility.instance.TurnOn(GuildUI.instance.playerInformationObject);        
        GuildUI.instance.characterButton[0].GetComponentInChildren<TMP_Text>().text = manageState == ManageState.View ? "Fire" : "Recruit";
        if (targetPlayer.altClass != null) Utility.instance.TurnOn(GuildUI.instance.characterButton[1].gameObject);
        else Utility.instance.TurnOff(GuildUI.instance.characterButton[1].gameObject);
        GuildUI.instance.playerInfo[0].text = targetPlayer.playerName;
        List<string> d = new List<string> { "", "M", "Tu", "W", "Th", "F", "Sat", "Sun" };
        string days = "";
        for (int i = 0; i < targetPlayer.availableDays.Count; i++)
        {
            if (i == targetPlayer.availableDays.Count - 1) days += d[targetPlayer.availableDays[i]];
            else days += d[targetPlayer.availableDays[i]] + ", ";
        }
        GuildUI.instance.playerInfo[1].text = $"Available: {days}";
        GuildUI.instance.playerInfo[2].text = "Available Today: " + (targetPlayer.availableDays.Contains(TimeManagement.instance.day) ? "Yes" : "No");
        GuildUI.instance.playerInfo[3].text = manageState == ManageState.Recruit ? $"Cost: {Mathf.Floor(targetPlayer.currentSkill)}" : "";
        string addA = (seeHidden || manageState != ManageState.Recruit) ? Math.Floor(targetPlayer.ambition).ToString() : "???";
        string a = "Ambition: "+addA ;
        string addB = seeHidden || manageState != ManageState.Recruit ? Math.Floor(targetPlayer.guildLoyalty).ToString() : "???";
        string b = "Guild Loyalty: " + addB;
        string addC = seeHidden || manageState != ManageState.Recruit ? Math.Floor(targetPlayer.enchantmentShards).ToString() : "???";
        string c = "Shards: "+addC;        
        GuildUI.instance.playerInfo[4].text = a;
        GuildUI.instance.playerInfo[5].text = b;
        GuildUI.instance.playerInfo[6].text = c;
        GuildUI.instance.playerInfo[7].text = !seeHidden && manageState == ManageState.Recruit ? "???" : targetPlayer.traits.Count > 0 ? targetPlayer.traits[0].traitName : "None";
        GuildUI.instance.playerInfo[8].text = !seeHidden && manageState == ManageState.Recruit ? "???" : targetPlayer.traits.Count > 1 ? targetPlayer.traits[1].traitName : "";
        GuildUI.instance.playerInfo[9].text = !seeHidden && manageState == ManageState.Recruit ? "???" : targetPlayer.traits.Count > 2 ? targetPlayer.traits[2].traitName : "";
        if (roster.Contains(targetPlayer)) GuildUI.instance.playerInfo[10].text = targetPlayer.task ==Task.None?"Task: None": $"Task: {targetPlayer.task} - {targetPlayer.timeOnTask} days";
        else GuildUI.instance.playerInfo[10].text = "";
    }

    public void GuildInfoShow()
    {
        GuildUI.instance.guildInfo[0].text = manageState == ManageState.Recruit ? "Recruit" : manageState == ManageState.View ? "View Roster" : manageState == ManageState.Schedule ? "Schedule Raid" : "Assign Tasks";
        GuildUI.instance.guildInfo[1].text = manageState == ManageState.Schedule ? "" : $"Guild Renown: {guildRenown}";
        GuildUI.instance.guildInfo[2].text = manageState == ManageState.Schedule ? "" : $"Guild Currency: {guildCurrency}";
    }

    /// <summary>
    /// BUTTONS
    /// </summary>  

    public void AddRemoveFromRaid()
    {
        if (nextRoster.roster.roster.Contains(targetPlayer.currentClass)) nextRoster.roster.roster.Remove(targetPlayer.currentClass);
        else nextRoster.roster.roster.Add(targetPlayer.currentClass);
        Manage();
    }
    public void Assign(int id)
    {
        targetPlayer.task = (Task)id;
        Manage();
    }

    public void Button1()
    {
        if (manageState == ManageState.Recruit) manageState = ManageState.View;
        else if (manageState == ManageState.View) manageState = ManageState.Recruit;
        else if (manageState == ManageState.Assign) manageState = ManageState.Recruit;
        else if (manageState == ManageState.Schedule) manageState = ManageState.Recruit;
        else if (manageState == ManageState.Upgrades) manageState = ManageState.Recruit;
        targetPlayer = null;
        Manage();
    }
    public void Button2()
    {
        if (manageState == ManageState.Recruit) manageState = ManageState.Assign;
        else if (manageState == ManageState.View) manageState = ManageState.Assign;
        else if (manageState == ManageState.Assign) manageState = ManageState.View;
        else if (manageState == ManageState.Schedule) manageState = ManageState.View;
        else if (manageState == ManageState.Upgrades) manageState = ManageState.View;
        targetPlayer = null;
        Manage();
    }
    public void Button3()
    {
        if (manageState == ManageState.Recruit) ScheduleRaid();
        else if (manageState == ManageState.View) ScheduleRaid();
        else if (manageState == ManageState.Assign) ScheduleRaid();
        else if (manageState == ManageState.Schedule) manageState = ManageState.Assign;
        else if (manageState == ManageState.Upgrades) manageState = ManageState.Assign;
        targetPlayer = null;
        Manage();
    }
    public void Button4()
    {
        if (manageState == ManageState.Recruit) manageState = ManageState.Upgrades;
        else if (manageState == ManageState.View) manageState = ManageState.Upgrades;
        else if (manageState == ManageState.Assign) manageState = ManageState.Upgrades;
        else if (manageState == ManageState.Schedule) manageState = ManageState.Upgrades;
        else if (manageState == ManageState.Upgrades) ScheduleRaid();
        targetPlayer = null;
        Manage();
    }
    internal void ButtonPress(int id)
    {
        if (manageState == ManageState.Upgrades) targetUpgrade = availableUpgrades[id];
        else targetPlayer = manageState == ManageState.Recruit ? CharacterList.instance.freeAgents[id] : manageState == ManageState.Schedule ? availableToday[id] : roster[id];
        Manage();
    }
    public void BuyUpgrade()
    {
        if (UpgradeAvailable(targetUpgrade))
        {
            guildCurrency -= targetUpgrade.cost;
            currentUpgrades.Add(targetUpgrade);
            availableUpgrades.Remove(targetUpgrade);
            targetUpgrade = null;
            Manage();
        }
    }
    public void SelectRaid()
    {
        Utility.instance.TurnOff(GuildUI.instance.raidInfoBox);
        GuildTooltipOff();
        ItemTooltipOff();
        ScheduleRaids();
    }

    public void Confirm()
    {
        nextRoster.roster.ready = true;
        nextRoster.CurrentDayAvailable();
        if (scheduleDay > nextRoster.available[0]) nextRoster.available[0] = scheduleDay;
        else
        {
            nextRoster.available[0] = scheduleDay;
            nextRoster.available[1]++;
            if (nextRoster.available[1] > 4)
            {
                nextRoster.available[1] -= 4;
                nextRoster.available[2]++;
            }
            if (nextRoster.available[2] > 12)
            {
                nextRoster.available[2] -= 12;
                nextRoster.available[3]++;
            }
        }
        nextRoster.ExpireMatchAvailable();
        EventList.instance.events.Add(nextRoster);
        nextRoster = null;
        Manage();
    }

    public void RaidDropdown()
    {
        //nextRoster.roster.dungeon = DungeonManager.instance.pve[GuildUI.instance.raidDropdown.value];
        OpenBossInfo();
        BossInfo(99);
        Manage();
    }
    public void RecruitOrFireTarget()
    {
        if (!roster.Contains(targetPlayer))
        {
            if (guildCurrency >= Math.Floor(targetPlayer.currentSkill))
            {
                guildCurrency -= (float)Math.Floor(targetPlayer.currentSkill);
                targetPlayer.guildLoyalty = 60;
                roster.Add(targetPlayer);
                targetPlayer.transform.SetParent(GameManager.instance.guild.transform);
                CharacterList.instance.freeAgents.Remove(targetPlayer);
            }
        }
        else
        {
            targetPlayer.guildLoyalty = 5;
            roster.Remove(targetPlayer);
            targetPlayer.transform.SetParent(GameManager.instance.freeAgent.transform);
            CharacterList.instance.freeAgents.Add(targetPlayer);
            targetPlayer = null;
        }
        NullifyTarget();
    }
    public void ScheduleDropdown()
    {
        scheduleDay = GuildUI.instance.scheduleDropdown.value + 1;
        if (nextRoster != null) nextRoster.roster.roster.Clear();
        Manage();
    }
    public void ScheduleRaid()
    {
        nextRoster = null;
        foreach (RosterEvent r in EventList.instance.rosterEvents) if (!r.roster.ready) nextRoster = r;
        showPlayer = true;
        manageState = ManageState.Schedule;
        ScheduleDropdown();
    }



    /// <summary>
    /// VIEW CHARACTER
    /// </summary>
    public void ChangeSpec()
    {
        targetPlayer.currentClass.ChangeSpec();
        targetPlayer.UpdateName();
        Manage();
    }
    public void PlayerCharacterShow()
    {
        if (showPlayer) showPlayer = false;
        else showPlayer = true;
        Manage();
    }
    public void SwitchCharacter()
    {
        targetPlayer.Switch();
        targetPlayer.UpdateName();
        Manage();
    }

    /// <summary>
    /// UTILITY
    /// </summary>

    private void ButtonStuff(List<Player> list)
    {
        foreach (Button b in GuildUI.instance.playerButton) Utility.instance.TurnOff(b.gameObject);
        for (int i = 0; i<list.Count; i++)
        {
            Utility.instance.TurnOn(GuildUI.instance.playerButton[i].gameObject);
            GuildUI.instance.playerButtonNames[i].text = list[i].playerName;
            GuildUI.instance.playerButtonClasses[i].text = Utility.instance.Class(list[i].currentClass);
        }
    }
    public void NullifyTarget()
    {
        targetPlayer = null;
        Manage();
    }
    public List<Player> NotInDungeon(List<Character> inFight)
    {
        List<Player> notInFight = new List<Player> { };
        foreach (Player a in roster) if (!inFight.Contains(a.currentClass)) notInFight.Add(a);
        return notInFight;
    }
    public List<Player> NotInDungeon(List<Player> inFight)
    {
        List<Player> notInFight = new List<Player> { };
        foreach (Player a in roster) if (!inFight.Contains(a)) notInFight.Add(a);
        return notInFight;
    }
    private void SortId(List<Player> list)
    {
        Player temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].currentClass.id > list[i + 1].currentClass.id)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }
    private bool UpgradeAvailable(GuildUpgrade guildUpgrade)
    {
        if (guildUpgrade.renownRequirement > guildRenown || guildUpgrade.cost > guildCurrency) return false;
        if (guildUpgrade.preReq.Count == 0) return true;
        foreach (GuildUpgrade g in guildUpgrade.preReq) if (!currentUpgrades.Contains(g)) return false;
        return true;
    }
    public void LoadData(GameData data)
    {
        guildCurrency = data.guildCurrency;
        guildRenown = data.guildRenown;
        seeHidden = data.seeHidden;
    }
    public void SaveData(GameData data)
    {
        data.guildCurrency = guildCurrency;
        data.guildRenown =guildRenown;
        data.seeHidden = seeHidden;
    }

}