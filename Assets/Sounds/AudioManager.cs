using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public static AudioManager instance;

    void Awake()
    {
        if (AudioManager.instance == null)
        {
            DontDestroyOnLoad(gameObject);
            AudioManager.instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        //Get the saved music volume, standard = 10f
        float master = PlayerPrefs.GetFloat("masterVolume", 0f);
        float music = PlayerPrefs.GetFloat("musicVolume", 0f);
        float effects = PlayerPrefs.GetFloat("soundEffectsVolume", 0f);

        //Set the music volume to the saved volume
        AdjustMasterVolume(master);
        AdjustMusicVolume(music);
        AdjustEffectsVolume(effects);
    }

    public void AdjustMasterVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("masterVolume", volume);

        //Update PlayerPrefs "Master"
        PlayerPrefs.SetFloat("masterVolume", volume);

        //Save changes
        PlayerPrefs.Save();
    }

    public void AdjustMusicVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("musicVolume", volume);

        //Update PlayerPrefs "Music"
        PlayerPrefs.SetFloat("musicVolume", volume);

        //Save changes
        PlayerPrefs.Save();
    }

    public void AdjustEffectsVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("soundEffectsVolume", volume);

        //Update PlayerPrefs "Effects"
        PlayerPrefs.SetFloat("soundEffectsVolume", volume);

        //Save changes
        PlayerPrefs.Save();
    }
}
