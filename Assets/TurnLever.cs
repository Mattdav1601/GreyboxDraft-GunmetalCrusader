using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TurnLever : VRTK_InteractableObject {

    private Vector3 originalPosition;
    public float maxOffset;

    public GameObject CurrentController;
    private Vector3 ContLastLoc;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (CurrentController != null)
        {
            float newX = CurrentController.transform.position.x;

            if (newX > transform.position.x + maxOffset)
                newX = transform.position.x + maxOffset;
            else if (newX < transform.position.x - maxOffset)
                newX = transform.position.x - maxOffset;

            Vector3 newPos = this.transform.position;
            newPos.x = newX;
            this.transform.position = newPos;
        }
    }

    public override void StartUsing(GameObject usingObject)
    {
        CurrentController = usingObject;
        ContLastLoc = CurrentController.transform.position;
    }

    public override void StopUsing(GameObject previousUsingObject)
    {
        transform.position = originalPosition;
        CurrentController = null;
    }

}
