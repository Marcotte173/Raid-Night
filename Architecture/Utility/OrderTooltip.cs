using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OrderTooltip : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public List<string> flavor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (flavor.Count > 0)
        {
            Utility.instance.TurnOn(EncounterUI.instance.orderToolTipBox);
            for (int i = 0; i < flavor.Count; i++) EncounterUI.instance.orderToolTipFlavor[i].text = flavor[i];
        }
    }
    public void OnPointerExit(PointerEventData eventData) => EncounterUI.instance.OrderToolTipOff();
   
}
