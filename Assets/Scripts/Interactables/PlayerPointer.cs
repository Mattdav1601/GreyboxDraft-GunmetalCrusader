using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerPointer : MonoBehaviour {

    // Holds the index of which weapon this attaches to. 0,1 correspond to weapons, 2 corresponds to the jump
    private int sideIndex = 0;

    // Holds a reference to the controller that is using us' events
    private VRTK_ControllerEvents controllerEvents;

    private bool triggerDown = false;

    // Use this for initialization
    void Start () {
        EventManager.instance.OnControllerDisconnect.AddListener((p) => {
            if (controllerEvents.gameObject == p)
            {
                Destroy(this.gameObject);
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
		if(controllerEvents)
        {
            if(controllerEvents.triggerClicked != triggerDown)
            {
                triggerDown = controllerEvents.triggerClicked;
                OnWeaponFirePacket wfp = new OnWeaponFirePacket();
                wfp.Pressed = triggerDown;
                wfp.SlotIndex = sideIndex;

                EventManager.instance.OnWeaponFire.Invoke(wfp);
            }
        }
	}

    public void initializePointer(VRTK_ControllerEvents c, int side)
    {
        controllerEvents = c;
    }
}
