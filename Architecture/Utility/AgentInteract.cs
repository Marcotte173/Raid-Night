using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AgentInteract : MonoBehaviour
{
    public bool drag;
    Vector2 mousePosition;
    Vector2 objPosition; 

    private void OnMouseDrag()
    {
        if(DungeonManager.instance.raidMode == RaidMode.Setup)
        {
            foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.characterMoveTiles)
            {
                if (t == UserControl.instance.mouseTile) t.GetComponent<SpriteRenderer>().sprite = t.characterMove;
                else t.GetComponent<SpriteRenderer>().sprite = t.unselected;
            }
            mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector2(objPosition.x, objPosition.y);
        }        
    }
    private void OnMouseUp()
    {        
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.characterMoveTiles) t.GetComponent<SpriteRenderer>().sprite = t.pic;
        Vector2 old = GetComponent<Move>().prevPosition;
        //selected = false;
        if (DungeonManager.instance.raidMode == RaidMode.Setup)
        {
            drag = false;
            if (DungeonManager.instance.currentDungeon.currentEncounter.characterMoveTiles.Contains(UserControl.instance.mouseTile))
            {
                NewPosition();
                GetComponent<Move>().prevPosition = transform.position;
            }
            else OldPosition();
        }
    }

    public void NewPosition()
    {
        transform.position = UserControl.instance.mouseTile.transform.position;
    }
    public void OldPosition()
    {
        transform.position = new Vector2(GetComponent<Move>().prevPosition.x, GetComponent<Move>().prevPosition.y);
    }
}