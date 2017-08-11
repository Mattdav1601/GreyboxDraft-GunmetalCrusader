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
    public AudioClip[] _NoBackupAmmunition;

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

        //Added OnRoundStartWarning listener
        EventManager.instance.OnRoundStartWarning.AddListener((i)=> {
            RoundStartWarning(i);
        });

        //Added OnRoundEndWarning listener
        EventManager.instance.OnRoundEndWarning.AddListener((b) => {
            RoundEndWarning(b);
        });

        EventManager.instance.OnOutOfAmmo.AddListener((i) => {
            OutOfAmmoWarning(i);
        });

        EventManager.instance.OnReloadAttempt.AddListener((b)=>{
            ReloadWarning(b);
        });
    }

    


  
    //Copilot voice sounds.

    void Welcome()
    {
        CoPilot.PlayOneShot(_WelcomePilot[Random.Range(0, _WelcomePilot.Length)]);
    }

    //CoPilot round indicator
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

    //Called whenever the player should know the round is about to start
    void RoundStartWarning(int clip){

        //If the warning is the ten second warning play ten second sound
        if(clip == 0)
            CoPilot.PlayOneShot(_TenSecondsLeft[Random.Range(0, _TenSecondsLeft.Length)]);
        else if(clip == 1)
        {
            //Play round about to start sound
            CoPilot.PlayOneShot(_RoundAlmostStarted[Random.Range(0, _RoundAlmostStarted.Length)]);
        }
    }

    //Called whenever the player should know the round is about to end
    void RoundEndWarning(bool b)
    {
        if (!b)
        {
            //Play first warning clip here
            CoPilot.PlayOneShot(_DropPodIncoming[Random.Range(0, _DropPodIncoming.Length)]);
        } else if (b)
        {
            //Play second warning clip here
            CoPilot.PlayOneShot(_StockUpHint[Random.Range(0, _StockUpHint.Length)]);
        }
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

  
    void OutOfAmmoWarning(int i)
    {
        switch (i)
        {
            case 0:
                //Left ammo warning
                CoPilot.PlayOneShot(_LeftWeaponEmpty[Random.Range(0, _LeftWeaponEmpty.Length)]);

                break;
            case 1:
                //Right ammo warning
                CoPilot.PlayOneShot(_RightWeaponEmpty[Random.Range(0, _RightWeaponEmpty.Length)]);

                break;
            case 2:
                //both ammo warning
                CoPilot.PlayOneShot(_BothWeaponEmpty[Random.Range(0, _BothWeaponEmpty.Length)]);
                break;
        }
    }

    public void FailedFireEmptyWeapon()
    {
        CoPilot.PlayOneShot(_TriedFiringWhileWeaponEmpty[Random.Range(0, _TriedFiringWhileWeaponEmpty.Length)]);
    }

    

    public void ReloadFinished()
    {
        CoPilot.PlayOneShot(_WeaponReloaded[Random.Range(0, _WeaponReloaded.Length)]);
    }

    //When the player tries to reload. True is for when the player is able to, false is unable
    void ReloadWarning(bool b)
    {
        if (b)
        {
            //Play can reload
            CoPilot.PlayOneShot(_Reloading[Random.Range(0, _Reloading.Length)]);
        } else if (!b)
        {
            //Play cannot reload
            CoPilot.PlayOneShot(_NoBackupAmmunition[Random.Range(0, _NoBackupAmmunition.Length)]);
        }
    }

   
    //health damage copilot warnings

    public void TakingDamage()
    {
        CoPilot.PlayOneShot(_TakingDamage[Random.Range(0, _TakingDamage.Length)]);
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
