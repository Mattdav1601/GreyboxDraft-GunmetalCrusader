using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWeaponInterfacePoint : MonoBehaviour {

    public enum E_ActiveSide
    {
        EAS_Left,
        EAS_Right
    };

    public E_ActiveSide ActiveSide = E_ActiveSide.EAS_Left;

    public GameObject UsingController;
    public GunPuller myPuller;

    public int InvulFrames = 10;

    void Update(){
        if (ActiveSide == MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left)
            this.gameObject.tag = "WeaponInterfaceL";
        else
            this.gameObject.tag = "WeaponInterfaceR";

        this.transform.position = UsingController.transform.position;
        this.transform.rotation = UsingController.transform.rotation;

        InvulFrames -= 1;

        if (UsingController.GetComponent<VRTK.VRTK_ControllerEvents>().touchpadPressed == true && InvulFrames < 0)
        {
            myPuller.AlreadySpawned = false;
            Destroy(this.gameObject, 0.0f);
        }
    }
}
