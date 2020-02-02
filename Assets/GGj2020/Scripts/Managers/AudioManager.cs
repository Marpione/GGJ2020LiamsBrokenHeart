using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource SemiDarkSource;
    public AudioSource DarkSource;


    public void SetMood(float SemiDarkMoodVolume, float DarkMoodVolume)
    {
        SemiDarkSource.DOFade(SemiDarkMoodVolume, 15f);
        DarkSource.DOFade(DarkMoodVolume, 15f);
    }
}
