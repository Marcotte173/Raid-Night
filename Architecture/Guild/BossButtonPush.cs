using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButtonPush : MonoBehaviour
{
    public int id;
    public void Push()
    {
        Guild.instance.BossInfo(id);
    }
}
