using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    /// <summary>
    /// Stores the Audio Assets so that the
    /// </summary>
    private static AudioAssets _instance;
    public static AudioAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<AudioAssets>("AudioAssets"));
            }
            return _instance;
        }
    }
    public SoundClass[] soundsArray;
    public SoundClass[] musicArray;

    [System.Serializable]
    public class SoundClass
    {
        public string name;
        public AudioClip audioClip;
        public SoundManager.Sound sound;
        public SoundManager.SoundType soundType;

        [Range(0f, 1f)]
        public float volume = 1f;
        public float length;
        public bool looped;
        public AudioSource source;

        public void SoundGenerated(AudioSource _soundSource)
        {
            _soundSource.clip = audioClip;
            _soundSource.volume = volume;
            _soundSource.loop = looped;
            length = audioClip.length;
        }
        //public bool hasCooldown;
    }

}
