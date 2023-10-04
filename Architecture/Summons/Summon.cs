using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Class
{
    public float time;
    public Character summoner;
    private void Start()
    {
        playerUI = GetComponent<CharacterUI>();
        Equip.instance.Item(ItemList.instance.noItem, head);
        Equip.instance.Item(ItemList.instance.noItem, chest);
        Equip.instance.Item(ItemList.instance.noItem, legs);
        Equip.instance.Item(ItemList.instance.noItem, feet);
        Equip.instance.Item(ItemList.instance.noItem, trinket);
        Equip.instance.Item(ItemList.instance.noItem, weapon);
        Equip.instance.Item(ItemList.instance.noItem, offHand);
    }
    public override void UpdateStuff()
    {
        base.UpdateStuff();
        time -= Time.deltaTime;
        if (time <= 0)
        {
            if(UserControl.instance.selectedCharacter == this)
            {
                EncounterUI.instance.select = false;
                UserControl.instance.selectedCharacter = null;
            }
            DungeonManager.instance.currentDungeon.currentEncounter.playerMinionSummons.Remove(this);
            Destroy(gameObject);
        }
    }
}
