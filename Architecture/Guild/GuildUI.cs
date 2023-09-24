using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GuildUI : MonoBehaviour
{
    public static GuildUI instance;
    public List<TMP_Text> playerInfo;
    public List<TMP_Text> characterInfo;
    public List<TMP_Text> raidInfo;
    public List<Image> raidRoleImages;
    public List<Button> characterButton;
    public List<Button> guildButton;
    public List<Button> playerButton;
    public List<TMP_Text> playerButtonNames;
    public List<TMP_Text> playerButtonClasses;
    public List<TMP_Text> playerButtonCost;
    public List<TMP_Text> guildInfo;
    public GameObject buttonHeading;
    public Button button;
    public Image dropImage;
    public Image bossButton;
    public GameObject bossHeading;
    public Image guildDay;
    public GameObject guildDayHeading;
    public Image guildUpgradeImage;
    public List<Image> guildUpgradeImages;
    public GameObject guildUpgradeHeading;
    public GameObject codexCategoryHeading;
    public Image codexButton;
    public GameObject codexEntryHeading;
    public GameObject dropHeading;
    public GameObject playerBox;
    public GameObject paperDoll;
    public List<Image> paperDollImages;
    //Turn information on an off in a block. Used for tasks and scheduling
    public GameObject playerInformationObject;
    public GameObject characterInformationObject;
    public GameObject raidInformationObject;
    public GameObject upgradeInformationObject;
    public GameObject taskObject;
    public GameObject itemTooltip;
    public List<TMP_Text> ItemTooltipInfo;
    public GameObject guildTooltip;
    public List<TMP_Text> guildTooltipInfo;
    public List<GameObject> backgroundObject;
    public TMP_Dropdown scheduleDropdown;
    public TMP_Dropdown raidDropdown;
    public List<Sprite> roleSprites;
    public List<Button> raidButtons;
    public GameObject raidInfoBox;
    public GameObject calendar;
    public GameObject codex;
    public List<Image> guildDays;
    public List<Image> bossDropSprite;
    public List<Image> bossInfoButton;
    public List<TMP_Text> bossInfoText;
    public GameObject abilities;
    public List<Image> bossAbilityImages;
    public List<TMP_Text> calendarInfo;
    public GameObject upgradeLeftSide;
    public List<TMP_Text> upgradeLeftText;
    public TMP_Text upgradeInstruction;
    public List<TMP_Text> codexText;
    public Image codexPic;
    public List<Image> codexCategoryButtons;
    public List<Image> codexEntryButtons;
    public List<Button> codexBackForthButtons;
    public List<Image> itemTooltipRoleImage;
    public TMP_Text recruitViewInstruction;   
    public GameObject raidButtonHeading;
    public Image raidSelectButton;
    public List<Image> raidSelectionButtons;
    public List<Image>    backgroundColor;
    public List<Image>    dungeonPic;
    public List<TMP_Text> dungeonName;
    public List<TMP_Text> dungeonBossNumber;
    public List<TMP_Text> gearScoreNumber;
    public List<Image>    dungeonCompletePic;
    public Button vanilla;
    public Button undead;
    public Button beyond;
    public GameObject raidSelect;
    public GameObject raidDisplay;
    public TMP_Text raidName;

    private void Update()
    {
        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Escape))
        {
            if (Guild.instance.guildToolTipHold)
            {
                Guild.instance.guildToolTipHold = false;
                Guild.instance.GuildTooltipOff();
            }
            else if (Guild.instance.itemToolTipHold)
            {
                
                Guild.instance.itemToolTipHold = false;
                Guild.instance.ItemTooltipOff();
            }
        }
    }
    private void Awake()
    {
        instance = this;
        for (int i = 0; i <30; i++)
        {
            Button b = Instantiate(button, buttonHeading.transform);
            GiveInfo g = b.GetComponent<GiveInfo>();
            playerButtonNames.Add(g.strings[0]);
            playerButtonClasses.Add(g.strings[1]);
            playerButtonCost.Add(g.strings[2]);
            playerButton.Add(b);
            g.id = i;
        }
        for (int i = 0; i < 20; i++)
        {
            Image b = Instantiate(dropImage, dropHeading.transform);
            ItemSprite isp = b.GetComponent<ItemSprite>();
            ItemTooltip g = isp.pic.GetComponent<ItemTooltip>();
            g.id = i;
            bossDropSprite.Add(b);
        }
        for (int i = 0; i < 28; i++)
        {
            Image b = Instantiate(guildDay, guildDayHeading.transform);
            ItemSprite isp = b.GetComponent<ItemSprite>();
            isp.flavor[0].text = (i + 1).ToString();
            guildDays.Add(b);                   
        }
        for (int i = 0; i < 28; i++)
        {
            Image b = Instantiate(guildUpgradeImage, guildUpgradeHeading.transform);
            ItemSprite isp = b.GetComponent<ItemSprite>();
            isp.flavor[0].GetComponent<GuildTooltip>().id = i;
            guildUpgradeImages.Add(b);
        }
        for (int i = 0; i < 10; i++)
        {
            Image b = Instantiate(bossButton, bossHeading.transform);
            ItemSprite isp = b.GetComponent<ItemSprite>();
            Button button = isp.button;
            BossButtonPush g = button.GetComponent<BossButtonPush>();
            if (i == 0) g.id = 99;
            else g.id = i - 1;
            bossInfoButton.Add(b);
        }
        for (int i = 0; i < 20; i++)
        {
            Image b = Instantiate(codexButton, codexCategoryHeading.transform);
            ItemSprite isp = b.GetComponent<ItemSprite>();
            Button button = isp.button;
            CodexButtonPush g = button.GetComponent<CodexButtonPush>();
            g.id = i;
            codexCategoryButtons.Add(b);
        }
        for (int i = 0; i < 20; i++)
        {
            Image b = Instantiate(codexButton, codexEntryHeading.transform);
            ItemSprite isp = b.GetComponent<ItemSprite>();
            Button button = isp.button;
            CodexButtonPush g = button.GetComponent<CodexButtonPush>();
            g.id = i;
            g.entry = true;
            codexEntryButtons.Add(b);
        }
        for (int i = 0; i < 6; i++)
        {
            Image b = Instantiate(raidSelectButton, raidButtonHeading.transform);
            RaidSelectImage r = b.GetComponent<RaidSelectImage>();
            Button button = r.button;
            RaidSelectionButton rb = button.GetComponent<RaidSelectionButton>();
            rb.id = i;
            backgroundColor.Add(r.pics[0]);
            dungeonPic.Add(r.pics[1]);
            dungeonCompletePic.Add(r.pics[2]);
            dungeonName.Add(r.strings[0]);
            dungeonBossNumber.Add(r.strings[1]);
            gearScoreNumber.Add(r.strings[2]);
            raidSelectionButtons.Add(b);
        }
    }   
}