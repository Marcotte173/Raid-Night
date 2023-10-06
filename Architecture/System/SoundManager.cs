using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public List<AudioSource> effect;
    public AudioSource music;
    
    private void Awake()
    {
        instance = this;
    }
    public void PlayEffect(AudioClip sound) => PlayEffect(sound, false);
    public void PlayEffect(AudioClip sound, AudioClip off)=> PlayEffect(sound, off, false);

    public void PlayEffect(AudioClip sound,bool loop)
    {
        foreach (AudioSource a in effect)
        {
            if (!a.isPlaying)
            {
                a.loop = loop;
                a.clip = sound;
                a.Play();
                break;
            }
        }
    }
    public AudioClip InstantiateSound(AudioClip a)
    {
        AudioClip au = Instantiate(a, transform);
        au.name = a.name;
        return au;
    }

    public void StopEffect(AudioClip audioClip)
    {
        foreach (AudioSource a in effect)
        {
            //If it is, put sound sound in that audiosource
            if (a.clip == audioClip && a.isPlaying)
            {
                a.Stop();
                break;
            }
        }
    }

    public void PlayEffect(AudioClip sound, AudioClip off,bool loop)
    {
        //Try to find first part of effect, if it's there
        bool haveSource = false;
        foreach(AudioSource a in effect)
        {
            //If it is, put sound sound in that audiosource
            if(a.clip == off&& a.isPlaying)
            {
                a.loop = loop;
                a.clip = sound;
                a.Play();
                haveSource = true;
                break;
            }
        }
        //If not, find one
        if (!haveSource)
        {
            foreach (AudioSource a in effect)
            {
                if (!a.isPlaying)
                {
                    a.clip = sound;
                    a.Play();
                    break;
                }
            }
        }
    }
    public void PlayMusic(AudioClip sound)
    {
        if (music.clip != sound || !music.isPlaying)
        {
            music.loop = true;
            music.clip = sound;
            music.Play();
        }
       
    }
}
