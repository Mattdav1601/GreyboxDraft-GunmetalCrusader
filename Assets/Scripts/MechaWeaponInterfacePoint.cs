using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWeaponInterfacePoint : MonoBehaviour
{

    public enum E_ActiveSide
    {
        EAS_Left,
        EAS_Right,
        EAS_Up
    };

    public E_ActiveSide ActiveSide = E_ActiveSide.EAS_Left;

    public GameObject UsingController;
    public GunPuller myPuller;

    VRTK.VRTK_ControllerEvents controllerEvents;

    //The two hands of the mech
    public GameObject mechLeft;
    public GameObject mechRight;

    public int InvulFrames = 10;

    public Renderer[] rend;

    private void Start()
    {
        SetBasics();
    }

    void Update()
    {
        if (UsingController)
        {
            this.transform.position = UsingController.transform.position;
            this.transform.rotation = UsingController.transform.rotation;


            //Take down invuln frames
            if (InvulFrames > 0)
                --InvulFrames;

            //Check if the touchpad is pressed
            if (controllerEvents.touchpadPressed && InvulFrames <= 0)
            {
                myPuller.AlreadySpawned = false;
                Destroy(this.gameObject, 0.0f);
            }
        }
    }

    //This function will set the basic variables for this object
    void SetBasics()
    {
        mechRight = GameObject.FindGameObjectWithTag("MechR");
        mechLeft = GameObject.FindGameObjectWithTag("MechL");
        controllerEvents = UsingController.GetComponent<VRTK.VRTK_ControllerEvents>();

        switch (ActiveSide)
        {
            case E_ActiveSide.EAS_Left:
                {
                    mechLeft.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer = mechLeft.GetComponent<VRTK.VRTK_StraightPointerRenderer>();
                    this.gameObject.tag = "WeaponInterfaceL";
                    foreach (Renderer r in rend)
                        r.material.SetColor("_OutlineColor", Color.green);
                }
                break;

            case E_ActiveSide.EAS_Right:
                {
                    mechRight.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer = mechRight.GetComponent<VRTK.VRTK_StraightPointerRenderer>();
                    this.gameObject.tag = "WeaponInterfaceR";
                    foreach (Renderer r in rend)
                        r.material.SetColor("_OutlineColor", Color.green);
                }
                break;

            case E_ActiveSide.EAS_Up:
                {
                    mechRight.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer = mechRight.GetComponent<VRTK.VRTK_BezierPointerRenderer>();
                    this.gameObject.tag = "JumpJetInterface";
                    foreach (Renderer r in rend)
                        r.material.SetColor("_OutlineColor", Color.yellow);
                }
                break;
        }
    }
}
