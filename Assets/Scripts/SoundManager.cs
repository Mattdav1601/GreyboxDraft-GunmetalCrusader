using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    public AudioClip[] Songs;

    public AudioSource Music;

    public AudioSource CoPilot;

    public AudioSource MechEffects;

    public AudioSource RightWepEffects;

    public AudioSource LeftWepEffects;

    //CoPilot sounds

        // round related
    public AudioClip _TenSecondsLeft;
    public AudioClip _RoundStarted;
    public AudioClip _RoundEnded;

        // weapons related
    public AudioClip _WeaponEmpty;
    public AudioClip _Reloading;
    public AudioClip _WeaponReloaded;

    public AudioClip _WeaponOutOfAmmo;
    public AudioClip _AllWeaponsOutOfAmmo;

        //booster related
    public AudioClip _GameStarted;
    public AudioClip _BoostingRocketsEngaged;

        //startup
    public AudioClip _WelcomePilot;

        // Warnings

    //mech sounds

    public AudioClip _HitGround;
    public AudioClip _BoostingLoop;
    public AudioClip _TookDamage;
    public AudioClip _Turning;

    //weapon sounds
    public AudioClip _GrenadeFire;
    public AudioClip _SMGFire;
    public AudioClip _GrenadeExplode;

    //android sounds
    public AudioClip[] _AndroidDie;

    void Start()
    {
        Invoke("Welcome", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Copilot voice sounds.

    void Welcome()
    {
        CoPilot.PlayOneShot(_WelcomePilot);
    }

    public void RoundStarted()
    {
        CoPilot.PlayOneShot(_RoundStarted);
    }

    public void RoundEnded()
    {
        Invoke("ActualRoundEnded", 3);
    }

    void ActualRoundEnded()
    {
        CoPilot.PlayOneShot(_RoundEnded);
    }

    public void TenSecondsLeft()
    {
        CoPilot.PlayOneShot(_TenSecondsLeft);
    }

    public void WeaponEmpty()
    {
        CoPilot.PlayOneShot(_WeaponEmpty);
    }

    public void BoosterEngaged()
    {
        CoPilot.PlayOneShot(_BoostingRocketsEngaged);
    }

    //Mech sounds

    public void Boosting()
    {
        MechEffects.loop = true;
        MechEffects.clip = _BoostingLoop;
        MechEffects.Play();
    }

    public void EndBoostSound()
    {
        MechEffects.loop = false;
        MechEffects.Stop();
    }

    public void EndBoosting()
    {
        
        MechEffects.PlayOneShot(_HitGround);
    }

    public void TookDamage()
    {
        MechEffects.PlayOneShot(_TookDamage);
    }


    // Weapon Sounds

    public void FiredGrenade()
    {
        RightWepEffects.PlayOneShot(_GrenadeFire);
    }

    public void SMGFIre()
    {
        LeftWepEffects.PlayOneShot(_SMGFire);
    }

    public void GrenadeExplode(AudioSource source)
    {
        source.PlayOneShot(_GrenadeExplode);
    }


    //android sounds

    public void AndroidDie(AudioSource source)
    {
        int n = Random.Range(0, _AndroidDie.Length);

        source.PlayOneShot(_AndroidDie[n]);
    }


    public void StartSoundTrack()
    {

               // StopCoroutine("FadeOut");
                Music.clip = Songs[Random.Range(0, Songs.Length)];
                Music.Play();

    }


    public void EndSoundTrack()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(Music, 4));
       // Music.Stop();

    }

    //public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    //{
    //    audioSource.Play();
    //    float startVolume = audioSource.volume;

    //    while (audioSource.volume < 1)
    //    {
    //        audioSource.volume += startVolume * Time.deltaTime / FadeTime;

    //        yield return null;
    //    }
      
    //    audioSource.volume = startVolume;
    //}

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }




}
