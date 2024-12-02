using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip deathClip;
    [SerializeField]
    AudioClip shootClip;

    public void OnEnemyDeath()
    {
        audioSource.PlayOneShot(deathClip);
    }

    public void OnShoot()
    {
        audioSource.PlayOneShot(shootClip);
    }
}