using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	/* Dakota's hekkin checkulisto
	 * Firing Functionality (Spawn an actor while firing that does stuff)
	 * Crit Modifier/Splash Damage/Penetration ;)
	 * Firing Effects
	 * */

	// Stores which side of the mech this weapon is on. 0 = Left, 1 = Right. Any others will be completely inactive.
	[Tooltip("Stores which side of the mech this weapon is on. 0 = Left, 1 = Right. Any others will be completely inactive.")]
	[SerializeField]
	private int sideIndex = 0;

	// Holds the amount of time (in seconds) until the weapon can be fired again after each shot.
	[Tooltip("Holds the amount of time (in seconds) until the weapon can be fired again after each shot.")]
	[SerializeField]
	private float fireInterval = 1.0f;

	// Holds the amount of time (in seconds) until the weapon can be fired again.
	private float fireTimer = 0.0f;

	// Should the weapon automatically fire as long as the trigger is held?
	[Tooltip("Should the weapon automatically fire as long as the trigger is held?")]
	[SerializeField]
	private bool autoRefire = false;

	// Holds whether the weapon is currently firing.
	private bool isFiring = false;

	// The maximum amount of ammo in a single clip
	[Tooltip("The maximum amount of ammo in a single clip.")]
	[SerializeField]
	private int maxAmmoCount = 255;

	// The amount of ammo in the current clip
	private int ammoCount;

	// The maximum amount of reserve clips.
	[Tooltip("The maximum amount of reserve clips.")]
	[SerializeField]
	private int maxClipCount = 16;

	// The amount of reserve clips remaining
	private int clipCount;

	// The amount of time (in seconds) that a reload takes.
	[Tooltip("The amount of time (in seconds) that a reload takes.")]
	[SerializeField]
	private float maxReloadTime = 2.0f;

	// The amount of time (in seconds) left in the current reload.
	private float reloadTimer = 0.0f;

	// Stores if we are currently reloading
	private bool isReloading = false;

	// The amount of x/y spread that can be imposed per shot.
	[Tooltip("The max amount of x/y spread that can be imposed per shot and the end of the volley")]
	[SerializeField]
	private Vector2 maxSpreadAmount = new Vector2(16.0f,16.0f);

	// The percentage of max of x/y spread that can be imposed on the first shot.
	[Tooltip("The percentage of max of x/y spread that can be imposed on the first shot.")]
	[SerializeField]
	private float minSpreadPercentage = 0.1f;

	// The number of shots it takes before the weapon is as inaccurate as it can be.
	[Tooltip("The number of shots it takes before the weapon is as inaccurate as it can be.")]
	[SerializeField]
	private int maxVolleyLength = 10;

	// The number of shots fired during the current volley.
	private int volleyLength = 0;

	// The amount of time (in seconds) before the volley resets and recoil begins calculating from the start.
	[Tooltip("The amount of time (in seconds) before the volley resets and recoil begins calculating from the start.")]
	[SerializeField]
	private float recoilTime = 1.0f;

    // The amount of time (in seconds) remaining until the volley resets.
    private float recoilTimer = 0.0f;

    // The amount of time (in seconds) before the volley resets and recoil begins calculating from the start.
    [Tooltip("Used exclusively for shotguns/flak guns. Determines how many times to run the projectile spawn action.")]
	[SerializeField]
	private int projectilesPerShot = 1;

    // Array of Locations and Rotations that bullets will be spawned from.
    [Tooltip("Array of Locations and Rotations that bullets will be spawned from.")]
	[SerializeField]
	private Transform[] muzzleTransform;

    // The current muzzle we should fire a shot from.
    private int currentMuzzle = 0;

    // The prefab that gets instantiated when the weapon is fired.
    [Tooltip("The prefab that gets instantiated when the weapon is fired.")]
    [SerializeField]
    private GameObject firedObject;

    /*
	 * Called on instance create
	 */
    void Start(){
		// Bind Events
		EventManager.instance.OnWeaponFire.AddListener((wfp) => {
			TakeFireInput(wfp);
		});
		EventManager.instance.OnWeaponFire.AddListener((i) => {
			TakeFireInput(i);
		});

		// Init ammo variables
		ammoCount = maxAmmoCount;
		clipCount = maxClipCount;
	}

	/*
	 * Called once per frame.
	 */
	void Update(){
		// Call CheckFire to...check the fire.
		CheckFire ();

		// Decrement the fireTimer
		fireTimer -= Time.deltaTime;

		// Call CheckReloadDone to...check if the reload is done.
		CheckReloadDone();

		// Decrement the reloadTimer
		reloadTimer -= Time.deltaTime;

        // Decrement the recoilTimer
        resetVolley();
	}

	/*
	 * TakeFireInput is called when OnWeaponFire is called
	 */
	void TakeFireInput(OnWeaponFirePacket wfp){
		if(wfp.SlotIndex == this.sideIndex)
			isFiring = wfp.Pressed;
	}

	/*
	 * Called once a frame to see if we are attempting to fire, and doing so if we can.
	 */
	void CheckFire(){
		if (isFiring && fireTimer <= 0.0f && ammoCount > 0) {
			DoFire ();
		} else {
			isFiring = false;
			if(ammoCount <= 0)
				EventManager.instance.OnAttemptFireWhileDepleted.Invoke(sideIndex);
		}
	}

	/*
	 * Calculates the amount of spread
	 */
	Vector2 CalcSpreadAmount()
	{
		float multiplier = Mathf.Lerp (minSpreadPercentage, 1.0f, volleyLength/maxVolleyLength);
		return new Vector2 (Random.Range(-maxSpreadAmount.x, maxSpreadAmount.x) * multiplier, Random.Range(-maxSpreadAmount.y, maxSpreadAmount.y) * multiplier);
	}

	/*
	 * Called from CheckFire when we can and are firing the weapon.
	 */
	void DoFire(){
        for (int i = 0; i < projectilesPerShot; i++) // For Shotguns, fire more than one
            Instantiate(firedObject, muzzleTransform[currentMuzzle]);
        currentMuzzle = nextMuzzle();
        fireTimer = fireInterval;
		isFiring = autoRefire;
        incrementVolley();
        modifyAmmo(-1);
	}

	/*
	 * TakeFireInput is called when OnWeaponReload is called
	 */
	void DoReload(int i){
		if (i == sideIndex) {
			if (clipCount > 0) {
                modifyAmmo(-maxAmmoCount);
				reloadTimer = maxReloadTime;
				isReloading = true;
			}

            // Tell the event manager we have begun reloading this weapon.
            EventManager.instance.OnReloadAttempt.Invoke(clipCount > 0);
		}
	}

	/*
	 * Called once a frame to see if we are done reloading, if at all.
	 */
	void CheckReloadDone(){
		if (isReloading && reloadTimer <= 0) {
			isReloading = false;
            fillAmmo();
            modifyClips(-1);
            resetMuzzle();

            // Tell the event manager we have finished reloading this weapon
            EventManager.instance.OnReloadComplete.Invoke (sideIndex);
		}
	}

    /*
     * Called after firing to increment the muzzle index.
     */
    int nextMuzzle()
    {
        int newMuzz = currentMuzzle + 1;
        // Overflow the muzzle index
        if (newMuzz > muzzleTransform.Length)
            newMuzz = 0;

        return newMuzz;
    }

    /*
     * Resets the muzzle index to zero.
     */
    void resetMuzzle()
    {
        currentMuzzle = 0;
    }

    /*
     * Used to add or subtract ammo.
     */
    void modifyAmmo(int amnt)
    {
        ammoCount = Mathf.Clamp(ammoCount + amnt, 0, maxAmmoCount);
    }

    /*
     * Maxes out ammo count.
     */
    void fillAmmo()
    {
        ammoCount = maxAmmoCount;
    }

    /*
     * Used to add or subtract clips.
     */
    void modifyClips(int amnt)
    {
        clipCount = Mathf.Clamp(clipCount + amnt, 0, maxClipCount);
    }

    /*
     * Maxes out clip count.
     */
    void fillClips()
    {
        clipCount = maxClipCount;
    }

    /*
     * Adds to our volley length and resets the recoil timer
     */
    void incrementVolley()
    {
        volleyLength = Mathf.Clamp(volleyLength + 1, 0, maxVolleyLength);
        recoilTimer = recoilTime;
    }

    /*
     * Reset our volley after the cool down
     */
    void resetVolley ()
    {
        recoilTimer -= Time.deltaTime;
        if (recoilTimer <= 0.0f)
            volleyLength = 0;
    }
}
