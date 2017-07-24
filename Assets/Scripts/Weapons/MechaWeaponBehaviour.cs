using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWeaponBehaviour : MonoBehaviour {

    [Tooltip("Point on the weapon that the projectile instantiates from")]
    [SerializeField]
    private GameObject Muzzle;

    public bool Firing = false;

    [Tooltip("Which gun does this correspond to? DON'T SET TO UP REEEEEEEE")]
    [SerializeField]
    public MechaWeaponInterfacePoint.E_ActiveSide ActiveSide = MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left;

    [Tooltip("The class spawned when the weapon fires.")]
    [SerializeField]
    private GameObject ProjectileClass;

    [Tooltip("Interval between shots.")]
    [SerializeField]
    private float ShotInterval = 1.0f;

    private float ShotTimer = 0;

    [Tooltip("Does the weapon automatically fire or does the player have to press the trigger every shot?")]
    [SerializeField]
    private bool bAutoRefire = false;

    bool AlreadyFired = false;


    //shit jack did

    [Tooltip("Ammunition count per clip")]
    [SerializeField]
    private float MaxAmmoCount;

    private float CurrentAmmo;
    [SerializeField]
    private float ReloadTime;

    private float currentreloadtime;

    private bool mustreload = false;
    private bool CalledReload = false;
    private bool CalledWeaponEmpty = false;

    [Tooltip("Total amount of clips in storage")]
    [SerializeField]
    private float TotalClips;

    private float CurrentClips;

    [SerializeField]
    private Material canfire;


    [SerializeField]
    private Material nofire;

    [SerializeField]
    private Material empty;
    //pretty sounds <3
    public SoundManager soundcontrol;

    //animations


    //private bool CalledFire = false;

    // Use this for initialization
    void Start () {
        FullRestock();
	}
	
	// Update is called once per frame
	void Update () {
        CheckController();

        if (Firing && ShotTimer <= 0.0f)
        {
            HandleFire();
        }

        ShotTimer = Mathf.Clamp(ShotTimer - Time.deltaTime, 0, ShotInterval);


        ReloadChecks();
	}

    void CallReload()
    {
        if (mustreload && CurrentClips > 0)
        {
            this.GetComponent<LineRenderer>().sharedMaterial = nofire;
           
            //Debug.Log("Called reload");
        }
        else if (mustreload && CurrentClips == 0)
        {
            if (!CalledWeaponEmpty)
            {
                soundcontrol.WeaponEmpty();
                this.GetComponent<LineRenderer>().sharedMaterial = empty;
                CalledWeaponEmpty = true;
                calledfailedreload();


            }
        }
    }

    void calledfailedreload()
    {
        mustreload = false;
        currentreloadtime = ReloadTime;
        CalledReload = false;
    }

    void CallFinishedReloading()
    {
        mustreload = false;
        CurrentAmmo = MaxAmmoCount;
        currentreloadtime = ReloadTime;
        CurrentClips--;
       // Debug.Log("Finished reloading");
       
        CalledReload = false;
        this.GetComponent<LineRenderer>().sharedMaterial = canfire;
    }

    public void FullRestock()
    {
        CurrentClips = TotalClips;
        CalledWeaponEmpty = false;
        mustreload = true;
        CallReload();
       // Debug.Log("Realixed we need to reload");
        CalledReload = true;
    }

    void ReloadChecks()
    {

        if (CurrentAmmo <= 0)
        { if (!CalledReload)
            {
                mustreload = true;
                CallReload();
             //   Debug.Log("Realixed we need to reload");
                CalledReload = true;
            }
        }

        if (mustreload)
        {
            currentreloadtime -= Time.deltaTime;
        }
        if (currentreloadtime <= 0)
        {
            CallFinishedReloading();
        }
    }

    void HandleFire()
    {
        if (!bAutoRefire && AlreadyFired)
            return;
        else
        {
            if (Muzzle && CurrentAmmo >0)
            {
                Instantiate(ProjectileClass, Muzzle.transform.position, Muzzle.transform.rotation);
                CurrentAmmo--;
                //soundcontrol.FiredGrenade();
                AlreadyFired = true;
                ShotTimer = ShotInterval;
            }
            else
                return;
        }
    }
    
    void CheckController()
    {
        GameObject thingo = null;
        if (ActiveSide == MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left)
        {
            thingo = GameObject.FindGameObjectWithTag("WeaponInterfaceL");
            if (thingo != null)
            {
                SetFiring(thingo.GetComponent<MechaWeaponInterfacePoint>().controllerEvents.triggerClicked);
                //Debug.Log("YES, I AM!!!!!");
            }
        }
        else if (ActiveSide == MechaWeaponInterfacePoint.E_ActiveSide.EAS_Right)
        {
            thingo = GameObject.FindGameObjectWithTag("WeaponInterfaceR");
            if (thingo != null)
            {
                SetFiring(thingo.GetComponent<MechaWeaponInterfacePoint>().controllerEvents.triggerClicked);
               // Debug.Log("YES, I AM!!!!!");
            }
        }
    }

    void SetFiring(bool newFiring)
    {
        if (Firing != newFiring)
            AlreadyFired = false;
        Firing = newFiring;
    }
}
