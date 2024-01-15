using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMP_Text[] soundLabels;
    [SerializeField] Slider[] soundSliders;

    private void Start()
    {
        soundSliders[0].value = Normalize(GetVolumeLevel("masterVolume"));
        soundLabels[0].text = Mathf.RoundToInt(Normalize(GetVolumeLevel("masterVolume")) * 100f).ToString() + " %";
        soundSliders[1].value = Normalize(GetVolumeLevel("musicVolume"));
        soundLabels[1].text = Mathf.RoundToInt(Normalize(GetVolumeLevel("musicVolume")) * 100f).ToString() + " %";
        soundSliders[2].value = Normalize(GetVolumeLevel("soundEffectsVolume"));
        soundLabels[2].text = Mathf.RoundToInt(Normalize(GetVolumeLevel("soundEffectsVolume")) * 100f).ToString() + " %";
    }

    private float GetVolumeLevel(string name)
    {
        float value;
        bool result = audioMixer.GetFloat(name, out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }

    public void ReturnPressed()
    {
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetMasterVolume(float volume)
    {
        soundLabels[0].text = Mathf.RoundToInt(volume * 100).ToString() + " %";
        AudioManager.instance.AdjustMasterVolume(Unnormalize(volume));
    }

    public void SetMusicVolume(float volume)
    {
        soundLabels[1].text = Mathf.RoundToInt(volume * 100).ToString() + " %";
        AudioManager.instance.AdjustMusicVolume(Unnormalize(volume));
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundLabels[2].text = Mathf.RoundToInt(volume * 100).ToString() + " %";
        AudioManager.instance.AdjustEffectsVolume(Unnormalize(volume));
    }

    private float Unnormalize(float volume)
    {
        if (volume != 0f)
        {
            return 20f * Mathf.Log(volume);
        } else
        {
            return -80f;
        }
    }

    private float Normalize(float volume)
    {
        if(volume != -80f)
        {
            return Mathf.Exp(volume / 20);
        }
        else
        {
            return 0;
        }
    }
}
