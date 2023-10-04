using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum Meters {Damage,Threat}
public class EncounterUI : MonoBehaviour
{
    public static EncounterUI instance;
    public List<Button> background;
    public List<Image> agentPic;
    public List<Image> headPic;
    public List<Image> chestPic;
    public List<Image> legsPic;
    public List<Image> feetPic;
    public List<Image> weaponPic;
    public List<Image> offHandPic;
    public List<Text>  agentNameText;
    public List<Text>  agentHealthText;
    public List<Text>  agentManaText;
    public List<TMP_Text>  agentActionText;
    public List<Slider> agentHealthBar;
    public List<Slider> agentManaBar;
    public List<Image> agentMana;
    public GameObject preFightBox;
    public Text meterText1;
    public Text meterText2;
    public List<Text> playerMeter;
    public List<Text> damageMeter;
    public List<Text> damageMeter2;
    public Button damageMeterButton;
    public Button threatMeterButton;
    public Button minimizeMaximiseButton;
    public GameObject meterBox;
    public Meters meter;
    public bool minimized;
    public List<Image> player1Buffs;
    public List<Image> player2Buffs;
    public GameObject uITooltipBox;
    public GameObject characterInfoTooltipBox;
    public GameObject characterInfoTooltipBox1;
    public GameObject targetInfoTooltipBox;
    public GameObject targetInfoTooltipBox1;
    public List<TMP_Text> uITooltipFlavor;
    public List<TMP_Text> characterInfoTooltipFlavor;
    public List<TMP_Text> characterInfoTooltipFlavor1;
    public List<TMP_Text> targetInfoTooltipFlavor;
    public List<TMP_Text> targetInfoTooltipFlavor1;
    public List<Effect> eList;
    public List<Slider> playerCastBar;
    public EffectTooltip currentEffectTooltip;
    public List<TMP_Text> flavor;
    public List<Button> choices;    
    public TMP_Text timeText;
    public GameObject playerKeys;
    public List<Image> playerButtonBackGround;
    public List<Button> playerButton;
    [HideInInspector]
    public Encounter currentEncounter;
    [HideInInspector]
    public bool select;
    public bool basicStats;
    public bool modInfo;
    public TMP_Text attemptText;
    public List<GameObject> buttonAnchor;
    public bool updateTime;
    public List<Image> timerBorder;
    public List<Image> timeBorder;
    public List<Sprite> timerPic;
    public List<Sprite> timePic;
    public Button DPSSelect;
    public Button tankSelect;
    public Button supportSelect;
    public TMP_Text selectText;
    public TMP_Text ordersText;
    public TMP_Text fullOrderText;
    public List<Slider> playerHBarUI;
    public List<Slider> enemyHBarUI;
    public List<TMP_Text> playerTextUI;
    public List<TMP_Text> enemyTextUI;
    public GameObject arenaGameObject;
    public GameObject charactersGameObject;
    [HideInInspector]
    public List<Flag> flags;
    public List<Button> flagButtons;
    public GameObject orderBox;
    public GameObject selectBox;    
    public Button runFromHazardButton;    
    public GameObject orderToolTipBox;
    public List<TMP_Text> orderToolTipFlavor;
    private void Awake()
    {
        instance = this;
    }
    public void OrderToolTipOff()
    {
        foreach (TMP_Text t in orderToolTipFlavor) t.text = "";
        Utility.instance.TurnOff(orderToolTipBox);
    }
    public void UpdateCombatUI()
    {
        currentEncounter.EncounterUpdate();
        if (UserControl.instance.control == Control.UserControl) UpdateButtons();         
        selectText.text = currentEncounter.selectText;
        ordersText.text = currentEncounter.orderText;
        if (currentEncounter.orderCooldownTimer <= 0)
        {
            if (currentEncounter.selected != Selected.NONE)
            {
                Utility.instance.TurnOff(selectBox);
                Utility.instance.TurnOn(orderBox);
            }
            else
            {
                Utility.instance.TurnOn(selectBox);
                Utility.instance.TurnOff(orderBox);
            }
        }
        else
        {
            Utility.instance.TurnOff(selectBox);
            Utility.instance.TurnOff(orderBox);
        }
        fullOrderText.text = currentEncounter.fullOrderText;
        TurnOffBackgrounds();
        if (select) UpdateUI();
        UpdateDamageMeters();
        UpdateThreatMeters();
        UpdateTooltipTime();
        UpdateSmallUI();
    }
    

    public void UpdateSmallUI()
    {
        for (int i = 0; i < currentEncounter.player.Count; i++)
        {
            playerHBarUI[i].value = (currentEncounter.player[i].ko) ? 0:currentEncounter.player[i].health / currentEncounter.player[i].maxHealth.value;
            playerTextUI[i].text = currentEncounter.player[i].characterName;
            if (currentEncounter.player[i].ko) playerTextUI[i].color = SpriteList.instance.bad;
            else playerTextUI[i].color = Color.white;
        }
        for (int i = 0; i < currentEncounter.Boss().Count; i++)
        {
            enemyHBarUI[i].value = currentEncounter.Boss()[i].health / currentEncounter.Boss()[i].maxHealth.value;
            enemyTextUI[i].text = currentEncounter.Boss()[i].characterName;
            if (currentEncounter.Boss()[i].ko) enemyTextUI[i].color = SpriteList.instance.bad;
            else enemyTextUI[i].color = Color.white;
        }
    }

    private void UpdateButtons()
    {
        for (int i = 0; i < UserControl.instance.selectedCharacter.ability.Count; i++)
        {
            if (!UserControl.instance.selectedCharacter.ability[i].passive)
            {
                int x = Convert.ToInt32(UserControl.instance.selectedCharacter.ability[i].cooldownTimer);               
                if (x<=30) timerBorder[i].sprite = timerPic[x];
                else timerBorder[i].sprite = timerPic[31];
                playerButton[i].GetComponent<Image>().fillAmount = 1 - (UserControl.instance.selectedCharacter.ability[i].cooldownTimer / UserControl.instance.selectedCharacter.ability[i].cooldownTime);
            }
        }
    }

    public void UpdateUI()
    {
        if (select)
        {
            SelectUI();
            if (UserControl.instance.selectedCharacter.target != null) TargetUI();
        }
    }

    private void UpdateTooltipTime()
    {
        if(currentEffectTooltip != null)
        {
            if (updateTime && currentEffectTooltip.flavor.Count > 3)
            {
                uITooltipFlavor[1].text = currentEffectTooltip.flavor[1];
                uITooltipFlavor[2].text = currentEffectTooltip.flavor[2];
                uITooltipFlavor[3].text = currentEffectTooltip.flavor[3];
            }
        }        
    }

    public void PlayerButtonOne() => UserControl.instance.Attack(1, DecisionState.Attack2);
    public void PlayerButtonTwo() => UserControl.instance.Attack(2, DecisionState.Attack3);
    public void PlayerButtonThree() => UserControl.instance.Attack(3, DecisionState.Attack4);
    public void PlayerButtonFour() => UserControl.instance.Attack(4, DecisionState.Attack5);
    public void PlayerButtonMouse() => UserControl.instance.Attack(0, DecisionState.Attack1);   

    private void UpdateBuffs(List<Image> buffList, Character c)
    {
        foreach (Image i in buffList)
        {
            i.sprite = SpriteList.instance.none;
            i.GetComponent<EffectTooltip>().flavor.Clear();
        }
        eList.Clear();
        if (c.buff.Count + c.debuff.Count <= 6)
        {
            foreach (Effect e in c.buff) eList.Add(e);
            foreach (Effect e in c.debuff) eList.Add(e);
        }
        else if(c.buff.Count<= 6)
        {
            foreach (Effect e in c.buff) eList.Add(e);
            for (int i = 0; i < 6-c.buff.Count; i++)
            {
                eList.Add(c.debuff[i]);
            }
        }
        else
        {
            for (int i = 0; i < 6 ; i++)
            {
                eList.Add(c.buff[i]);
            }
        }
        for (int i = 0; i < eList.Count; i++) 
        {
            buffList[i].sprite = eList[i].sprite;
            foreach (string s in eList[i].flavor) buffList[i].GetComponent<EffectTooltip>().flavor.Add(s);
        }
    }

    public void TurnOffBackgrounds()
    {
        foreach (Button b in background) Utility.instance.TurnOff(b.gameObject);        
        Utility.instance.TurnOff(characterInfoTooltipBox.gameObject);
        Utility.instance.TurnOff(characterInfoTooltipBox1.gameObject);
        Utility.instance.TurnOff(targetInfoTooltipBox.gameObject);
        Utility.instance.TurnOff(targetInfoTooltipBox1.gameObject);
    }

    public void SelectUI()
    {
        Character player = UserControl.instance.selectedCharacter;
        Character character = UserControl.instance.selectedCharacter;
        CharacterUI(player,character,0);
        if (basicStats)
        {
            Utility.instance.TurnOn(characterInfoTooltipBox);
            StatsInfo(character, characterInfoTooltipFlavor);
        }
        else if (modInfo)
        {
            Utility.instance.TurnOn(characterInfoTooltipBox1);
            ModInfo(character, characterInfoTooltipFlavor1);
        }
        UpdateBuffs(player1Buffs, character);
    }
    public void TargetUI()
    {
        Character player = UserControl.instance.selectedCharacter.target;        
        Character character = UserControl.instance.selectedCharacter.target;
        if (basicStats)
        {
            Utility.instance.TurnOn(targetInfoTooltipBox);
            StatsInfo(character, targetInfoTooltipFlavor);
        }
        else if (modInfo)
        {
            Utility.instance.TurnOn(targetInfoTooltipBox1);
            ModInfo(character, targetInfoTooltipFlavor1);
        }
        CharacterUI(player, character, 1);
        UpdateBuffs(player2Buffs, character);
    }

    public void CharacterUI(Character player, Character character,int x)
    {
        Utility.instance.TurnOn(background[x].gameObject);
        agentPic[x].sprite = (player.ko) ? SpriteList.instance.dead : character.GetComponent<SpriteRenderer>().sprite;
        headPic[x].sprite = (player.ko) ? SpriteList.instance.none : character.head.pic;
        chestPic[x].sprite = (player.ko) ? SpriteList.instance.none : character.chest.pic;
        legsPic[x].sprite = (player.ko) ? SpriteList.instance.none : character.legs.pic;
        feetPic[x].sprite = (player.ko) ? SpriteList.instance.none : character.feet.pic;
        weaponPic[x].sprite = (player.ko) ? SpriteList.instance.none : character.weapon.pic;
        offHandPic[x].sprite = (player.ko || character.offHand.itemName == "------ || ------") ? SpriteList.instance.none : character.offHand.pic;
        WeaponPicOffset(x,character.weapon);
        OffHandPicOffset(x,character.offHand);        
        agentNameText[x].text = character.characterName;
        agentHealthText[x].text = (player.ko) ? "Dead" : Convert.ToInt32(character.Health()).ToString() + "/" + character.maxHealth.value.ToString();
        agentManaText[x].text = (player.ko) ? "Dead" : character.Mana().ToString() + "/" + character.maxMana.value.ToString();
        agentHealthBar[x].value = character.Health() / character.maxHealth.value;
        agentManaBar[x].value = (character.maxMana.value <= 0) ? 0 : character.Mana() / character.maxMana.value;
        agentMana[x].color = (character.GetComponent<Beserker>()) ? SpriteList.instance.rage : (character.GetComponent<Rogue>() ? SpriteList.instance.energy : SpriteList.instance.mana);
        agentActionText[x].text = (player.ko) ? "Dead" : character.action;
        if (character.state == DecisionState.Cast)
        {
            playerCastBar[x].value = (character.actionCast - character.castTimer) / character.actionCast;
        }
        else playerCastBar[x].value = 0;        
    }

    private void OffHandPicOffset(int x, Item item)
    {
        offHandPic[x].GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 0);
        if (item.offHandType == OffHandType.Dagger)
        {
            offHandPic[x].GetComponent<Transform>().localPosition = new Vector2(16f, 0f);            
            offHandPic[x].GetComponent<Transform>().Rotate(0, 180, 30f);
        }
        else
        {
            offHandPic[x].GetComponent<Transform>().localPosition = new Vector2(16f, 0f);
            if (item.offHandType == OffHandType.Shield) offHandPic[x].GetComponent<Transform>().localScale = new Vector2(.7f, .7f);
        }
    }

    private void WeaponPicOffset(int x, Item item)
    {
        weaponPic[x].GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 0);
        if (item.hands == Hands.One)
        {
            weaponPic[x].GetComponent<Transform>().Rotate(0, 0, 30f);
            if (item.weaponType == WeaponType.Sword) weaponPic[x].GetComponent<Transform>().localPosition = new Vector2(-21.1f, 8.1f);
            if (item.weaponType == WeaponType.Dagger || item.weaponType == WeaponType.Axe || item.weaponType == WeaponType.Mace || item.weaponType == WeaponType.Wand) weaponPic[x].GetComponent<Transform>().localPosition = new Vector2(-17.8f, 1.6f);
        }
        else if (item.weaponType == WeaponType.Staff) weaponPic[x].GetComponent<Transform>().localPosition = new Vector2(-16f, 7.5f);
        else
        {
            weaponPic[x].GetComponent<Transform>().localPosition = new Vector2(11.4f, 14.6f);
            weaponPic[x].GetComponent<Transform>().Rotate(0, 0, -50.888f);
        }
    }

    public void UpdateDamageMeters()
    {
        if (meter == Meters.Damage)
        {
            meterText1.text = "Damage";
            meterText2.text = "DPS";
            List<Character> damageList = new List<Character> { };
            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.player) damageList.Add(a);
            SortDamage(damageList);
            for (int i = 0; i < damageList.Count; i++)
            {
                Class c = (Class)damageList[i];                
                playerMeter[i].color = (damageList[i].GetType() == typeof(Beserker)) ? SpriteList.instance.beserker : (damageList[i].GetType() == typeof(Druid)) ? SpriteList.instance.druid : (damageList[i].GetType() == typeof(Bard)) ? SpriteList.instance.bard : (damageList[i].GetType() == typeof(Mage)) ? SpriteList.instance.mage : (damageList[i].GetType() == typeof(Rogue)) ? SpriteList.instance.rogue : SpriteList.instance.shieldBearer;
                playerMeter[i].text = damageList[i].characterName;
                damageMeter[i].text = Mathf.Round(damageList[i].damageDone).ToString();
                damageMeter2[i].text = (!c.startTimeInFight)?"":$"{Mathf.Round(damageList[i].damageDone / c.timeInFight)}";
            }
        }
        if (minimized) minimizeMaximiseButton.GetComponentInChildren<Text>().text = "Meters";
        else minimizeMaximiseButton.GetComponentInChildren<Text>().text = "Minimize";
    }

    public void UpdateThreatMeters()
    {
        if (meter == Meters.Threat)
        {
            meterText1.text = "Threat";
            meterText2.text = "TPS";
            foreach (Text t in playerMeter) t.text = "";
            foreach (Text t in damageMeter) t.text = "";
            List<Aggro> aggro = DungeonManager.instance.currentDungeon.currentEncounter.Boss()[0].GetComponent<Boss>().aggro;
            if (aggro.Count != 0)
            {
                for (int i = 0; i < aggro.Count; i++)
                {
                    Class c = (Class)aggro[i].agent;
                    playerMeter[i].color = (aggro[i].agent.GetComponentInChildren<Beserker>()) ? SpriteList.instance.beserker : (aggro[i].agent.GetComponentInChildren<Druid>()) ? SpriteList.instance.druid : (aggro[i].agent.GetComponentInChildren<Bard>()) ? SpriteList.instance.bard : (aggro[i].agent.GetComponentInChildren<Mage>()) ? SpriteList.instance.mage : (aggro[i].agent.GetComponentInChildren<Rogue>()) ? SpriteList.instance.rogue : SpriteList.instance.shieldBearer;
                    playerMeter[i].text = aggro[i].agent.characterName;
                    damageMeter[i].text = Mathf.Round(aggro[i].aggro).ToString();
                    damageMeter2[i].text = (!c.startTimeInFight) ? "" : $"{Mathf.Round(aggro[i].aggro / c.timeInFight)}";
                }
            }
        }
    }

    public void FightBoxButton()
    {
        if(DungeonManager.instance.raidMode == RaidMode.Setup)
        {            
            Utility.instance.TurnOff(preFightBox.gameObject);            
            TurnOnFightButtons();
            DungeonManager.instance.raidMode = RaidMode.Combat;
        }
        else if (DungeonManager.instance.raidMode == RaidMode.Resolve)
        {
            Utility.instance.TurnOff(preFightBox.gameObject);
            DungeonManager.instance.raidMode = RaidMode.Off;
            EndMatch.instance.Resolve();                  
        }
    }

    private void TurnOnFightButtons()
    {
        currentEncounter.selected = Selected.NONE;
        currentEncounter.orders = Orders.NONE;
        foreach (Slider b in playerHBarUI) Utility.instance.TurnOff(b.gameObject);
        foreach (Slider b in enemyHBarUI) Utility.instance.TurnOff(b.gameObject);
        for (int i = 0; i < currentEncounter.player.Count; i++) Utility.instance.TurnOn(playerHBarUI[i].gameObject);
        for (int i = 0; i < currentEncounter.Boss().Count; i++) Utility.instance.TurnOn(enemyHBarUI[i].gameObject);
        if (currentEncounter.howManyFlags > 0)
        {
            if (currentEncounter.FindDPS().Count > 0) Utility.instance.TurnOn(DPSSelect.gameObject);
            else Utility.instance.TurnOff(DPSSelect.gameObject);
            if (currentEncounter.FindSupport().Count > 0) Utility.instance.TurnOn(supportSelect.gameObject);
            else Utility.instance.TurnOff(supportSelect.gameObject);
            if (currentEncounter.tank != null || currentEncounter.offTank!=null) Utility.instance.TurnOn(tankSelect.gameObject);
            else Utility.instance.TurnOff(tankSelect.gameObject);
            bool blue = false;
            bool red = false;
            bool green = false;
            bool yellow = false;
            if (currentEncounter.blueFlagTiles.Count > 0) blue = true;
            if (currentEncounter.redFlagTiles.Count > 0) red = true;
            if (currentEncounter.greenFlagTiles.Count > 0) green = true;
            if (currentEncounter.yellowFlagTiles.Count > 0) yellow = true;
            if (blue || red || green | yellow)
            {
                Utility.instance.TurnOn(flagButtons[4].gameObject);
                if(blue) Utility.instance.TurnOn(flagButtons[0].gameObject);
                if (red) Utility.instance.TurnOn(flagButtons[1].gameObject);
                if (green) Utility.instance.TurnOn(flagButtons[2].gameObject);
                if (yellow) Utility.instance.TurnOn(flagButtons[3].gameObject);
                foreach (Tile t in currentEncounter.tileList)
                {
                    for (int i = 0; i < currentEncounter.howManyFlags; i++)
                    {
                        if (t.x == -1 && t.y == -1 + i)
                        {
                            t.GetComponent<SpriteRenderer>().sprite = SpriteList.instance.none;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void SortDamage(List<Character> list)
    {
        Character temp;
        for (int j = 0; j <= list.Count - 2; j++)
        {
            for (int i = 0; i <= list.Count - 2; i++)
            {
                if (list[i].damageDone < list[i + 1].damageDone)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
    }
    public void DamageNumbersOn()
    {
        meter = Meters.Damage;
    }
    public void ThreatNumbersOn()
    {
        meter = Meters.Threat;
    }
    public void MinMax()
    {
        if (minimized)
        {
            Utility.instance.TurnOn(meterBox.gameObject);
            minimized = false;
        }
        else
        {
            Utility.instance.TurnOff(meterBox.gameObject);
            minimized = true;
        }
    }
    public void PreEncounterButtonOne() => currentEncounter.preEncounter.Button1();
    public void PreEncounterButtonTwo() => currentEncounter.preEncounter.Button2();
    public void PreEncounterButtonThree() => currentEncounter.preEncounter.ButtonThree();
    public void PreEncounterButtonFour() => currentEncounter.preEncounter.ButtonFour();
    public void PreEncounterButtonFive() => currentEncounter.preEncounter.ButtonFive();
    public void StatsInfo(Character character, List<TMP_Text> list)
    {
        list[0].text = $"Max Mana: {character.maxMana.value}";
        list[1].text = (character.GetComponent<Beserker>()) ? $"Mana Regen: {character.GetComponent<Beserker>().energyGainOnHit}/{character.GetComponent<Beserker>().energyGainOnReceiveHit} per hit" : $"Mana Regen: {character.manaRegenValue.value} per {character.manaRegenTime.value} sec";
        list[2].text = $"Vamp: {character.vamp.value}";
        list[3].text = $"Haste: {character.haste.value}%";
        list[4].text = $"Max Health: {character.maxHealth.value}";
        list[5].text = $"Defence: {character.defence.value}";
        list[6].text = $"Damage: {character.damage.value}";
        list[7].text = $"Spell Power: {character.spellpower.value}";
        list[8].text = $"Attack Power: {character.attackPower.value}";
        list[9].text = $"Crit: {character.crit.value}%";
    }
    public void ModInfo(Character character, List<TMP_Text> list)
    {
        list[0].text = $"Magic Damage: {character.magicDamageMod.value * 100}%";
        list[1].text = $"Mana Regen: {character.manaRegenMod.value * 100}%";
        list[2].text = $"Energy Cost: {character.energyCostMod.value * 100}%";
        list[3].text = $"Movement: {character.movement.value}";        
        list[4].text = $"Healing: {character.healingMod.value * 100}%";
        list[5].text = $"Damage Taken: {character.damageTakenMod.value * 100}%";
        list[6].text = $"Physical Damage: {character.physicalDamageMod.value * 100}%";
        list[7].text = $"Thorns: {character.thorns.value}";
    }
    public void BlueFlag() => currentEncounter.Order(1);
    public void RedFlag() => currentEncounter.Order(2);
    public void GreenFlag() => currentEncounter.Order(3);
    public void YellowFlag() => currentEncounter.Order(4);
    public void AllFlag() => currentEncounter.Order(5);
    public void SelectedWait() => currentEncounter.Order(6);
    public void SelectedResume() => currentEncounter.Order(7);
    public void SelectedTarget() => currentEncounter.Order(8);
    public void AllWait() => currentEncounter.Order(9);
    public void AllResume() => currentEncounter.Order(10);
    public void AllTarget() => currentEncounter.Order(11);
    public void RunFromHazard() => currentEncounter.Order(12);
    public void RunToBoss() => currentEncounter.Order(13);
    public void RunFromBoss() => currentEncounter.Order(14);
    public void SelectAll() => currentEncounter.Select(currentEncounter.player, Selected.All);
    public void Tank() => currentEncounter.Select(currentEncounter.FindTank(), Selected.TANK);
    public void DPS() => currentEncounter.Select(currentEncounter.FindDPS(), Selected.DPS);
    public void Support() => currentEncounter.Select(currentEncounter.FindSupport(), Selected.SUPPORT);
}