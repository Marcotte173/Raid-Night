using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveInfo : MonoBehaviour
{
    public List<TMP_Text> strings;
    public int id;

    public void Click()
    {
        Guild.instance.ButtonPress(id);
    }
    public void Assign()
    {
        Guild.instance.Assign(id);
    }
}
