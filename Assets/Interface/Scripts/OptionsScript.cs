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
        soundLabels[0].text = Mathf.RoundToInt(((soundSliders[0].value + 80f) / 0.8f)).ToString() + " %";
        soundSliders[1].value = GetVolumeLevel("musicVolume");
        soundLabels[1].text = Mathf.RoundToInt(((soundSliders[1].value + 80f) / 0.8f)).ToString() + " %";
        soundSliders[2].value = GetVolumeLevel("soundEffectsVolume");
        soundLabels[2].text = Mathf.RoundToInt(((soundSliders[2].value + 80f) / 0.8f)).ToString() + " %";
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
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        soundLabels[0].text = Mathf.RoundToInt(((volume + 80f) / 0.8f)).ToString() + " %";
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        soundLabels[1].text = Mathf.RoundToInt(((volume + 80f) / 0.8f)).ToString() + " %";
    }

    public void SetSoundEffectsVolume(float volume)
    {
        audioMixer.SetFloat("soundEffectsVolume", Mathf.Log10(volume) * 20);
        soundLabels[2].text = Mathf.RoundToInt(((volume + 80f) / 0.8f)).ToString() + " %";
    }
}
