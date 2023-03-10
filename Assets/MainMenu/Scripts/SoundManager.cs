using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    ///Remind me that pausing game should stop all sound but music 
    ///Also remind me that looping sounds might be needed 
    /// <summary>
    /// Sound Manager handles the playing of all sounds in the game, 
    /// Global sound values stored here too
    /// </summary>

    //Volumes
    public static float musicVolume = 1f;
    public static float soundVolume = 1f;
    public static float enemyVolume = 1f;
    
    public static GameObject musicPlayer;
    public static bool volumeUpdated = false;


    public enum Sound //All the different versions of the sounds
    {
        //Sound Effect sounds
        Footsteps,
        NormalAttack,
        SpecialAttack,
        Dash,
        
        //Voice sounds
        EnemyHit,
        EnemyDie,
        PlayerDie,

        //Non-Diagetic Sounds
        MenuOptionSelected,
        Music, //At the bottom because it's not used in the sounds array 
    }

    public enum Music
    {
        Menu,
        Battle,
        NonBattle,
    }

    public enum SoundType
    {
        Music,
        SoundEffect,
        Enemy,
    }

    public static void MusicVolumeChange(float newValue)
    {
        musicVolume = newValue;
        for (int i = 0; i < AudioAssets.instance.musicArray.Length; i++)
        {
            AudioAssets.instance.musicArray[i].volume = musicVolume;
        }
        musicPlayer.GetComponent<AudioSource>().volume = musicVolume;
    }

    public static void SoundEffectVolumeChange(float newValue)
    {
        volumeUpdated = true;
        soundVolume = newValue;
        for (int i = 0; i < AudioAssets.instance.soundsArray.Length; i++)
        {
            if (AudioAssets.instance.soundsArray[i].soundType == SoundType.SoundEffect)
                AudioAssets.instance.soundsArray[i].volume = soundVolume;
        }
    }

    public static void EnemyVolumeChange(float newValue)
    {
        volumeUpdated = true;
        enemyVolume = newValue;
        for (int i = 0; i < AudioAssets.instance.soundsArray.Length; i++)
        {
            if (AudioAssets.instance.soundsArray[i].soundType == SoundType.Enemy)
            {
                AudioAssets.instance.soundsArray[i].volume = enemyVolume;
            }
        }
    }
    /// <summary>
    /// Ensures that any scene the player is loaded in has a music player
    /// </summary>
    public static void InitialiseMusicPlayer()
    {
        AudioSource audioSource;
        if (!(musicPlayer = GameObject.Find("MusicPlayer")))
        {
            musicPlayer = new GameObject("MusicPlayer");
            audioSource = musicPlayer.AddComponent<AudioSource>();
            audioSource.volume = musicVolume;
        }
        else if (!musicPlayer.GetComponent<AudioSource>())
        {
            audioSource = musicPlayer.GetComponent<AudioSource>();
            audioSource.volume = musicVolume;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="music"></param>
    public static void PlayMusic(Music music)
    {
        if ((AudioAssets.instance.musicArray.Length > (int)music) && ((int)music >= 0)) //Checks if the music exists 
        {
           AudioSource musicSource = musicPlayer.GetComponent<AudioSource>();
           AudioAssets.instance.musicArray[(int)music].SoundGenerated(musicSource);
           musicSource.ignoreListenerPause = true; 
           musicSource.Play();
        }
        else
        {
            Debug.LogError("Music " + music + " can't be found");
        }
    }

    public static void PlayLoopingSound()
    {

    }

    /// <summary>
    /// Play a sound without a specific location, mostly for UI + non-diagetic sounds 
    /// </summary>
    /// <param name="sound"></param>
    public static void PlaySound(Sound sound, GameObject sourceObj)
    {
        if (!sourceObj.GetComponent<AudioSource>())
        {
            sourceObj.AddComponent<AudioSource>();
        }
        if (AudioAssets.instance.soundsArray.Length > (int)sound && (int)sound >= 0) //Checks if the sound the system is trying to use is stored in the audio assets 
        {
            AudioSource audioSource = sourceObj.AddComponent<AudioSource>();
            AudioAssets.instance.soundsArray[(int)sound].SoundGenerated(audioSource);
            Debug.Log(audioSource.volume);
            audioSource.PlayOneShot(audioSource.clip);
            GameObject.Destroy(audioSource, AudioAssets.instance.soundsArray[(int)sound].length);
        }
    }

    /// <summary>
    /// Plays a sound for checking audio levels
    /// </summary>
    /// <param name="sound"></param>
    //public static void PlayCheckSound(Sound sound)
    //{
    //    if (AudioAssets.instance.soundsArray.Length > (int)sound && (int)sound >= 0) //Checks if the sound the system is trying to use is stored in the audio assets 
    //    {
    //        AudioSource audioSource = musicPlayer.AddComponent<AudioSource>();
    //        AudioAssets.instance.soundsArray[(int)sound].SoundGenerated(audioSource);
    //        Debug.Log(audioSource.volume);
    //        audioSource.PlayOneShot(audioSource.clip);
    //        GameObject.Destroy(audioSource, AudioAssets.instance.soundsArray[(int)sound].length);
    //    }
    //}
    /// <summary>
    /// Play a sound with a specific location
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="position"></param>
    public static void Play3DSound(Sound sound, GameObject _sourceObject)
    {
        if (!_sourceObject.GetComponent<AudioSource>())
        {
            AudioSource audioSource = _sourceObject.AddComponent<AudioSource>();
            GameObject.Destroy(audioSource, AudioAssets.instance.soundsArray[(int)sound].length);
        }
        if (AudioAssets.instance.soundsArray.Length > (int)sound && (int)sound >= 0) //Checks if the sound the system is trying to use is stored in the audio assets 
        {
            AudioSource audioSource = _sourceObject.GetComponent<AudioSource>();
            AudioAssets.instance.soundsArray[(int)sound].SoundGenerated(audioSource);
            // audioSource.clip = AudioAssets.instance.soundsArray[(int)sound].audioClip;
            //
            // if (AudioAssets.instance.soundsArray[(int)sound].soundType == SoundType.Enemy) 
            //     audioSource.volume = enemyVolume;
            // else
            //     audioSource.volume = soundVolume;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Sound " + sound + " can't be found");
        }
    }
}


//    public void Stop(string name)
//    {
//        Sound sound = Array.Find(sounds, s => s.name == name);

//        if (sound == null)
//        {
//            Debug.LogError("Sound " + name + " Not Found!");
//            return;
//        }

//        sound.source.Stop();
//    }

//    private static bool CanPlaySound(Sound sound)
//    {
//        if (soundTimerDictionary.ContainsKey(sound.name))
//        {
//            float lastTimePlayed = soundTimerDictionary[sound.name];

//            if (lastTimePlayed + sound.clip.length < Time.time)
//            {
//                soundTimerDictionary[sound.name] = Time.time;
//                return true;
//            }

//            return false;
//        }

//        return true;
//    }
//}
