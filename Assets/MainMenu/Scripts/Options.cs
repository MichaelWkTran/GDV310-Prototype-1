using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
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
        if (musicSlider.value != SoundManager.musicVolume)
        {
            SoundManager.MusicVolumeChange(musicSlider.value);
        }
        if (soundSlider.value != SoundManager.soundVolume)
        {
            SoundManager.SoundEffectVolumeChange(soundSlider.value);
            SoundManager.PlayCheckSound(SoundManager.Sound.MenuOptionSelected);
        }
        if (enemySlider.value != SoundManager.enemyVolume)
        {
            SoundManager.EnemyVolumeChange(enemySlider.value);
            SoundManager.PlayCheckSound(SoundManager.Sound.EnemyDie);
        }
    }


}