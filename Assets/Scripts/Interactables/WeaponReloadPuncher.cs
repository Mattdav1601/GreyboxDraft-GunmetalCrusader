using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WeaponReloadPuncher : VRTK_InteractableObject
{
    public int sideIndex = 0;
    private RaycastHit hit;
    public float CooldownTime = 0.5f;
    private float RealCooldownTime;
    void Start()
    {
        RealCooldownTime = CooldownTime;
    }
    public override void StartTouching(GameObject currentTouchingObject)
    {
        if (RealCooldownTime <= 0)
        {
            EventManager.instance.OnWeaponReload.Invoke(sideIndex);
            RealCooldownTime = CooldownTime;
        }
    }


  
}