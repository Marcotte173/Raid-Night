using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class EncounterManager : MonoBehaviour
{
    public Encounter currentEncounter;

    internal void Begin(Encounter e)
    {
        Encounter enc = Instantiate(e, DungeonManager.instance.currentDungeon.transform);
        enc.transform.position = new Vector2(0,0);
        DungeonManager.instance.currentDungeon.currentEncounter = enc;
        currentEncounter = enc;       
        enc.CreateArena();
        enc.GetComponent<EncounterFeatures>().SpawnPOI();
        enc.CreateDrops();             
        enc.ResetEncounter();
        enc.AddFlags();
    }    

    public void BeginPlacement()
    {
        Utility.instance.TurnOn(EncounterUI.instance.preFightBox);
        EncounterUI.instance.preFightBox.GetComponentInChildren<Text>().text = "Move your team into position\nPress the Start Button to begin";
        EncounterUI.instance.preFightBox.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Start";
        foreach (Character a in currentEncounter.Characters())
        {
            a.move.on = true;
        }
        Combat();
    }

    //Update What is going on in the Encounter
    public void UpdateAgents()
    {
        //All Bosses act
        foreach (Character a in currentEncounter.BossAndMinion())
        {
            a.move.UpdateMove();
            //Update Character Info
            a.UpdateStuff();
            //Act
            a.GetComponent<Boss>().State();            
            foreach (Ability ab in a.ability) ab.UpdateStuff();
        }
        //All Players Act
        foreach (Character a in currentEncounter.PlayerAndMinion())
        {
            a.move.UpdateMove();
            //Update Character Info
            a.UpdateStuff();
            //Act
            a.State();
            foreach (Ability ab in a.ability) ab.UpdateStuff();
        }
        //Make minions act
        //if (currentEncounter.PlayerMinion().Count > 0) foreach (Character a in currentEncounter.PlayerMinion()) a.GetComponentInChildren<Class>().State();
        //if (currentEncounter.BossMinion().Count > 0) foreach (Character a in currentEncounter.BossMinion()) a.GetComponent<Minion>().State();
    }
   
    
    public void Combat()
    {
        DungeonManager.instance.raidMode = RaidMode.Setup;
        foreach (Flag f in EncounterUI.instance.flags)
        {            
            Utility.instance.TurnOn(f.gameObject);
            f.transform.position = f.home.transform.position;
        }
        foreach (Button b in EncounterUI.instance.flagButtons) Utility.instance.TurnOff(b.gameObject);        
    }
    public void Rewards()
    {
        UIManager.instance.Rewards();
        DungeonManager.instance.raidMode = RaidMode.Rewards;
    }    

    internal void BeginRewards()
    {
        Rewards();
        global::Rewards.instance.currentEncounter=currentEncounter;
        global::Rewards.instance.Begin();
    }
}