using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCheck : MonoBehaviour
{
    public bool musicShouldPlay = true; //Whether a scene 
    public static int enemyCount; //Tracks the enemy count 

    private void Start()
    {
        SoundManager.InitialiseMusicPlayer();
        SoundManager.MusicVolumeChange(SoundManager.musicVolume);
        SoundManager.SoundEffectVolumeChange(SoundManager.soundVolume);
        SoundManager.EnemyVolumeChange(SoundManager.enemyVolume);
    }

    private void Update()
    {
        ///Here would be code to check the amount of enemies within the radius of the player, or the amount of
        ///enemies that would detect the player and this would trigger different music for the player
        ///For now it just changes based on the count of enemies
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (!SoundManager.musicPlayer.GetComponent<AudioSource>().isPlaying && musicShouldPlay)
        {
            if (enemyCount > 0)
            {
                SoundManager.PlayMusic(SoundManager.Music.Battle);
            }
            else
            {
                SoundManager.PlayMusic(SoundManager.Music.NonBattle);
            }
        }
        if (SoundManager.musicPlayer.GetComponent<AudioSource>().isPlaying)
        {
            if (enemyCount > 0 && SoundManager.musicPlayer.GetComponent<AudioSource>().clip.name != AudioAssets.instance.musicArray[1].audioClip.name)
            {
                SoundManager.PlayMusic(SoundManager.Music.Battle);
            }
            else if (SoundManager.musicPlayer.GetComponent<AudioSource>().clip.name != AudioAssets.instance.musicArray[2].audioClip.name && enemyCount == 0)
            { 
                SoundManager.PlayMusic(SoundManager.Music.NonBattle);
            }
        }
    }
}
