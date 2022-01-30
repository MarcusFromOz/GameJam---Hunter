using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetAudioParameter : MonoBehaviour
{
    public AudioMixer masterMixer;


    public void SetSfxLevel(float sfxLvl)
    {

        masterMixer.SetFloat("SFXVolume", sfxLvl);

    }

    public void SetMusicLevel(float musicLvl)
    {

        masterMixer.SetFloat("MusicVolume", musicLvl);

    }

    public void SetDialogLevel(float dialogLvl)
    {

        masterMixer.SetFloat("DialogVolume", dialogLvl);

    }

}

