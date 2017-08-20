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
    
    VRTK_InteractTouch iTouch;

	// Use this for initialization
	void Start () {
        //Check for errors
		if(GetComponent<VRTK_ControllerEvents>() == null)
        {
            Debug.LogError("The mech interaction script is not on a contoller with the VRTK_ControllerEvents script!");
            return;
        }
        iTouch = GetComponent<VRTK_InteractTouch>();
        oldCoordUpdateTime = oldCoordUpdateTimer;

        GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
	}
	
	// Update is called once per frame
	void Update () {

        oldCoordUpdateTimer -= Time.deltaTime;

        if(oldCoordUpdateTimer < 0)
        {
            updateCoords = true;
            oldCoordUpdateTimer = 0.8f;
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

        if (Mathf.Abs(delta) > 0.9f)
        {
            if (delta < 0)
            {
                EventManager.instance.OnControllerDisconnect.Invoke(this.gameObject);
                Debug.Log("Swiped Down");
            }
            else if (delta > 0)
            {
                if (iTouch.GetTouchedObject())
                {
                    if (iTouch.GetTouchedObject().gameObject.tag == "MechComponent")
                    {
                        EventManager.instance.OnControllerConnect.Invoke(iTouch.GetTouchedObject(), this.gameObject);
                        Debug.Log("Swiped Up");
                    }
                }                
            }
        }
    }
}
