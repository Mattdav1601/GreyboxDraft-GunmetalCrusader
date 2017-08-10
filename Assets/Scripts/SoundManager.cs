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
    public AudioClip[] _TenSecondsLeft;
    public AudioClip[] _RoundStarted;
    public AudioClip[] _RoundAlmostStarted;
    public AudioClip[] _RoundEnded;
    public AudioClip[] _DropPodIncoming;
    public AudioClip[] _StockUpHint;
    public AudioClip[] _PlayeResupplied;

        // weapons related
   
    public AudioClip[] _Reloading;
    public AudioClip[] _WeaponReloaded;

    public AudioClip[] _LeftWeaponEmpty;
    public AudioClip[] _RightWeaponEmpty;
    public AudioClip[] _BothWeaponEmpty;
    public AudioClip[] _TriedFiringWhileWeaponEmpty;

    public AudioClip[] _AllWeaponsOutOfAmmo;

        // booster related
    public AudioClip[] _BoostingRocketsEngaged;
    public AudioClip[] _InvalidLocation;
    public AudioClip[] _BoostCancelled;

        // Health related
    public AudioClip[] _TakingDamage;

        //startup
    public AudioClip[] _WelcomePilot;

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
        EventManager.instance.OnRoundStart.AddListener((p) => {
            RoundStarted();
        });

        EventManager.instance.OnRoundEnd.AddListener(RoundEnded);
    }

   


    //
    //
    //
    //Copilot voice sounds.

    void Welcome()
    {
        CoPilot.PlayOneShot(_WelcomePilot[Random.Range(0, _WelcomePilot.Length)]);
    }

    //CoPilot round indicator
    public void AlmostStarted()
    {
        CoPilot.PlayOneShot(_RoundAlmostStarted[Random.Range(0, _RoundAlmostStarted.Length)]);
    }
    public void RoundStarted()
    {
        CoPilot.PlayOneShot(_RoundStarted[Random.Range(0, _RoundStarted.Length)]);
    }

    public void RoundEnded()
    {
        Invoke("ActualRoundEnded", 3);
    }

    void ActualRoundEnded()
    {
        CoPilot.PlayOneShot(_RoundEnded[Random.Range(0, _RoundEnded.Length)]);
    }

    public void TenSecondsLeft()
    {
        CoPilot.PlayOneShot(_TenSecondsLeft[Random.Range(0, _TenSecondsLeft.Length)]);
    }

    public void DropPodIncoming()
    {
        CoPilot.PlayOneShot(_DropPodIncoming[Random.Range(0, _DropPodIncoming.Length)]);
    }


    public void WeShouldStockUp()
    {
        CoPilot.PlayOneShot(_StockUpHint[Random.Range(0, _StockUpHint.Length)]);
    }

    public void ReSupplied()
    {
        CoPilot.PlayOneShot(_PlayeResupplied[Random.Range(0, _PlayeResupplied.Length)]);
    }

   //booster CoPilot
    public void BoosterEngaged()
    {
        CoPilot.PlayOneShot(_BoostingRocketsEngaged[Random.Range(0,_BoostingRocketsEngaged.Length)]);
    }

    public void CoordinatesInvalid()
    {
        CoPilot.PlayOneShot(_InvalidLocation[Random.Range(0, _InvalidLocation.Length)]);
    }

    public void BoostCancelled()
    {
        CoPilot.PlayOneShot(_BoostCancelled[Random.Range(0, _BoostCancelled.Length)]);
    }

    // weapon reloading copilot
    public void LeftWeaponEmpty()
    {
        CoPilot.PlayOneShot(_LeftWeaponEmpty[Random.Range(0, _LeftWeaponEmpty.Length)]);
    }

    public void RightWeaponEmpty()
    {
        CoPilot.PlayOneShot(_RightWeaponEmpty[Random.Range(0, _RightWeaponEmpty.Length)]);
    }

    public void BothWeaponsEmpty()
    {
        CoPilot.PlayOneShot(_BothWeaponEmpty[Random.Range(0, _BothWeaponEmpty.Length)]);
    }

    public void FailedFireEmptyWeapon()
    {
        CoPilot.PlayOneShot(_TriedFiringWhileWeaponEmpty[Random.Range(0, _TriedFiringWhileWeaponEmpty.Length)]);
    }

    public void Reloading()
    {
        CoPilot.PlayOneShot(_Reloading[Random.Range(0, _Reloading.Length)]);
    }

    public void ReloadFinished()
    {
        CoPilot.PlayOneShot(_WeaponReloaded[Random.Range(0, _WeaponReloaded.Length)]);
    }

   
    //health damage copilot warnings

    public void TakingDamage()
    {
        CoPilot.PlayOneShot(_TakingDamage[Random.Range(0, _TakingDamage.Length)]);
    }




    //
    //
    //
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







    //
    //
    //
    // Weapon Sounds (firing)

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
    




 
    //
    //
    //
    //android sounds

    public void AndroidDie(AudioSource source)
    {
        int n = Random.Range(0, _AndroidDie.Length);

        source.PlayOneShot(_AndroidDie[n]);
    }



    //
    //
    //
    // SoundTrack
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

    }


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
