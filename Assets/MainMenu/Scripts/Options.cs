using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    /// <summary>
    /// Class for the options menu 
    /// </summary>
    public Slider musicSlider; 
    public Slider soundSlider;
    public Slider enemySlider;

    private void Start()
    {
        musicSlider.value = SoundManager.musicVolume;
        soundSlider.value = SoundManager.soundVolume;
        enemySlider.value = SoundManager.enemyVolume;
    }

    private void Update()
    {
        // Updates the volumes for the different kinds of sounds when the slider is changed 
        if (musicSlider.value != SoundManager.musicVolume)
        {
            SoundManager.MusicVolumeChange(musicSlider.value);
        }
        if (soundSlider.value != SoundManager.soundVolume)
        {
            SoundManager.SoundEffectVolumeChange(soundSlider.value);
            if (!GetComponent<AudioSource>())
            {
                SoundManager.PlaySound(SoundManager.Sound.MenuOptionSelected, gameObject);
            }
            else
            {
                GameObject.Destroy(GetComponent<AudioSource>());
                SoundManager.PlaySound(SoundManager.Sound.MenuOptionSelected, gameObject);
            }
        }
        if (enemySlider.value != SoundManager.enemyVolume)
        {
            SoundManager.EnemyVolumeChange(enemySlider.value);
            if (!GetComponent<AudioSource>())
            {
                SoundManager.PlaySound(SoundManager.Sound.EnemyDie, gameObject);
            }
            else
            {
                GameObject.Destroy(GetComponent<AudioSource>());
                SoundManager.PlaySound(SoundManager.Sound.EnemyDie, gameObject);
            }
        }
        // 
        AudioSource source;
        if (source = GetComponent<AudioSource>())
        {
            source.ignoreListenerPause = true;
        }
    }
}
