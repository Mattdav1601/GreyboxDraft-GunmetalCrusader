using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerController : MonoBehaviour {

    //public GameObject LeftGunLaser;
    //public GameObject RightGunLaser;
    //public GameObject LeftBezierLazer;
    //public GameObject RightBezierLazer;


    public GameObject Rend;

    // Use this for initialization
    void Start () {
        Rend.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

       // if EAS.UP is a heckin hapnin thing

        {
            //if controller using  EAS.up is left
            //if left trigger is held
            //{activate bezier renderer}

            //if controller using EAS.up is right
            //if right trigger is held
            //{activate bezier renderer}
        }
        Rend.SetActive(false);
        foreach (MechaWeaponInterfacePoint wp in FindObjectsOfType<MechaWeaponInterfacePoint>())
        {
            if(wp.ActiveSide == MechaWeaponInterfacePoint.E_ActiveSide.EAS_Up)
            {
               if ( wp.UsingController.GetComponent<VRTK.VRTK_ControllerEvents>().triggerPressed)
                {
                    Rend.SetActive(true);
                }

                else if ( wp.UsingController.GetComponent<VRTK.VRTK_ControllerEvents>().triggerPressed)
                {
                    Rend.SetActive(true);
                }
            }
        }

        //LeftGunLaser.active = false;
        //RightGunLaser.active = false;
        //LeftBezierLazer.active = false;
        //RightBezierLazer.active = false;
        //foreach (MechaWeaponInterfacePoint wp in FindObjectsOfType<MechaWeaponInterfacePoint>())
        //{
        //    switch(wp.ActiveSide)
        //    {
        //        case MechaWeaponInterfacePoint.E_ActiveSide.EAS_Left:
        //            {
        //                LeftGunLaser.active = true;
        //                break;
        //            }
        //        case MechaWeaponInterfacePoint.E_ActiveSide.EAS_Right:
        //            {
        //                RightGunLaser.active = true;
        //                break;
        //            }
        //        case MechaWeaponInterfacePoint.E_ActiveSide.EAS_Up:
        //            {
        //                LeftBezierLazer.active = true;
        //                RightBezierLazer.active = true;
        //                break;
        //            }
        //    }
        //}
    }
}
