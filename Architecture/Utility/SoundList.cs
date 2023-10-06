using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    public static SoundList instance;
    [Header("Music")]
    public AudioClip creditIntroTheme;
    public AudioClip vanillaMenuTheme;
    public AudioClip beyondMenuTheme;
    public AudioClip undeadMenuTheme;
    [Header("Effects")]
    public AudioClip mouseClick;
    [Header("Classes")]
    [Header("Mage SFX")]    
    public List<AudioClip> explosiveMissiles;
    public AudioClip clawsFromTheDeep;

    [Header("Bosses")]
    [Header("Vanilla")]
    [Header("Monastery of the Midnight Order")]
    [Header("PALADIN OF CERES SOUND")]
    public AudioClip paladinofCeresEncounterTheme;    
    public AudioClip cleansingFlame;
    public List<AudioClip> fireBlast;


    private void Awake()
    {
        instance = this;
        InstantiateSound();        
    }

    private void InstantiateSound()
    {
        mouseClick = SoundManager.instance.InstantiateSound(mouseClick);
    }

    
    public AudioClip CurrentMenuTheme() => DungeonManager.instance.expansion == Expansion.Vanilla ? vanillaMenuTheme : DungeonManager.instance.expansion == Expansion.Undead ? undeadMenuTheme : beyondMenuTheme;
        
}
