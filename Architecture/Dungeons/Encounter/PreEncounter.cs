using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PreEncounter : MonoBehaviour
{    
    [HideInInspector]
    public Character tankAtHome;
    public int circumstance;
    public Character chosenCharacter;
    [HideInInspector]
    public bool end;
    public int genericNumber;
    public virtual void Ready()
    {        
        chosenCharacter = null;
        EncounterUI.instance.currentEncounter.tank = null;
        EncounterUI.instance.currentEncounter.offTank = null;
        EncounterUI.instance.currentEncounter.healer = null;
        EncounterUI.instance.currentEncounter.offHealer = null;
        EncounterUI.instance.attemptText.text = "";
        //Identify Tank and offtank
        List<Character> t = new List<Character> { };
        List<Character> ot = new List<Character> { };
        foreach (Character a in EncounterUI.instance.currentEncounter.player)
        {
            Class c = (Class)a;
            if (c.spec == Spec.Stalwart) t.Add(a);
            else if (c.GetComponent<Rogue>() || c.GetComponent<ShieldBearer>()||c.GetComponent<Beserker>()) ot.Add(a);
        }
        if (t.Count == 1) EncounterUI.instance.currentEncounter.tank = t[0];
        else if (t.Count > 1) EncounterUI.instance.currentEncounter.tank = HighestHP(t);
        foreach (Character a in t) if (EncounterUI.instance.currentEncounter.tank != a) ot.Add(a);
        if (ot.Count ==1) EncounterUI.instance.currentEncounter.offTank = ot[0];
        else if (ot.Count > 1) EncounterUI.instance.currentEncounter.offTank = HighestHP(ot);
        //If testing, skip to readyCheck();
        if (GameManager.instance.test)
        {
            DungeonManager.instance.currentDungeon.currentEncounter.BeginPreSetup();
        }
        else
        {
            //If you didn't Bring a tank
            if (EncounterUI.instance.currentEncounter.tank == null) NoTank();
            else Circumstances();
        }        
    }

    public virtual void Circumstances()
    {
        //Generic Stuff
        if (Random.Range(0, 100) < genericNumber) Generic();
        // Event Specific
        else Begin();
    }

    public virtual void NoTank()
    {
        circumstance = 99;
        EncounterUI.instance.flavor[0].text = "No Tank";
        EncounterUI.instance.flavor[1].text = "";
        EncounterUI.instance.flavor[2].text = "";
        EncounterUI.instance.flavor[3].text = "";
        EncounterUI.instance.flavor[4].text = "So you didn't... bring a tank";
        EncounterUI.instance.flavor[5].text = "";
        EncounterUI.instance.flavor[6].text = "";
        EncounterUI.instance.flavor[7].text = $"Well, we'll see how that goes...";
        EncounterUI.instance.flavor[8].text = "";
        EncounterUI.instance.flavor[9].text = "";
        TimeManagement.instance.MinuteAdd(20);
        OneButton("Gulp!");
    }

    private Character HighestHP(List<Character> list)
    {
        Character a = list[0];
        for (int i = 1; i < list.Count; i++) if (list[i].maxHealth.value > a.maxHealth.value) a = list[i];
        return a;
    }

    private void Generic()
    {
        //int x = Random.Range(1, 4);
        int x = 3;
        circumstance = x;
        if (x == 1)
        {
            EncounterUI.instance.flavor[0].text = "Clean pulls";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = "Shockingly, everything goes very smoothly";
            EncounterUI.instance.flavor[5].text = "";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = $"+{EncounterUI.instance.currentEncounter.pullTime} mins";
            EncounterUI.instance.flavor[8].text = "";
            EncounterUI.instance.flavor[9].text = "";
            TimeManagement.instance.MinuteAdd(20);
            Setup("Great!");
        }
        else if (x == 2)
        {
            int p = Random.Range(0, EncounterUI.instance.currentEncounter.player.Count);
            chosenCharacter = EncounterUI.instance.currentEncounter.player[p];
            EncounterUI.instance.flavor[0].text = "Shortcut";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = $"{chosenCharacter.characterName} turns to the group and says ";
            EncounterUI.instance.flavor[5].text = "'Hey guys! I know which monsters we can skip to get to the boss faster'";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = "You're not sure tho. You're pretty sure if they mess it up it could take longer";
            EncounterUI.instance.flavor[8].text = "";
            EncounterUI.instance.flavor[9].text = "";
            TwoButtons("Take the Shortcut", "Play it safe");
        }
        else if (x == 3)
        {
            List<Character> potGuildTank = new List<Character> { };
            if (Guild.instance.NotInDungeon(EncounterUI.instance.currentEncounter.player).Count > 0)
            {
                foreach(Player a in Guild.instance.NotInDungeon(EncounterUI.instance.currentEncounter.player)) if (a.currentClass.GetComponent<Beserker>() || a.currentClass.GetComponent<ShieldBearer>()) potGuildTank.Add(a.currentClass);
            }
            if (potGuildTank.Count > 0)
            {
                tankAtHome = potGuildTank[Random.Range(0, potGuildTank.Count)];
                Debug.Log(tankAtHome.characterName);
            }
            EncounterUI.instance.flavor[0].text = "Tank Phone Call";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = $"{EncounterUI.instance.currentEncounter.tank.characterName} just got a phone call they can't ignore.";
            EncounterUI.instance.flavor[3].text = "He says he'll be back in 5 but you've experienced his \"5 minute\" calls before";
            EncounterUI.instance.flavor[4].text = "";
            EncounterUI.instance.flavor[5].text = "You could wait for him, or just press on without a tank";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = (EncounterUI.instance.currentEncounter.offTank != null)?$"{EncounterUI.instance.currentEncounter.offTank.characterName} mentions that they can offtank":"";
            EncounterUI.instance.flavor[8].text = (tankAtHome != null) ? $"Someone mentions that {tankAtHome.characterName} is online. They could probably sub in" :"";
            EncounterUI.instance.flavor[9].text = (tankAtHome != null) ? $"{EncounterUI.instance.currentEncounter.tank.characterName} might be pretty mad to be dropped tho":"";
            string line3 = "";
            string line4 = "";
            int optCount = 2;
            int buttonPlace = 0;
            if(EncounterUI.instance.currentEncounter.offTank != null)
            {
                optCount++;
                line3 = $"{EncounterUI.instance.currentEncounter.offTank.characterName} can off tank";
                buttonPlace = 2;
            }
            if (tankAtHome != null)
            {
                optCount++;
                if (line3 == "")
                {
                    line3 = $"Drop him. {tankAtHome.characterName} can tank for us";
                    buttonPlace = 3;
                }
                else line4 = $"Drop him. {tankAtHome.characterName} can tank for us";
            }
            Debug.Log(optCount);
            if (optCount == 4) FourButtons("Wait for the Tank", "We don't NEED him for trash", line3, line4);
            else if (optCount == 3) ThreeButtons("Wait for the Tank", "We don't NEED him for trash", line3,buttonPlace);
            else TwoButtons("Wait for the Tank", "We don't NEED him for trash");
        };
    }

    public virtual void Begin()
    {
        Generic();
    }

    public void PreSetup()
    {
        EncounterUI.instance.attemptText.text = "Attempt: " + EncounterUI.instance.currentEncounter.attempt;
        EncounterUI.instance.timeText.text = TimeManagement.instance.TimeOfDayDisplay();
        EncounterUI.instance.flavor[0].text = "Ready Check";
        EncounterUI.instance.flavor[1].text = "";
        EncounterUI.instance.flavor[2].text = $"{EncounterUI.instance.currentEncounter.player[0].characterName} is ready";
        EncounterUI.instance.flavor[3].text =   (EncounterUI.instance.currentEncounter.player.Count>1)?$"{EncounterUI.instance.currentEncounter.player[1].characterName} is ready":"";
        EncounterUI.instance.flavor[4].text =   (EncounterUI.instance.currentEncounter.player.Count>2)?$"{EncounterUI.instance.currentEncounter.player[2].characterName} is ready":"";
        EncounterUI.instance.flavor[5].text =   (EncounterUI.instance.currentEncounter.player.Count>3)?$"{EncounterUI.instance.currentEncounter.player[3].characterName} is ready":"";
        EncounterUI.instance.flavor[6].text =   (EncounterUI.instance.currentEncounter.player.Count>4)?$"{EncounterUI.instance.currentEncounter.player[4].characterName} is ready":"";
        EncounterUI.instance.flavor[7].text = "";
        EncounterUI.instance.flavor[8].text = "";
        EncounterUI.instance.flavor[9].text = "";
        TwoButtons("Begin", "Return");
    }
    public void Button1()
    {
        if (DungeonManager.instance.raidMode == RaidMode.PreSetup)
        {
            UIManager.instance.Combat();
            DungeonManager.instance.currentDungeon.GetComponent<EncounterManager>().BeginPlacement();
        }
        else ButtonOne();
    }
    public virtual void ButtonOne()
    {
        if (circumstance == 99)
        {
            bool success = (Random.Range(0, 10) < 2);
            EncounterUI.instance.flavor[0].text = (success) ? "Success!" : "Oh NO!";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = (success) ? $"Turns out you didn't need a tank!" : $"Who would've thought you need a tank to tank mobs?!";
            EncounterUI.instance.flavor[5].text = (success) ? "You clear the trash and move on" : "You finally get to the boss 30 minutes later than scheduled";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = (success) ? $"+{EncounterUI.instance.currentEncounter.pullTime} mins" : $"+{EncounterUI.instance.currentEncounter.pullTime + 30} minutes";
            EncounterUI.instance.flavor[8].text = "";
            EncounterUI.instance.flavor[9].text = "";
            TimeManagement.instance.MinuteAdd((success) ? EncounterUI.instance.currentEncounter.pullTime : EncounterUI.instance.currentEncounter.pullTime + 30);
            Setup((success) ? "Great!" : "Ugh!");
        }
        if (circumstance == 2)
        {
            bool success = (Random.Range(0, 10) < 4);
            EncounterUI.instance.flavor[0].text = (success) ? "Success!" : "Oh NO!";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = (success) ? $"Turns out {chosenCharacter.characterName} isn't a TOTAL idiot." : $"As you wipe for the 3rd time, you make a mental to never trust {chosenCharacter.characterName} again";
            EncounterUI.instance.flavor[5].text = (success) ? "You end up saving 10 minutes!" : "You finally get to the boss 15 minutes later than scheduled";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = (success) ? $"+{EncounterUI.instance.currentEncounter.pullTime - 10} mins" : $"+{EncounterUI.instance.currentEncounter.pullTime + 15} minutes";
            EncounterUI.instance.flavor[8].text = "";
            EncounterUI.instance.flavor[9].text = "";
            TimeManagement.instance.MinuteAdd((success) ? EncounterUI.instance.currentEncounter.pullTime - 10 : EncounterUI.instance.currentEncounter.pullTime + 15);
            Setup((success) ? "Great!" : "Oops!");
        }
        if(circumstance == 3)
        {
            int roll = Random.Range(1, 101);
            EncounterUI.instance.flavor[0].text = (roll < 11) ? "A swift return!" : (roll < 31) ? "A swift ish return" : (roll < 61) ? "Well, they returned" : (roll < 91) ? "This is ridiculous" : "I'm shocked they came back";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = (roll < 11) ? "Shockingly, they are back 5 minutes later" : (roll < 31) ? "5 minutes, 15 minutes, what's the difference?" : (roll < 61) ? "As you suspected, they return a half hour later" : (roll < 91) ? "After a half hour, they come back on briefly to let you know they will be 5 more minute" : "One HOUR later they return";
            EncounterUI.instance.flavor[5].text = (roll < 11) ? "You thank your lucky stars and move on" : (roll < 31) ? "Knowing it could have been WAY worse, you move on" : (roll < 61) ? "This is annoying, but not devastating to the night" : (roll < 91) ? "15 minutes after that they finally return" : "It's clear they don't really undersand why you are upset";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = (roll < 11) ? $"+{EncounterUI.instance.currentEncounter.pullTime + 5} mins" : (roll < 31) ? $"+{EncounterUI.instance.currentEncounter.pullTime + 15} mins" : (roll < 61) ? $"+{EncounterUI.instance.currentEncounter.pullTime + 30} mins" : (roll < 91) ? $"+{EncounterUI.instance.currentEncounter.pullTime + 45} mins" : $"+{EncounterUI.instance.currentEncounter.pullTime + 60} mins";
            EncounterUI.instance.flavor[8].text = "";
            EncounterUI.instance.flavor[9].text = "";
            TimeManagement.instance.MinuteAdd((roll < 11) ? EncounterUI.instance.currentEncounter.pullTime + 5 : (roll < 31) ? EncounterUI.instance.currentEncounter.pullTime + 15 : (roll < 61) ? EncounterUI.instance.currentEncounter.pullTime + 30 : (roll < 91) ? EncounterUI.instance.currentEncounter.pullTime + 45 : EncounterUI.instance.currentEncounter.pullTime + 60);
            Setup((roll < 11) ? "Phew!" : (roll < 31) ? "Not Too bad" : (roll < 61) ? "Could be worse" : (roll < 91) ? "Come ON!" : "Are you SERIOUS?!?!");
        }
    }
    public void Button2()
    {
        if (DungeonManager.instance.raidMode == RaidMode.PreSetup)
        {
            EndMatch.instance.SendHome();
            UIManager.instance.Menu();
        }
        else ButtonTwo();
    }
    public virtual void ButtonTwo()
    {
        if (circumstance == 2)
        {
            EncounterUI.instance.flavor[0].text = "Play it safe";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = "You play it safe, and everything gets done well, if slightly slow";
            EncounterUI.instance.flavor[5].text = "";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = $"+{EncounterUI.instance.currentEncounter.pullTime + 5} mins";
            EncounterUI.instance.flavor[8].text = "";
            EncounterUI.instance.flavor[9].text = "";
            TimeManagement.instance.MinuteAdd(EncounterUI.instance.currentEncounter.pullTime + 5);
            Setup("Ok");
        }
        if (circumstance == 3)
        {
            int roll = Random.Range(1, 101);
            EncounterUI.instance.flavor[0].text = (roll < 11) ? "That went... Well!" : (roll < 91) ? "Well, that's why we use a tank!" : "Disaster!";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = (roll < 11) ? "It went WAY smoother than you thought it would" : (roll < 91) ? "It was a SLOG, but you got through" : "You just wipe over and over";
            EncounterUI.instance.flavor[5].text = (roll < 11) ? "Smoother than normal" : (roll < 91) ? "" :"Eventually, everyone gets mad and gives up";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = (roll < 11) ? $"+{EncounterUI.instance.currentEncounter.pullTime - 5} mins" : (roll < 91) ? $"+{EncounterUI.instance.currentEncounter.pullTime + 45} mins" : "Raid night is OVER";
            EncounterUI.instance.flavor[8].text = "";
            EncounterUI.instance.flavor[9].text = "";
            TimeManagement.instance.MinuteAdd((roll < 11) ? EncounterUI.instance.currentEncounter.pullTime - 5 : (roll < 31) ? EncounterUI.instance.currentEncounter.pullTime + 45 : 0);
            Setup((roll < 11) ? "Nice!" : (roll < 91) ? "Oh well" : "Try again next time");
            if (roll > 90) end = true;
        }
    }
    
    public virtual void ButtonThree()
    {
        if (circumstance == 3)
        {
            int roll = Random.Range(1, 101);
            EncounterUI.instance.flavor[0].text = (roll < 31) ? "That went... Well!" : (roll < 71) ? "Not Terrible" : "Well, that's why we use a tank!";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = (roll < 31) ? "Overall, it went pretty well" : (roll < 71) ? "It went ok" : "Off tanks are off tanks for a reason";
            EncounterUI.instance.flavor[5].text = "";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = (roll < 31) ? $"+{EncounterUI.instance.currentEncounter.pullTime + 5} mins" : (roll < 71) ? $"+{EncounterUI.instance.currentEncounter.pullTime + 15} mins" : $"+{EncounterUI.instance.currentEncounter.pullTime + 25} mins";
            EncounterUI.instance.flavor[8].text = "";
            EncounterUI.instance.flavor[9].text = "";
            TimeManagement.instance.MinuteAdd((roll < 31) ? EncounterUI.instance.currentEncounter.pullTime + 5 : (roll < 71) ? EncounterUI.instance.currentEncounter.pullTime + 15 : EncounterUI.instance.currentEncounter.pullTime + 25);
            Setup("Ok");
        }
    }
    public virtual void ButtonFour()
    {
        if (circumstance == 3)
        {
            EncounterUI.instance.flavor[0].text =$"The replacent tank";
            EncounterUI.instance.flavor[1].text = "";
            EncounterUI.instance.flavor[2].text = "";
            EncounterUI.instance.flavor[3].text = "";
            EncounterUI.instance.flavor[4].text = $"You kick {EncounterUI.instance.currentEncounter.tank.characterName} and invite {tankAtHome.characterName} to join";
            EncounterUI.instance.flavor[5].text = $"{tankAtHome.characterName} is ecstatic! {EncounterUI.instance.currentEncounter.tank.characterName}, not so much ";
            EncounterUI.instance.flavor[6].text = "";
            EncounterUI.instance.flavor[7].text = $"{tankAtHome.characterName} + 5 Guild Loyalty/ +3 Happiness";
            EncounterUI.instance.flavor[8].text = $"{EncounterUI.instance.currentEncounter.tank.characterName} +7 Guild Loyalty/ -5 Happiness";
            EncounterUI.instance.flavor[9].text = "";
            tankAtHome.player.guildLoyalty += 5;
            EncounterUI.instance.currentEncounter.tank.player.guildLoyalty += 5;
            EncounterUI.instance.currentEncounter.player.Remove(EncounterUI.instance.currentEncounter.tank);
            EncounterUI.instance.currentEncounter.player.Add(tankAtHome);
            EncounterUI.instance.currentEncounter.tank = tankAtHome;
            tankAtHome = null;
            Setup("Ok");
        }
    }
    public virtual void ButtonFive()
    {
        if(!end) DungeonManager.instance.currentDungeon.currentEncounter.BeginPreSetup();
        else
        {
            //End raid for the night
            end = false;
            EndMatch.instance.SendHome();
            UIManager.instance.Menu();
        }
    }
    public void Setup(string a)
    {
        foreach(Button button in EncounterUI.instance.choices) Utility.instance.TurnOff(button.gameObject);
        Utility.instance.TurnOn(EncounterUI.instance.choices[4].gameObject);
        EncounterUI.instance.choices[4].GetComponentInChildren<TMP_Text>().text = a;
        EncounterUI.instance.choices[4].transform.position = EncounterUI.instance.buttonAnchor[4].transform.position;
    }
    public void OneButton(string a)
    {
        foreach (Button button in EncounterUI.instance.choices) Utility.instance.TurnOff(button.gameObject);
        Utility.instance.TurnOn(EncounterUI.instance.choices[0].gameObject);
        EncounterUI.instance.choices[0].GetComponentInChildren<TMP_Text>().text = a;
        EncounterUI.instance.choices[0].transform.position = EncounterUI.instance.buttonAnchor[4].transform.position;
    }
    public void TwoButtons(string a, string b)
    {
        foreach (Button button in EncounterUI.instance.choices) Utility.instance.TurnOff(button.gameObject);
        for (int i = 0; i < 2; i++) Utility.instance.TurnOn(EncounterUI.instance.choices[i].gameObject);
        EncounterUI.instance.choices[0].GetComponentInChildren<TMP_Text>().text = a;        
        EncounterUI.instance.choices[1].GetComponentInChildren<TMP_Text>().text = b;
        EncounterUI.instance.choices[0].transform.position = EncounterUI.instance.buttonAnchor[1].transform.position;
        EncounterUI.instance.choices[1].transform.position = EncounterUI.instance.buttonAnchor[2].transform.position;
    }
    public void ThreeButtons(string a, string b,string c)
    {
        foreach (Button button in EncounterUI.instance.choices) Utility.instance.TurnOff(button.gameObject);
        for (int i = 0; i < 3; i++) Utility.instance.TurnOn(EncounterUI.instance.choices[i].gameObject);
        EncounterUI.instance.choices[0].GetComponentInChildren<TMP_Text>().text = a;        
        EncounterUI.instance.choices[1].GetComponentInChildren<TMP_Text>().text = b;
        EncounterUI.instance.choices[2].GetComponentInChildren<TMP_Text>().text = c;
        EncounterUI.instance.choices[0].transform.position = EncounterUI.instance.buttonAnchor[0].transform.position;
        EncounterUI.instance.choices[1].transform.position = EncounterUI.instance.buttonAnchor[4].transform.position;
        EncounterUI.instance.choices[2].transform.position = EncounterUI.instance.buttonAnchor[3].transform.position;
    }
    public void ThreeButtons(string a, string b, string c,int x)
    {
        foreach (Button button in EncounterUI.instance.choices) Utility.instance.TurnOff(button.gameObject);
        for (int i = 0; i < 3; i++) Utility.instance.TurnOn(EncounterUI.instance.choices[i].gameObject);
        EncounterUI.instance.choices[0].GetComponentInChildren<TMP_Text>().text = a;
        EncounterUI.instance.choices[1].GetComponentInChildren<TMP_Text>().text = b;
        EncounterUI.instance.choices[x].GetComponentInChildren<TMP_Text>().text = c;
        EncounterUI.instance.choices[0].transform.position = EncounterUI.instance.buttonAnchor[0].transform.position;
        EncounterUI.instance.choices[1].transform.position = EncounterUI.instance.buttonAnchor[4].transform.position;
        EncounterUI.instance.choices[x].transform.position = EncounterUI.instance.buttonAnchor[3].transform.position;
    }
    public void FourButtons(string a, string b,string c, string d)
    {
        foreach (Button button in EncounterUI.instance.choices) Utility.instance.TurnOff(button.gameObject);
        for (int i = 0; i < 4; i++) Utility.instance.TurnOn(EncounterUI.instance.choices[i].gameObject);
        EncounterUI.instance.choices[0].GetComponentInChildren<TMP_Text>().text = a;
        EncounterUI.instance.choices[1].GetComponentInChildren<TMP_Text>().text = b;
        EncounterUI.instance.choices[2].GetComponentInChildren<TMP_Text>().text = c;
        EncounterUI.instance.choices[3].GetComponentInChildren<TMP_Text>().text = d;
        EncounterUI.instance.choices[0].transform.position = EncounterUI.instance.buttonAnchor[0].transform.position;
        EncounterUI.instance.choices[1].transform.position = EncounterUI.instance.buttonAnchor[1].transform.position;
        EncounterUI.instance.choices[2].transform.position = EncounterUI.instance.buttonAnchor[2].transform.position;
        EncounterUI.instance.choices[3].transform.position = EncounterUI.instance.buttonAnchor[3].transform.position;
    }
    public virtual void StartThemeSong()
    {

    }
}
