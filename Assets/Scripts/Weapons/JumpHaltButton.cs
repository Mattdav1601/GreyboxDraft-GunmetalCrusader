using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class JumpHaltButton : VRTK_InteractableObject
{
    private RaycastHit hit;
    public override void StartTouching(GameObject currentTouchingObject)
    { 
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 25) && hit.transform.tag == "Floor")
        {
            if (FindObjectOfType<PlayerMovement>().WeDoinAHekkinJumpo)
            {
                FindObjectOfType<PlayerMovement>().GroundoPoundo();
                this.gameObject.GetComponent<Animation>().Play();
            }
        }
    }
}
