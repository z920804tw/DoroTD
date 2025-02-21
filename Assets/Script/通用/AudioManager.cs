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

    [Header("關卡音效設定")]
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float transitionDuration;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TransitionMusic(0, 0, 0.5f));
        LoadAudioVolume();
    }
    public void SetMainVolume()
    {
        float volume = mainVolumeSlider.value;
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
        mainVolumeText.text = $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("MainVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        musicVolumeText.text = $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetGunVolume()
    {
        float volume = gunSoundSlider.value;
        audioMixer.SetFloat("GunVolume", Mathf.Log10(volume) * 20);
        gunVolumeText.text = $"{Mathf.RoundToInt(volume * 100)}";
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

    public IEnumerator TransitionMusic(int i, float start, float end)
    {
        float timer = 0;
        //前次音樂退場
        if (audioSource.clip != audioClips[i])
        {
            while (timer < transitionDuration)
            {
                timer += Time.deltaTime;
                float t = timer / transitionDuration;
                audioSource.volume = Mathf.Lerp(end, start, t);
                yield return null;
            }
            audioSource.clip = audioClips[i];
            Debug.Log("切換完成");
        }

        //目標音樂的進場
        timer = 0;
        audioSource.Play();
        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;

            float t = timer / transitionDuration;
            audioSource.volume = Mathf.Lerp(start, end, t);
            yield return null;
        }


    }
}
