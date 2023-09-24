using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList instance;
    public List<ItemSO> startingHead;
    public List<ItemSO> startingChest;
    public List<ItemSO> startingLegs;
    public List<ItemSO> startingFeet;
    public List<ItemSO> startingTrinket;
    public List<ItemSO> startingWeapon;
    public List<ItemSO> startingOffHand;
    public List<ItemSO> equipmentMasterList;
    public ItemSO noItem;
    private void Awake()
    {
        instance = this;
    }
}
