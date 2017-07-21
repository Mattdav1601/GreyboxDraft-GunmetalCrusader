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

    public AudioClip _TenSecondsLeft;
    public AudioClip _RoundStarted;
    public AudioClip _RoundEnded;
    public AudioClip _WeaponEmpty;

    public AudioClip _BoostingRockets;


   


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Copilot voice sounds.
    public void RoundStarted()
    {

    }

    public void RoundEnded()
    {

    }

    public void TenSecondsLeft()
    {

    }

    public void WeaponEmpty()
    {

    }

    //Mech sounds

    void StartedBoosting()
    {

    }


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
        Music.Stop();

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

    //public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    //{
    //    float startVolume = audioSource.volume;

    //    while (audioSource.volume > 0)
    //    {
    //        audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

    //        yield return null;
    //    }

    //    audioSource.Stop();
    //    audioSource.volume = startVolume;
    //}




}
