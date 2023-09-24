using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemTooltip : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler,IPointerClickHandler
{
    public int id;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        ToolTip();
        Guild.instance.itemToolTipHold = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTip();
    }
    public void ToolTip()
    {
        if (Guild.instance.manageState == ManageState.View) Guild.instance.ItemTooltipOn(id);
        else if (Guild.instance.manageState == ManageState.Recruit) { if (Guild.instance.seeHidden) Guild.instance.ItemTooltipOn(id); }
        else Guild.instance.DropItemTooltipOn(id);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Guild.instance.itemToolTipHold)
        {
            Guild.instance.ItemTooltipOff();
            Guild.instance.itemToView = null;
            Guild.instance.itemSOToView = null;
        }
    }
}
