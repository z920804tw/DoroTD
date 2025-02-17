using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audioMixer;

    [Header("Slider設定")]
    [SerializeField] Slider mainVolumeSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider gunSoundSlider;

    [Header("音量文字設定")]

    [SerializeField] TMP_Text mainVolumeText;
    [SerializeField] TMP_Text musicVolumeText;
    [SerializeField] TMP_Text gunVolumeText;

    void Start()
    {
        LoadAudioVolume();
    }
    public void SetMainVolume()
    {
        float volume = mainVolumeSlider.value;
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
        mainVolumeText.text= $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("MainVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        musicVolumeText.text= $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetGunVolume()
    {
        float volume = gunSoundSlider.value;
        audioMixer.SetFloat("GunVolume", Mathf.Log10(volume) * 20);
        gunVolumeText.text= $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("GunVolume", volume);
    }

    public void LoadAudioVolume()
    {
        mainVolumeSlider.value = PlayerPrefs.GetFloat("MainVolume");
        SetMainVolume();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume();
        gunSoundSlider.value = PlayerPrefs.GetFloat("GunVolume");
        SetGunVolume();
    }
}
