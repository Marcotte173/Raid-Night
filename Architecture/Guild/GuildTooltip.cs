using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuildTooltip : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    public int id;
    public List<string> flavor;
    public bool generate;
    public bool upgrade;
    public int xAdjust;
    

    public void OnPointerClick(PointerEventData eventData)
    {
        Tooltip();
        Guild.instance.guildToolTipHold = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip();
    }
    public void Tooltip()
    {
        Vector2 pos = new Vector2(transform.position.x + xAdjust, transform.position.y);
        if (generate)
        {
            if (id == 0 && Guild.instance.manageState != ManageState.Recruit) Guild.instance.GuildTooltipOn(flavor, pos);
            else if (id == 1 && (Guild.instance.seeHidden || Guild.instance.manageState != ManageState.Recruit)) Guild.instance.GuildTooltipOn(id, pos);
            else if (id == 2 && Guild.instance.targetPlayer.traits.Count > 1 && (Guild.instance.seeHidden || Guild.instance.manageState != ManageState.Recruit)) Guild.instance.GuildTooltipOn(id, pos);
            else if (id == 3 && Guild.instance.targetPlayer.traits.Count > 2 && (Guild.instance.seeHidden || Guild.instance.manageState != ManageState.Recruit)) Guild.instance.GuildTooltipOn(id, pos);
            else if (id == 4)
            {
                List<string> flavorString = new List<string> { };
                if (Guild.instance.itemSOToView != null)
                {
                    flavorString.Add(flavor[(int)Guild.instance.itemSOToView.role]);
                    flavorString.Add(flavor[(int)Guild.instance.itemSOToView.role + 5]);
                }
                else if (Guild.instance.itemToView != null)
                {
                    flavorString.Add(flavor[(int)Guild.instance.itemToView.role]);
                    flavorString.Add(flavor[(int)Guild.instance.itemToView.role + 5]);
                }
                Guild.instance.GuildTooltipOn(flavorString, pos);
            }
        }
        else if (upgrade) Guild.instance.GuildTooltipOn(Guild.instance.currentUpgrades[id].flavor, pos);
        else Guild.instance.GuildTooltipOn(flavor, pos);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Guild.instance.guildToolTipHold) Guild.instance.GuildTooltipOff();
    }
}
