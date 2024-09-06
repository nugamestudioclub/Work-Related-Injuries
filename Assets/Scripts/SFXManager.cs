using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioClip saw;
    public AudioClip fall;
    public AudioClip playerRespawn;
    public AudioClip boxDeposit;
    public AudioClip playerPickup;
    public AudioClip playerPlace;
    public AudioClip playerThrow;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Saw()
    {
        PlaySound(saw, 0.1f);
    }

    public void Fall()
    {
        PlaySound(fall, 0.3f);
    }

    public void PlayerRespawn()
    {
        PlaySound(playerRespawn, 0.3f);
    }

    public void BoxDeposit()
    {
        PlaySound(boxDeposit, 0.6f);
    }

    public void PlayerPickup()
    {
        PlaySound(playerPickup, 0.3f);
    }

    public void PlayerPlace()
    {
        PlaySound(playerPlace, 0.3f);
    }

    public void PlayerThrow()
    {
        PlaySound(playerThrow, 1f);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}
