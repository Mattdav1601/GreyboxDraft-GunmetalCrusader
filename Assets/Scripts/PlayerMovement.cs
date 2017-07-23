using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject _TurnLever;
    private TurnLever LeverScript;
    public float turnspeed;
    public float Maxturnspeed = 5.0f;
    public float Minturnspeed = 5.0f;
    public float CurrTurnSpeed = 0;
    public GameObject ObjectWeTurn;

    public bool WeDoinAHekkinJumpo = false;
   // private GameObject JumpController;

    public float BoostSpeed = 1.0f;
    public float AltLock = 4.0f;
    public Vector3 StoredPos;
    public bool StoredShitYet = false;
    public float StopThreshold = 10.0f;

    //VRTK.VRTK_ControllerEvents controllerEvents;

    private float CurrDist;
    public bool canjumpthatdistance = false;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        LeverScript = _TurnLever.GetComponent<TurnLever>();
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckTurning();
        DoJumperoonie();

       
       


    }
    //if the TurnLever has signalled that it is turning right or left then this function rotates the player frame. 
    void CheckTurning()
    {
        Vector3 newRot = ObjectWeTurn.transform.eulerAngles;
        if (LeverScript.TurningLeft)
        {
            UpdateTurnSpeed(true);
            newRot.y -= CurrTurnSpeed * Time.deltaTime;
        }

        else if (LeverScript.TurningRight)
        {
            UpdateTurnSpeed(true);
            newRot.y += CurrTurnSpeed * Time.deltaTime;
        }

        else
            UpdateTurnSpeed(false);

        ObjectWeTurn.transform.eulerAngles = newRot;
    }

    void UpdateTurnSpeed(bool increasing)
    {
        if (increasing)
            CurrTurnSpeed = Mathf.Clamp(CurrTurnSpeed + (turnspeed * Time.deltaTime), Minturnspeed, Maxturnspeed);
        else
            CurrTurnSpeed = Mathf.Clamp(CurrTurnSpeed - (turnspeed * Time.deltaTime * Minturnspeed), Minturnspeed, Maxturnspeed);
    }

    void DoJumperoonie()
    {
        if(WeDoinAHekkinJumpo)
        {
            if (!StoredShitYet)
            {
                StoredPos = this.transform.position;
                Debug.Log("stored jump data");
                CurrDist = 0;
                StoredShitYet = true;
            }

            rb.constraints = RigidbodyConstraints.FreezeAll;
            Vector3 Target = GameObject.FindGameObjectWithTag("JumpTarget").transform.position;


            float MaxDist = Vector3.Distance(new Vector3(StoredPos.x, 0, StoredPos.z), new Vector3(Target.x, 0, Target.z));
            if (MaxDist - CurrDist < StopThreshold)
            {
                WeDoinAHekkinJumpo = false;
            }
            else
            {

                CurrDist += BoostSpeed * Time.deltaTime;
                Vector3 NewPos = Vector3.Lerp(StoredPos, Target, CurrDist / MaxDist);

                float Height = Mathf.Lerp(StoredPos.y, StoredPos.y + AltLock, Mathf.Sin((CurrDist / MaxDist) * Mathf.PI));
                NewPos.y = Height;

                this.transform.position = NewPos;

                //rb.MovePosition(NewPos);
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StoredShitYet = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        WeDoinAHekkinJumpo = false;
    }
}
