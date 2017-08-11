using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GunPuller : VRTK_InteractableObject {

    [Tooltip("Determines which mech-arms the spawned guns will relate to.")]
    public MechaWeaponInterfacePoint.E_ActiveSide ActiveSide = MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left;

    [Tooltip("The ghost gun model that the player will see on their controllers")]
    public GameObject ghostGun;

    [Tooltip("False for left side, true for right side")]
    public bool handSide = true;

    public bool AlreadySpawned = false;


    void Start()
    {
        EventManager.instance.OnControllerConnect.AddListener((p, o)=> {
            if(p == this.gameObject)
            {
                OnSwipeUp(o);
            }
        });
    }

    public override void StartUsing(GameObject usingObject)
    {
        if (!AlreadySpawned)
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GameObject newGhost = Instantiate(ghostGun, usingObject.transform.position, Quaternion.identity) as GameObject;
            newGhost.GetComponent<MechaWeaponInterfacePoint>().UsingController = usingObject;
            newGhost.GetComponent<MechaWeaponInterfacePoint>().ActiveSide = ActiveSide;

            newGhost.GetComponent<MechaWeaponInterfacePoint>().myPuller = this;
            AlreadySpawned = true;
        }
        else return;        
    }

    //Performed when swiping up
    public void OnSwipeUp(GameObject usingObject)
    {
        if (!AlreadySpawned)
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GameObject newGhost = Instantiate(ghostGun, usingObject.transform.position, Quaternion.identity) as GameObject;
            newGhost.GetComponent<MechaWeaponInterfacePoint>().UsingController = usingObject;
            newGhost.GetComponent<MechaWeaponInterfacePoint>().ActiveSide = ActiveSide;

            newGhost.GetComponent<MechaWeaponInterfacePoint>().myPuller = this;
            AlreadySpawned = true;
        }
        else return;  
    }
}
