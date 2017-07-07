using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject _TurnLever;
    private TurnLever LeverScript;
    public float turnspeed;
    public GameObject ObjectWeTurn;


	// Use this for initialization
	void Start () {
        LeverScript = _TurnLever.GetComponent<TurnLever>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckTurning();

    }
    //if the TurnLever has signalled that it is turning right or left then this function rotates the player frame. 
    void CheckTurning()
    {
        Vector3 newRot = ObjectWeTurn.transform.eulerAngles;
        if (LeverScript.TurningLeft)
        {
            newRot.y -= turnspeed * Time.deltaTime;
        }

        else if (LeverScript.TurningRight)
        {
            newRot.y += turnspeed * Time.deltaTime;
        }

        ObjectWeTurn.transform.eulerAngles = newRot;
    }
}
