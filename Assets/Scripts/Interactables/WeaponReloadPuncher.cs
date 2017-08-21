using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WeaponReloadPuncher : VRTK_InteractableObject
{
    public int sideIndex = 0;
    public override void StartTouching(GameObject currentTouchingObject)
    {
        EventManager.instance.OnWeaponReload.Invoke(sideIndex);
    }
}