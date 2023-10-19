using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameMode {Regular,OneCharacter,FullControl,Multiplayer}
public enum Control { PlayerChoice, UserControl }
public enum ControlType { Mouse, WASD,Controller }
public enum RaidMode { Off, PreDungeon, PreSetup, Setup, Combat, Resolve, Rewards, Pause }
public enum Expansion {Vanilla,Undead,Beyond }

public class GameManager : MonoBehaviour
{    
    public static GameManager instance;
    public float defModifier;
    public float hasteModifier;
    public float cooldownModifier;
    public float thornsAoeRange;
    public float healThreat;
    public Guild guild;
    public CreateAgent freeAgent;
    public bool test;

    private void Start()
    {
        instance = this;
        SoundManager.instance.PlayMusic(SoundList.instance.creditIntroTheme);
        EventList.instance.MakeEvents();
        CodexManager.instance.Generate();
    }   

    //This is the ONLY Update in the game. ALL Others are beholden to this
    private void Update()
    {
        //Input Options during management
        if (DungeonManager.instance.raidMode == RaidMode.Off)
        {
            UserControl.instance.UpdateGuildInput();
        }
        //If there is a selected Dungeon
        if (DungeonManager.instance.currentDungeon != null)
        {
            //And there is a current Encounter
            if(DungeonManager.instance.currentDungeon.currentEncounter != null)
            {
                //Update Agents
                if (DungeonManager.instance.raidMode == RaidMode.Combat)
                {
                    DungeonManager.instance.currentDungeon.GetComponent<EncounterManager>().UpdateAgents();
                    if (DungeonManager.instance.raidMode == RaidMode.Combat) UserControl.instance.UpdateCombatInput();
                    if (DungeonManager.instance.raidMode == RaidMode.Combat) EncounterUI.instance.UpdateCombatUI();
                    if (DungeonManager.instance.raidMode == RaidMode.Combat) ObjectUpdate();
                    if (DungeonManager.instance.raidMode == RaidMode.Combat) Camera.main.GetComponent<CameraMove>().UpdateCamera();
                }
                //Input Options when Paused
                else if (DungeonManager.instance.raidMode == RaidMode.Pause)
                {
                    UserControl.instance.UpdatePauseInput();
                }
                //Input Options during setup
                else if (DungeonManager.instance.raidMode == RaidMode.Setup)
                {
                    UserControl.instance.UpdateSetupInput();
                }
            }            
        }
    }

    private void ObjectUpdate()
    {
        foreach(GameObject o in DungeonManager.instance.currentDungeon.currentEncounter.objects.ToList())
        {
            if (o != null &&o.GetComponent<Projectile>()) o.GetComponent<Projectile>().UpdateProjectile();
            if (o != null &&o.GetComponent<Hazard>()) o.GetComponent<Hazard>().UpdateHazard();
            if (o != null &&o.GetComponent<DestroyOverTime>()) o.GetComponent<DestroyOverTime>().UpdateDestroy();
            if (o != null &&o.GetComponent<Effect>()) o.GetComponent<Effect>().UpdateEffect();
            if (o != null &&o.GetComponent<DamageNumbers>()) o.GetComponent<DamageNumbers>().UpdateNumbers();
        }
    }
}
