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
        soundSliders[0].value = GetVolumeLevel("masterVolume");
        soundLabels[0].text = Mathf.RoundToInt(((GetVolumeLevel("masterVolume") + 80f) / 0.8f)).ToString() + " %";
        soundSliders[1].value = GetVolumeLevel("musicVolume");
        soundLabels[1].text = Mathf.RoundToInt(((GetVolumeLevel("musicVolume") + 80f) / 0.8f)).ToString() + " %";
        soundSliders[2].value = GetVolumeLevel("soundEffectsVolume");
        soundLabels[2].text = Mathf.RoundToInt(((GetVolumeLevel("soundEffectsVolume") + 80f) / 0.8f)).ToString() + " %";
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
        soundLabels[0].text = Mathf.RoundToInt(((volume + 80f) / 0.8f)).ToString() + " %";
        AudioManager.instance.AdjustMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        soundLabels[1].text = Mathf.RoundToInt(((volume + 80f) / 0.8f)).ToString() + " %";
        AudioManager.instance.AdjustMusicVolume(volume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundLabels[2].text = Mathf.RoundToInt(((volume + 80f) / 0.8f)).ToString() + " %";
        AudioManager.instance.AdjustEffectsVolume(volume);
    }
}
