using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : Boss
{
    public override void Equipment()
    {
        Equip.instance.Item(ItemList.instance.noItem, head);
        Equip.instance.Item(ItemList.instance.noItem, chest);
        Equip.instance.Item(ItemList.instance.noItem, legs);
        Equip.instance.Item(ItemList.instance.noItem, feet);
        Equip.instance.Item(ItemList.instance.noItem, trinket);
        Equip.instance.Item(ItemList.instance.noItem, weapon);
        Equip.instance.Item(ItemList.instance.noItem, offHand);
    }
    public override void Decision()
    {
        
    }
    public override void CoreStats()
    {
        base.CoreStats();
    }
}
