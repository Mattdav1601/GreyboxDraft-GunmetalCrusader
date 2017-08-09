using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Events;

public class MechInteraction : MonoBehaviour {

    //Hold and update the coordinates
    Vector2 oldCoords = new Vector2(0, 0);
    Vector2 newCoords = new Vector2(0, 0);

    bool swipeUpSuccess;

    float minimumSwipeDistance = 0.3f;

    float oldCoordUpdateTime = 0.2f;
    float oldCoordUpdateTimer;

	// Use this for initialization
	void Start () {
        //Check for errors
		if(GetComponent<VRTK_ControllerEvents>() == null)
        {
            Debug.LogError("The mech interaction script is not on a contoller with the VRTK_ControllerEvents script!");
            return;
        }

        oldCoordUpdateTime = oldCoordUpdateTimer;

        GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
	}
	
	// Update is called once per frame
	void Update () {
        oldCoordUpdateTimer -= Time.deltaTime;

        //Reset timer
        if(oldCoordUpdateTimer <= 0)
        {
            oldCoordUpdateTimer = oldCoordUpdateTime;
        }

        //Clean up
        if(swipeUpSuccess)
        {
            swipeUpSuccess = false;
        }
    }

    private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        //Update the old coords if a certain time has passed
        if(oldCoordUpdateTimer <= 0)
        {
            oldCoords = e.touchpadAxis;
        }

        //Update the new coords
        newCoords = e.touchpadAxis;

        float delta = newCoords.x - oldCoords.x;

        if(Mathf.Abs(delta) > minimumSwipeDistance)
        {
            if(delta < 0)
            {
                EventManager.instance.OnControllerDisconnect.Invoke(this.gameObject);
            } else if(delta > 0)
            {
                swipeUpSuccess = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "InteractableMechElement")
        {
            if (swipeUpSuccess)
            {
                other.GetComponent<GunPuller>().OnSwipeUp(this.gameObject);
            }
        }
    }
}
