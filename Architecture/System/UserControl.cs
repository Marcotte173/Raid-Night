using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserControl : MonoBehaviour
{
    public static UserControl instance;
    public Character selectedCharacter;
    public Character controlledCharacter;
    public Vector3 worldPosition;
    public Tile clickTile;
    public Tile rightClickTile;
    public Tile mouseTile;
    public Control control;
    public ControlType controlType;

    private void Awake()
    {
        instance = this;
    }
    public void UpdateSetupInput()
    {
        MousePosition();
    }
    public void UpdateCombatInput()
    {
        MousePosition();
        //Find tile mouse is over
        MousePosition();
        //If mouse over agent, Show their name
        foreach (Character a in EncounterUI.instance.currentEncounter.Characters())
        {
            if(a.playerUI!=null) Utility.instance.TurnOff(a.playerUI.nameObject);
        }
        if (mouseTile != null && mouseTile.OccupiedBy() != null) Utility.instance.TurnOn(mouseTile.OccupiedBy().playerUI.nameObject);
        if (selectedCharacter != null)
        {
            Utility.instance.TurnOn(selectedCharacter.playerUI.nameObject);
            if (selectedCharacter.target != null) Utility.instance.TurnOn(selectedCharacter.target.playerUI.nameObject);
        }
        //Check for a click
        MouseClickCheck();
        if (Input.GetKeyUp(KeyCode.Space)) DungeonManager.instance.raidMode = RaidMode.Pause;
        //If C or X is pressed, pull up info for characters as required
        if (Input.GetKey(KeyCode.C)) EncounterUI.instance.basicStats = true;
        else EncounterUI.instance.basicStats = false;
        if (Input.GetKey(KeyCode.X)) EncounterUI.instance.modInfo = true;
        else EncounterUI.instance.modInfo = false;
        //Controls if User has control
        if (control == Control.UserControl)
        {
            UpdateButtons();
            if (controlledCharacter.state != DecisionState.DashCast)
            {
                //Toggle User Control
                if (Input.GetKeyDown(KeyCode.P)) UserControlToggle(controlledCharacter);
                else
                {
                    if (controlledCharacter.state != DecisionState.Cast) controlledCharacter.action = $"";
                    if (Input.GetKeyDown(KeyCode.LeftAlt)) SwitchControlType();
                    //1-5 Abilities
                    if (Input.GetKeyUp(KeyCode.Alpha1)) Attack(1, DecisionState.Attack2);
                    else if (Input.GetKeyUp(KeyCode.Alpha2)) Attack(2, DecisionState.Attack3);
                    else if (Input.GetKeyUp(KeyCode.Alpha3)) Attack(3, DecisionState.Attack4);
                    else if (Input.GetKeyUp(KeyCode.Alpha4)) Attack(4, DecisionState.Attack5);
                    //else if (Input.GetKeyUp(KeyCode.Alpha5)) Attack(5); 
                    //else if (Input.GetKeyUp(KeyCode.Alpha6)) Attack(6);
                    ///
                    //WASD CONTROL SCHEME
                    ///
                    if (controlType == ControlType.WASD)
                    {
                        //LEFTCLICK ON CHARACTER
                        if (clickTile != null && clickTile.OccupiedBy() != null)
                        {
                            if (controlledCharacter.target != null) controlledCharacter.TargetOff();
                            controlledCharacter.target = clickTile.OccupiedBy();
                            controlledCharacter.target.OutlineOn(controlledCharacter.Enemy(controlledCharacter, controlledCharacter.target) ? 2 : 3);
                            clickTile = null;
                        }
                        //RIGHT CLICK
                        if (rightClickTile != null && controlledCharacter.target != null)
                        {
                            controlledCharacter.TargetOff();
                            rightClickTile = null;
                        }
                        //WASD
                        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                        {
                            List<Tile> occ = new List<Tile> { };
                            foreach (Character a in DungeonManager.instance.currentDungeon.currentEncounter.CharactersWhoAreNotMe(controlledCharacter)) occ.Add(a.move.currentTile);
                            if (Input.GetKey(KeyCode.W)) Move(0, 1, occ);
                            if (Input.GetKey(KeyCode.A)) Move(-1, 0, occ);
                            if (Input.GetKey(KeyCode.S)) Move(0, -1, occ);
                            if (Input.GetKey(KeyCode.D)) Move(1, 0, occ);
                        }
                        //ESCAPE
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            controlledCharacter.TargetOff();
                            controlledCharacter.OutlineOff();
                            controlledCharacter.target = null;
                        }
                    }
                    else
                    {
                        //LEFTCLICK
                        if (clickTile != null)
                        {
                            //If Not Occupied
                            if (clickTile.OccupiedBy() == null)
                            {
                                controlledCharacter.moveTile = clickTile;
                                controlledCharacter.state = DecisionState.Move;
                                clickTile = null;
                            }
                            else
                            {
                                if (controlledCharacter.target != null) controlledCharacter.TargetOff();
                                controlledCharacter.target = clickTile.OccupiedBy();
                                controlledCharacter.target.OutlineOn(controlledCharacter.Enemy(controlledCharacter, controlledCharacter.target) ? 2 : 3);
                                clickTile = null;
                                if (controlledCharacter.Enemy(controlledCharacter, controlledCharacter.target)) Attack(0, DecisionState.Attack1);
                            }
                        }
                        //RIGHT CLICK
                        if (rightClickTile != null && controlledCharacter.target != null)
                        {
                            controlledCharacter.TargetOff();
                            rightClickTile = null;
                        }
                        //ESCAPE
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            controlledCharacter.TargetOff();
                            controlledCharacter.OutlineOff();
                            controlledCharacter.target = null;
                        }
                    }
                }
            }
        }
        //Player Control
        else
        {
            if (Input.GetKeyUp(KeyCode.K)) DungeonManager.instance.currentDungeon.currentEncounter.boss[0].TakeDamage(DungeonManager.instance.currentDungeon.currentEncounter.player[0],3000,true,"poop");
            // Toggle User Control
            //***********
            //USER CONTROL REMOVED FROM TEST
            //***********
            if (Input.GetKeyDown(KeyCode.P)&& selectedCharacter != null) UserControlToggle(selectedCharacter);
            //***********
            //LEFTCLICK
            if (clickTile != null && clickTile.OccupiedBy() != null)
            {
                //If Orders are to focus on someone, Go to target Function
                if (DungeonManager.instance.currentDungeon.currentEncounter.orders == Orders.Focus && DungeonManager.instance.currentDungeon.currentEncounter.selectedTarget == null) OrderTarget();
                //Otherwise select them
                else Select(clickTile.OccupiedBy());
                clickTile = null;
            }
            //ESCAPE
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                //If you have selected a character, Deselect
                if (selectedCharacter != null) SelectOff();
                //If you have selected any group, Deselect
                else if (DungeonManager.instance.currentDungeon.currentEncounter.selected != Selected.NONE) DungeonManager.instance.currentDungeon.currentEncounter.ResetOrders();
                //Open options
                else
                {

                }
            }
            //Right CLick
            else if (Input.GetMouseButtonUp(1))
            {
                //If you right click, Deselect the player. Also RESET Orders/Selection
                DungeonManager.instance.currentDungeon.currentEncounter.ResetOrders();
                if (selectedCharacter != null) SelectOff();
            }
        }
    }
    public void UpdatePauseInput()
    {
       if (Input.GetKeyUp(KeyCode.Space)) DungeonManager.instance.raidMode = RaidMode.Combat;
    }
    public void UpdateGuildInput()
    {

    }

    private void OrderTarget()
    {
        DungeonManager.instance.currentDungeon.currentEncounter.selectedTarget = clickTile.OccupiedBy();
        DungeonManager.instance.currentDungeon.currentEncounter.TargetCharacter();
    }

    public void Attack(int x, DecisionState state)
    {
        if(selectedCharacter.ability.Count>x && !selectedCharacter.ability[x].passive)
        {
            if (selectedCharacter.ability[x].cooldownTimer > 0) Utility.instance.DamageNumber(selectedCharacter, "On Cooldown", SpriteList.instance.bad);
            else if (selectedCharacter.ability[x].energyRequired > selectedCharacter.mana) Utility.instance.DamageNumber(selectedCharacter, "Not Enough Mana", SpriteList.instance.bad);
            else selectedCharacter.state = state;
        }       
    }

    private void UpdateButtons() 
    {
        for (int i = 0; i < selectedCharacter.ability.Count; i++)
        {
            if (!selectedCharacter.ability[i].passive) UpdateButton(i);
            else Utility.instance.TurnOff(EncounterUI.instance.playerButton[i].gameObject);
        }
    }

    private void UpdateButton(int i)
    {
        Utility.instance.TurnOn(EncounterUI.instance.playerButton[i].gameObject);
        Utility.instance.TurnOn(EncounterUI.instance.playerButtonBackGround[i].gameObject);
        int x = Convert.ToInt32(selectedCharacter.ability[i].cooldownTime);
        if (x <= 30) EncounterUI.instance.timeBorder[i].sprite = EncounterUI.instance.timePic[x];
        else EncounterUI.instance.timeBorder[i].sprite = EncounterUI.instance.timePic[31];
        EncounterUI.instance.playerButton[i].GetComponent<Image>().sprite = EncounterUI.instance.playerButtonBackGround[i].sprite = selectedCharacter.ability[i].pic;
    }

    private void Select(Character currentCharacter)
    {
        //If who you selected is a player
        if (DungeonManager.instance.currentDungeon.currentEncounter.player.Contains(currentCharacter))
        {
            //If off cooldown
            if(EncounterUI.instance.currentEncounter.orderCooldownTimer <= 0)
            {
                //Select him to give orders
                DungeonManager.instance.currentDungeon.currentEncounter.Select(new List<Character> { currentCharacter }, Selected.SELECT);
            }           
        }
        //Otherwise No orders
        else DungeonManager.instance.currentDungeon.currentEncounter.ResetOrders();
        if (selectedCharacter != null && selectedCharacter.playerUI.outline.activeSelf) selectedCharacter.OutlineOff();
        selectedCharacter = currentCharacter;
        EncounterUI.instance.select = true;
        selectedCharacter.OutlineOn(0);
    }
    private void SelectOff()
    {
        if (selectedCharacter.playerUI.outline.activeSelf) selectedCharacter.OutlineOff();       
        selectedCharacter = null;        
        EncounterUI.instance.select = false;
        DungeonManager.instance.currentDungeon.currentEncounter.ResetOrders();
    }

    public void MousePosition()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (Tile t in DungeonManager.instance.currentDungeon.currentEncounter.tileList)
        {
            if (t.x == Mathf.RoundToInt(worldPosition.x) && t.y == Mathf.RoundToInt(worldPosition.y))
            {
                mouseTile = t;
                break;
            }
        }
    }

    private void MouseClickCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickTile = mouseTile;
            rightClickTile = null;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            clickTile = null;
            rightClickTile = mouseTile;
            if(control == Control.PlayerChoice)
            {
                if (DungeonManager.instance.currentDungeon.currentEncounter.selected == Selected.SELECT)
                {
                    DungeonManager.instance.currentDungeon.currentEncounter.selected = Selected.All;
                }
            }            
        }
    }

    private void SwitchControlType()
    {
        if (controlType == ControlType.WASD) controlType = ControlType.Mouse;
        else controlType = ControlType.WASD;
    }

    public void One() => controlledCharacter.state = DecisionState.Attack1;
    public void Two() => controlledCharacter.state = DecisionState.Attack2;
    public void Three() => controlledCharacter.state = DecisionState.Attack3;
    public void Four() => controlledCharacter.state = DecisionState.Attack4;
    public void Five() => controlledCharacter.state = DecisionState.Attack5;
    public void Six() => controlledCharacter.state = DecisionState.Attack6;



    private bool AvailableTile(int x, int y,List<Tile> occ)
    {       
        foreach(Tile t in controlledCharacter.move.currentTile.neighbor)
        {
            if (t.x == controlledCharacter.move.currentTile.x + x && t.y == controlledCharacter.move.currentTile.y + y && !occ.Contains(t)) return true;
        }
        return false;
    }

    private void Move(int x, int y, List<Tile> occ)
    {
        if (AvailableTile(x, y,occ))
        {
            MoveCast();
            controlledCharacter.action = $"Moving";
            Vector3 v = new Vector3(x, y).normalized;
            controlledCharacter.transform.Translate(v * Time.deltaTime * controlledCharacter.movement.value * MoveManager.instance.speed);
        }        
    }

    private void MoveCast()
    {
        foreach(Ability a in controlledCharacter.ability)
        {
            if(a.cast && !a.mobile)
            {
                a.cast = false;
                controlledCharacter.state = DecisionState.Downtime;
                break;
            }
        }
    }

    private void UserControlToggle(Character character)
    {
        if (!character.GetComponent<Boss>())
        {
            if (control == Control.PlayerChoice)
            {
                if (controlledCharacter == null)
                {
                    controlledCharacter = character;
                    Utility.instance.TurnOn(EncounterUI.instance.playerKeys);
                    character.TargetOff();
                    control = Control.UserControl;
                    clickTile = null;
                    character.OutlineOn(1);
                    rightClickTile = null;
                    Camera.main.orthographicSize = 7;
                }
            }
            else
            {
                Utility.instance.TurnOff(EncounterUI.instance.playerKeys);
                control = Control.PlayerChoice;
                controlledCharacter = null;
                if (character.target != null) character.target.OutlineOff();
                character.OutlineOff();
            }
        }        
    }

    internal void Reset()
    {
        selectedCharacter = null;
        controlledCharacter = null;
        clickTile = null;
        rightClickTile = null;
        mouseTile = null;
        EncounterUI.instance.select = false;
    }
}