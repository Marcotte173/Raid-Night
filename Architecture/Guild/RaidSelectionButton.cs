using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidSelectionButton : MonoBehaviour
{
    public int id;
    public void Push()
    {
        Guild.instance.RaidButtonPush(id);
    }
}
