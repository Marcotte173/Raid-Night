using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class EndMatch : MonoBehaviour
{
    public static EndMatch instance;
    private void Awake()
    {
        instance = this;
    }

    public void FindWinner()
    {
        Utility.instance.TurnOn(EncounterUI.instance.preFightBox);
        UserControl.instance.Reset();
        EncounterUI.instance.TurnOffBackgrounds();       
        TimeManagement.instance.MinuteAdd(DungeonManager.instance.currentDungeon.currentEncounter.bossFightTime);
        foreach (GameObject g in DungeonManager.instance.currentDungeon.currentEncounter.objects.ToList()) if (g != null) Destroy(g);
        DungeonManager.instance.currentDungeon.currentEncounter.objects.Clear();
        if (DungeonManager.instance.currentDungeon.currentEncounter.Player().Count == 0)
        {
            EncounterUI.instance.preFightBox.GetComponentInChildren<Text>().text = "You Lose";
            EncounterUI.instance.preFightBox.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Reset Encounter";
        }
        else
        {
            EncounterUI.instance.preFightBox.GetComponentInChildren<Text>().text = "You Win";
            EncounterUI.instance.preFightBox.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Rewards";
        }        
    }
    public void Resolve()
    {        
        if (DungeonManager.instance.currentDungeon.currentEncounter.Player().Count == 0) Lose();
        else Win();        
    }

    public void Win()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.ClearAllLists();
        DungeonManager.instance.currentDungeon.GetComponent<EncounterManager>().BeginRewards();        
    }

    public void Lose()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.ResetEncounter();
    }
    public void SendHome()
    {
        foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.player.ToList())
        {
            a.ko = false;
            DungeonManager.instance.SendHome(a.player);
            DungeonManager.instance.currentDungeon.currentEncounter.player.Clear();
        }
        Destroy(DungeonManager.instance.currentDungeon.currentEncounter.gameObject);
    }
    public void NextEncounter()
    {
        List<Character> list = DungeonManager.instance.currentDungeon.currentEncounter.player.ToList();
        Destroy(DungeonManager.instance.currentDungeon.currentEncounter.gameObject);
        DungeonManager.instance.Raid(DungeonManager.instance.currentDungeon, list);
    }
}