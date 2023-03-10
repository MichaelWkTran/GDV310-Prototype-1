using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCheck : MonoBehaviour
{
    public bool musicShouldPlay = true;
    private void Start()
    {
        SoundManager.InitialiseMusicPlayer();
        SoundManager.MusicVolumeChange(SoundManager.musicVolume);
        SoundManager.SoundEffectVolumeChange(SoundManager.soundVolume);
        SoundManager.EnemyVolumeChange(SoundManager.enemyVolume);
    }

    private void Update()
    {
        //Here would be code to check the amount of enemies within the radius of the player, or the amount of
        //enemies that would detect the player and this would trigger different music for the player
        if (!SoundManager.musicPlayer.GetComponent<AudioSource>().isPlaying && musicShouldPlay)
        {
            SoundManager.PlayMusic(SoundManager.Music.NonBattle);
        }
    }
}
