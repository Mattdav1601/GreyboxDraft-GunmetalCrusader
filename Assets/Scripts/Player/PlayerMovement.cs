using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //public GameObject _TurnLever;
    //private TurnLever LeverScript;
    public GameObject turnRayOrigin;
    public float RotationBound;
    public float RestingYRotation;
    public float StoredTurnDir = 0;
    public float MiniminBounds;
    public float turnspeed;
    public float Maxturnspeed = 5.0f;
    public float Minturnspeed = 5.0f;
    public float TurnDecelerateSpeed = 8.0f;
    public float CurrTurnSpeed = 0;
    public GameObject ObjectWeTurn;

    public SoundManager soundController;

    private Vector3 StoredJumpDirection = new Vector3(0,0,0);
    public float BoostVelocityTransfer = 10.0f;
    public float BoostHopVelocity = 25.0f;

    public bool WeDoinAHekkinJumpo = false;
   // private GameObject JumpController;

    public float BoostSpeed = 1.0f;
    public float AltLock = 4.0f;
    public Vector3 StoredPos;
    public bool StoredShitYet = false;
    public float StopThreshold = 10.0f;
    public bool canjump = false;

    //VRTK.VRTK_ControllerEvents controllerEvents;

    private float CurrDist;
    public bool canjumpthatdistance = false;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
      //  LeverScript = _TurnLever.GetComponent<TurnLever>();
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
        if (Camera.main.transform.localEulerAngles.y < RotationBound || Camera.main.transform.localEulerAngles.y > (360 - RotationBound))
        {
            UpdateTurnSpeed(false);
            newRot.y += (CurrTurnSpeed * Time.deltaTime) * StoredTurnDir;
        }
        else
        {
            UpdateTurnSpeed(true);

            if (Camera.main.transform.localEulerAngles.y < 180)
            {
                newRot.y += CurrTurnSpeed * Time.deltaTime;
                StoredTurnDir = 1;
            }
            else
            {
                newRot.y -= CurrTurnSpeed * Time.deltaTime;
                StoredTurnDir = -1;
            }
        }

        ObjectWeTurn.transform.eulerAngles = newRot;

        //StoredTurnDir

        //dont delete it k.

        //Vector3 newRot = ObjectWeTurn.transform.eulerAngles;
        //if (LeverScript.TurningLeft)
        //{
        //    UpdateTurnSpeed(true);
        //    newRot.y -= CurrTurnSpeed * Time.deltaTime;
        //}

        //else if (LeverScript.TurningRight)
        //{
        //    UpdateTurnSpeed(true);
        //    newRot.y += CurrTurnSpeed * Time.deltaTime;
        //}

        //else
        //    UpdateTurnSpeed(false);

        //ObjectWeTurn.transform.eulerAngles = newRot;
    }

    void UpdateTurnSpeed(bool increasing)
    {
        if (increasing)
            CurrTurnSpeed = Mathf.Clamp(CurrTurnSpeed + (turnspeed * Time.deltaTime), Minturnspeed, Maxturnspeed);
        else
            CurrTurnSpeed = Mathf.Clamp(CurrTurnSpeed - (turnspeed * Time.deltaTime * TurnDecelerateSpeed), 0, Maxturnspeed);
    }

    void DoJumperoonie()
    {
        if(WeDoinAHekkinJumpo)
        {
            if (!StoredShitYet )
            {
                StoredPos = this.transform.position;
                Debug.Log("stored jump data");
                CurrDist = 0;
                soundController.BoosterEngaged();
                StoredShitYet = true;
                soundController.Boosting();
            }

            rb.constraints = RigidbodyConstraints.FreezeAll;
            Vector3 Target = GameObject.FindGameObjectWithTag("JumpTarget").transform.position;

            StoredJumpDirection = (this.transform.position - Target).normalized;

            float MaxDist = Vector3.Distance(new Vector3(StoredPos.x, 0, StoredPos.z), new Vector3(Target.x, 0, Target.z));
            if (MaxDist - CurrDist < StopThreshold)
            {
                WeDoinAHekkinJumpo = false;
                Debug.Log("cancelled cause they tried to jump too far");
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

    public void GroundoPoundo()
    {
        WeDoinAHekkinJumpo = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        StoredShitYet = false;

        //DELET THIS LINE IF IT FEELS LIKE SHIT YO
        rb.AddForce(new Vector3(BoostVelocityTransfer * StoredJumpDirection.x, BoostHopVelocity, BoostVelocityTransfer * StoredJumpDirection.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        WeDoinAHekkinJumpo = false;
        soundController.EndBoostSound();
        Debug.Log(collision.gameObject.name);

        if (collision.collider.tag == "Floor" && Time.timeSinceLevelLoad >= 4)
        {
            soundController.EndBoosting();
            Debug.Log("Should have heard a clang");
        }

        else if (collision.collider.tag == "Enemy" && rb.velocity.y >= 1)
        {
            //call death
            collision.collider.GetComponent<Enemy>().OnDeath();

        }
        else if (collision.collider.tag == "KillBox")
        {
            //call death
            GetComponent<PlayerHealth>().Die();

        }
    }
}
