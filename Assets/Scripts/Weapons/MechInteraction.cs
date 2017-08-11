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
    bool updateCoords = false;

    float minimumSwipeDistance = 0.2f;

    public float oldCoordUpdateTime = 0.5f;
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

        if(oldCoordUpdateTimer < 0)
        {
            updateCoords = true;
            oldCoordUpdateTimer = 0.5f;
            print(oldCoordUpdateTimer);
        }
    }

    private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        //Update the coordinates
        if (updateCoords)
        {
            oldCoords = e.touchpadAxis;
            updateCoords = false;
        }

        //Do something with the new coords
        newCoords = e.touchpadAxis;

        float delta = newCoords.y - oldCoords.y;

        print(delta);

        if (Mathf.Abs(delta) > 0.8f)
        {
            if (delta < 0)
            {
                EventManager.instance.OnControllerDisconnect.Invoke(this.gameObject);
            }
            else if (delta > 0)
            {
                EventManager
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "InteractableMechElement")
        {
            if (swipeUpSuccess)
            {
                swipeUpSuccess = false;
                other.GetComponent<GunPuller>().OnSwipeUp(this.gameObject);
            }
        }
    }
}
