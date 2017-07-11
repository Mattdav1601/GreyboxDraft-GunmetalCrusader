using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject _TurnLever;
    private TurnLever LeverScript;
    public float turnspeed;
    public GameObject ObjectWeTurn;

    public bool WeDoinAHekkinJumpo = false;

    public float BoostSpeed = 1.0f;
    public float RiseSpeed = 1.0f;
    public float AltLock = 4.0f;
    public Vector3 StoredPos;
    public bool StoredShitYet = false;
    public float StopThreshold = 10.0f;

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
            newRot.y -= turnspeed * Time.deltaTime;
        }

        else if (LeverScript.TurningRight)
        {
            newRot.y += turnspeed * Time.deltaTime;
        }

        ObjectWeTurn.transform.eulerAngles = newRot;
    }

    void DoJumperoonie()
    {
        if(WeDoinAHekkinJumpo)
        {
            if (!StoredShitYet)
            {
                StoredPos = this.transform.position;
                StoredShitYet = true;
            }

            rb.isKinematic = true;
            Vector3 Target = GameObject.FindGameObjectWithTag("JumpTarget").transform.position;
            Vector3 NewPos = Vector3.Lerp(this.transform.position, Target, BoostSpeed * Time.deltaTime);

            // Height Adjust Here
            float CurrDist = Vector3.Distance(this.transform.position, new Vector3(Target.x, this.transform.position.y, Target.z));
            float MaxDist = Vector3.Distance(new Vector3(StoredPos.x, this.transform.position.y, StoredPos.z), new Vector3(Target.x, this.transform.position.y, Target.z));

            float MaxHeight = Mathf.Lerp(StoredPos.z, StoredPos.z + AltLock, Mathf.Sin((CurrDist/MaxDist) * Mathf.PI));

            Debug.Log(CurrDist / MaxDist);

            NewPos.y = MaxHeight;//Mathf.Clamp(this.transform.position.y + RiseSpeed * Time.deltaTime, StoredPos.z, StoredPos.z + AltLock); // Lock between our starting height and that + AltLock

            this.transform.position = NewPos;

            if (CurrDist < StopThreshold)
                WeDoinAHekkinJumpo = false;
        }
        else
        {
            rb.isKinematic = false;
            StoredShitYet = false;
        }
    }
}
