using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public static MoveManager instance;
    public float speed;
    public float margin;
    
    private void Awake()
    {
        instance = this;
    }

    internal void Move(Move move)
    {
        //Move Toward Position
        move.transform.position = Vector3.MoveTowards(move.transform.position, new Vector3(move.nextTile.transform.position.x, move.nextTile.transform.position.y), speed * (move.character.movement.value) * UnityEngine.Time.deltaTime);        
    }

    public bool IsAtTile(Move move, Tile target) => (move.transform.position.x <= target.transform.position.x + margin && move.transform.position.x >= target.transform.position.x - margin && move.transform.position.y <= target.transform.position.y + margin && move.transform.position.y >= target.transform.position.y - margin);

    public bool GetNewPath(Character g,Tile end,bool direct)
    {
        Move move = g.move;
        move.closed.Clear();
        move.open.Clear();

        Tile start = move.currentTile;
        Tile current = start;
    
        move.unwalkable.Clear();
        move.path.Clear();
        move.open.Clear();
        move.closed.Clear();
        move.open.Add(start);

        //Find unwalkables//    
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList) if (t.id != 0) move.unwalkable.Add(t);
        //For each g 
        List<Character> agentList = move.PeopleWhoAreNotMeOrMyTarget();
        move.objectName.Clear();
        foreach (Character agent in agentList)
        {
            Move agentPawn = agent.move;           
            move.objectName.Add(agent.characterName);
            if (agentPawn.isMoving) move.unwalkable.Add(agentPawn.nextTile);
            else move.unwalkable.Add(agentPawn.currentTile);
        }
        while (!move.closed.Contains(end))
        {
            foreach (Tile neighbor in current.neighbor)
            {
                //Calculate each neighbor's f value
                neighbor.g = (move.unwalkable.Contains(neighbor)) ? 200 : 1;
                neighbor.h = Vector3.Distance(neighbor.transform.position, end.transform.position);
                neighbor.f = (neighbor.g + neighbor.h);//Consider doubling g in calculation, to disensentivise diagonals
                //Adding neighbor to the open list
                foreach (Tile t in move.open.ToList())
                {
                    if (neighbor == t && neighbor.f < t.f)
                    {
                        t.f = neighbor.f;
                        neighbor.parent = current;
                    }
                    else if (!move.open.Contains(neighbor) && (!move.closed.Contains(neighbor)) && !move.unwalkable.Contains(neighbor))
                    {
                        neighbor.parent = current;
                        move.open.Add(neighbor);
                    }
                }
            }
            move.open.Remove(current);
            move.closed.Add(current);
            if (move.open.Count > 0)
            {
                current = move.open[0];
                for (int i = 1; i < move.open.Count; i++) { if (move.open[i].f < current.f) current = move.open[i]; }
                if (current == end) break;
            }
            else break;
        }
        while (current.parent != start)
        {
            move.path.Add(current.parent);
            current = current.parent;
        }       
        move.path.Reverse();
        if(direct)move.path.Add(end);
        if (move.path.Count > 0)
        {
            move.nextTile = move.path[0];
            return true;
        }
        else return false;
    }

    public void MoveAgent(Move move)
    {
        MoveAgent(move, (move.character.target.move.isMoving) ? move.character.target.move.nextTile : move.character.target.move.currentTile);
    }

    public void MoveAgent(Move move, Tile target,bool direct)
    {
        move.isMoving = true;        
        if (move.path.Count == 0 || IsAtTile(move, move.nextTile)||move.unwalkable.Contains(move.nextTile)) GetNewPath(move.character, target,direct);
        if (move.nextTile != null) Move(move);
        else
        {
            move.character.state = DecisionState.Downtime;
            move.isMoving = false;
        }
    }
    public void MoveAgentDirect(Move move, Tile target)
    {
        MoveAgent(move, target, true);
    }
    public void MoveAgent(Move move, Tile target)
    {
        MoveAgent(move, target, false);
    }    
}
