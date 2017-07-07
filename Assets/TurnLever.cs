using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TurnLever : VRTK_InteractableObject {

    private Vector3 originalPosition;
    public float maxOffset;

    public GameObject CurrentController;
    // private Vector3 ContLastLoc;

    private Vector3 LeverOriginPoint;

    public bool TurningRight = false;
    public bool TurningLeft = false;    

    void Start()
    {
        //LeverOriginPoint = transform.localPosition;

        originalPosition = transform.localPosition;
    }

    void Update()
    {
        UpdateBools();
        Movelever();
    }

    void Movelever()
    {
        if (CurrentController != null)
        {
            //updates position of the physical gameobject in relation ot the controller.
            float newX = CurrentController.transform.parent.localPosition.x;

            if (newX > originalPosition.x + maxOffset)

                newX = transform.localPosition.x + maxOffset;

            else if (newX < originalPosition.x - maxOffset)

                newX = transform.localPosition.x - maxOffset;

            Vector3 newPos = this.transform.localPosition;

            newPos.x = newX;

            transform.localPosition = newPos;
        }
    }

    void UpdateBools()
    {
        //are we turning right or left. This function determines that based on the position of the schtick.
        TurningRight = false;
        TurningLeft = false;

        if (CurrentController != null)
        {
            if (this.transform.localPosition.x > originalPosition.x)
                TurningRight = true;
            else if (this.transform.localPosition.x < originalPosition.x)
                TurningLeft = true;
        }
    }

    public override void StartUsing(GameObject usingObject)
    {
        CurrentController = usingObject;
    }

    public override void StopUsing(GameObject previousUsingObject)
    {
        CurrentController = null;
        transform.localPosition = originalPosition;
    }

}
