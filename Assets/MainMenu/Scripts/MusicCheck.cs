using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCheck : MonoBehaviour
{

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
        if (!SoundManager.musicPlayer.GetComponent<AudioSource>().isPlaying)
        {
            SoundManager.PlayMusic(SoundManager.Music.NonBattle);
        }
    }
}
