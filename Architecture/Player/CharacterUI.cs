using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour
{
    public SpriteRenderer head;
    public SpriteRenderer chest;
    public SpriteRenderer legs;
    public SpriteRenderer feet;
    public SpriteRenderer weapon;
    public SpriteRenderer offHand;
    public GameObject outline;
    public GameObject nameObject;
    public Text characterNameText;
    public GameObject shield;


    private void Awake()
    {
        characterNameText = GetComponentInChildren<Text>();
    }
}
