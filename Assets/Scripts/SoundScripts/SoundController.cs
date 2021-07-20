using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{

    public AudioClip walkSound;
    public AudioClip eatSound;
    public AudioClip levelupSound;
    public AudioClip deathSound;
    // Use this for initialization
    public void PlayWalk()
    {
        AudioSource.PlayClipAtPoint(walkSound, transform.position);
    }

    public void PlayEat()
    {
        AudioSource.PlayClipAtPoint(eatSound, transform.position);
    }

    public void PlayLvlup()
    {
        AudioSource.PlayClipAtPoint(levelupSound, transform.position);
    }

    public void PlayDead()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }

}