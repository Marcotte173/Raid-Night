using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexButtonPush : MonoBehaviour
{
    public int id;
    public bool entry;

    public void Push()
    {
        if (entry) CodexManager.instance.EntrySelect(id);
        else CodexManager.instance.CategorySelect(id);
    }
    
}
