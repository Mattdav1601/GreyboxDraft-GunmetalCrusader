using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    public AudioClip[] Songs;

    public AudioSource Music;

    public AudioSource CoPilot;

    public AudioSource MechEffects;
    public AudioSource LowPriorityMechEffects;

    //CoPilot sounds
    public AudioClip _TenSecondsLeft;
    public AudioClip _RoundStarted;
    public AudioClip _RoundEnded;
    public AudioClip _WeaponEmpty;
    public AudioClip _GameStarted;
    public AudioClip _BoostingRocketsEngaged;
    public AudioClip _WelcomePilot;

    //mech sounds

   


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

       

    public void EndBoosting()
    {

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
