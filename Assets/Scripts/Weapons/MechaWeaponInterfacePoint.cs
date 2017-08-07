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

    float PointerRange = 1000;
    public Vector3 TargetPos = new Vector3(0,0,0);

    public GameObject LaserPointer;

    public LineRenderer lineRend;

    public VRTK.VRTK_ControllerEvents controllerEvents;

    //The two hands of the mech
    public GameObject mechLeft;
    public GameObject mechRight;

    public GameObject JumpDestination;
    private GameObject Player;

    public int InvulFrames = 10;

    public Renderer[] rend;


    public Animation death;



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
                Destroy(this.gameObject, 1.0f);
                death.Play();
            }
        }

        if (controllerEvents.triggerClicked)
        {
            if(tag == "JumpJetInterface")
                    Player.GetComponent<PlayerMovement>().WeDoinAHekkinJumpo = true;
        }

        DoRaycast();
    }

    //This function will set the basic variables for this object
    void SetBasics()
    {
        mechRight = GameObject.FindGameObjectWithTag("MechR");
        mechLeft = GameObject.FindGameObjectWithTag("MechL");

        JumpDestination = GameObject.FindGameObjectWithTag("JumpTarget");
        Player = GameObject.FindGameObjectWithTag("Player");

        controllerEvents = UsingController.GetComponent<VRTK.VRTK_ControllerEvents>();

        switch (ActiveSide)
        {
            case E_ActiveSide.EAS_Left:
                {
                   // mechLeft.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer = mechLeft.GetComponent<VRTK.VRTK_StraightPointerRenderer>();
                    this.gameObject.tag = "WeaponInterfaceL";
                    foreach (Renderer r in rend)
                        r.material.SetColor("_OutlineColor", Color.green);
                }
                break;

            case E_ActiveSide.EAS_Right:
                {
                   // mechRight.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer = mechRight.GetComponent<VRTK.VRTK_StraightPointerRenderer>();
                    this.gameObject.tag = "WeaponInterfaceR";
                  
                    foreach (Renderer r in rend)
                        r.material.SetColor("_OutlineColor", Color.green);
                }
                break;

            case E_ActiveSide.EAS_Up:
                {
                   // mechRight.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer = mechRight.GetComponent<VRTK.VRTK_BezierPointerRenderer>();
                    this.gameObject.tag = "JumpJetInterface";
                   
                    foreach (Renderer r in rend)
                        r.material.SetColor("_OutlineColor", Color.yellow);
                }
                break;
        }
    }

    void SetJumpPoint(Vector3 target)
    {
        JumpDestination.SetActive(true);
        if (Player.GetComponent<PlayerMovement>().WeDoinAHekkinJumpo == false)
        {
            JumpDestination.transform.position = target;
        }


        if (this.gameObject.tag == "JumpJetInterface")
        {
            JumpDestination.GetComponent<Renderer>().enabled = true;
        }

        else
        {
            JumpDestination.GetComponent<Renderer>().enabled = false;
        }
    }

    void DoRaycast()
    {
        RaycastHit hit;
        Vector3[] LineRendPts = new Vector3[2];

        LineRendPts[0] = LaserPointer.transform.position;

        if (Physics.Raycast(transform.position, LaserPointer.transform.forward, out hit, PointerRange))
        {
            LineRendPts[1] = hit.point;
            TargetPos = hit.point;

            if (this.tag == "JumpJetInterface")
            {
                if (hit.collider.tag == "Floor")
                {
                    SetJumpPoint(hit.point);
                }
              
            }
        }
        else
        {
            LineRendPts[1] = LaserPointer.transform.position + (LaserPointer.transform.forward * PointerRange);
            TargetPos = LaserPointer.transform.position + (LaserPointer.transform.forward * PointerRange);
        }

        lineRend.SetPositions(LineRendPts);
    }
}
