using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    AudioMixer master;
    [SerializeField]
    PlayerControl playerControl;

    private void Awake()
    {
        SetSensitivity(playerControl.sensitivity);
    }

    public void SetMaster(float value)
    {
        master.SetFloat("MasterVolume", value);
    }

    public void SetEffects(float value)
    {
        master.SetFloat("EffectsVolume", value);
    }

    public void SetMusic(float value)
    {
        master.SetFloat("MusicVolume", value);
    }

    public void SetSensitivity(float value)
    {
        playerControl.sensitivity = value;
    }
}
