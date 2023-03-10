using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSoundcheck : MonoBehaviour
{
    public bool enemyTestSounds = false;
    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AudioSource>())
        {
            if (!enemyTestSounds)
            {
                SoundManager.Play3DSound(SoundManager.Sound.PlayerDie, gameObject);
            }
            else
                SoundManager.Play3DSound(SoundManager.Sound.EnemyDie, gameObject);
        }
    }
}
