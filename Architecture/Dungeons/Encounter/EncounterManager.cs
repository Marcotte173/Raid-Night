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
        foreach(Character a in currentEncounter.Characters()) a.move.UpdateMove();
        foreach (Character a in currentEncounter.BossAndMinion())
        {           
            CheckOccupied(a);
            //Update Character Info
            a.UpdateStuff();
            //Act
            a.GetComponent<Boss>().State();            
            foreach (Ability ab in a.ability) ab.UpdateStuff();
        }
        //All Players Act
        foreach (Character a in currentEncounter.PlayerAndMinion())
        {
            CheckOccupied(a);
            //Update Character Info
            a.UpdateStuff();
            //Act
            a.State();
            foreach (Ability ab in a.ability) ab.UpdateStuff();
        }
    }

    private void CheckOccupied(Character a)
    {
        
        foreach (Character agent in currentEncounter.Characters()) 
        {
            if (a != agent)
            {
                if(a.move.currentTile == agent.move.currentTile&& !agent.move.isMoving&& !a.move.isMoving)
                {
                    Debug.Log(a.move.currentTile.name);
                    a.state = DecisionState.Move;
                    if (a.target == null)
                    {
                        if (GetComponent<Boss>())
                        {
                            Boss b = GetComponent<Boss>();
                            b.GetTarget();
                        }
                        else a.GetTarget();
                    }
                    a.moveTile = FindTile.instance.FindClosestUnoccupiedTile(a.move.currentTile, a.target.move.currentTile) ;
                    Debug.Log(a.moveTile);
                }
            }               
        }
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