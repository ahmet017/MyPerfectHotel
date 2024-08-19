using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource, bgAudioSource;
    bool soundToggle = true;
    public Sprite audioOn, audioOff;
    public Image audioBtnImage;

    public void Play(string name)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);

        if(audioSource.clip != clip)
        audioSource.clip = clip;

        audioSource.Play();
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Sound"))
            SoundToggle();
    }

    public void SoundToggle()
    {
        Play("Click");

        soundToggle = !soundToggle;
        if (soundToggle)
        {
            audioBtnImage.sprite = audioOn;
            audioSource.mute = false;
            bgAudioSource.mute = false;
            PlayerPrefs.DeleteKey("Sound");
        }
        else
        {
            audioBtnImage.sprite = audioOff;
            audioSource.mute = true;
            bgAudioSource.mute = true;
            PlayerPrefs.SetString("Sound", "");
        }
    }
}
