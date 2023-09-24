using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class EffectTooltip : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public List<string> flavor;
    public GameObject tooltipAnchor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (flavor.Count > 0)
        {
            EncounterUI.instance.currentEffectTooltip = this;
            EncounterUI.instance.updateTime = true;
            Utility.instance.TurnOn(EncounterUI.instance.uITooltipBox);
            EncounterUI.instance.uITooltipBox.transform.position = tooltipAnchor.transform.position;
            for (int i = 0; i < flavor.Count; i++)
            {
                EncounterUI.instance.uITooltipFlavor[i].text = flavor[i];
            }
        }        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        EncounterUI.instance.currentEffectTooltip = null;
        EncounterUI.instance.updateTime = false;
        foreach (TMP_Text t in EncounterUI.instance.uITooltipFlavor) t.text = "";
        Utility.instance.TurnOff(EncounterUI.instance.uITooltipBox);
    }
}
