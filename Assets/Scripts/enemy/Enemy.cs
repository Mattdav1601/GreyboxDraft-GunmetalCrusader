using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Exploder.Utils;

public class Enemy : MonoBehaviour {

 
    //Health value of the enemy
    [SerializeField]
    private float health;
    public float maxHealth;

    private WaveManager _WaveManager;

    // Navmesh Agent
    private NavMeshAgent agent;

    [Tooltip("Which Gameobject should this enemy be following?")]
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float StopDist;

    private float dist;

    public bool Movement = true;

    private float timetillmovement;

    [Tooltip("after being hit by an explosion, how long is the enemy knocked back for")]
    [SerializeField]
    private float TimeDisabledFor;

    private bool LookOnly = false;

    //4 point raycast AI

    //[SerializeField]
    //private int range;

    //[SerializeField]
    //private float speed;

    //[SerializeField]
    //private float rotationSpeed;

    //[SerializeField]
    //private float raycastsideoffset;

    //[SerializeField]
    //private float raycastlength;

    //private RaycastHit hit;

    //private bool isThereAnyThing = false;

    //EnemySpawning spawner;

    void Start () {
        //spawner = FindObjectOfType<EnemySpawning>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        _WaveManager = GameObject.FindObjectOfType<WaveManager>();

    }

    //Called at a fixed rate
    private void FixedUpdate()
    {
        dist = Vector3.Distance(target.transform.position, transform.position);

        if (Movement)
        {
            if (dist >= StopDist)
            {
                if (agent.isActiveAndEnabled)
                {
                    agent.SetDestination(target.transform.position);
                }
            }
            //{ Move(); }
            //Look();
        }
        
    }


    //void Look()
    //{
    //    //Look At Somthly Towards the Target if there is nothing in front.
    //    if (!isThereAnyThing)
    //    {
    //        Vector3 relativePos = target.transform.position - transform.position;
    //        Quaternion rotation = Quaternion.LookRotation(relativePos);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    //    }
    //    //Checking for any Obstacle in front.
    //    // Two rays left and right to the object to detect the obstacle.
    //    Transform leftRay = transform;
    //    Transform rightRay = transform;
    //    //Use Phyics.RayCast to detect the obstacle
    //    if (Physics.Raycast(leftRay.position + (transform.right * raycastsideoffset), transform.forward, out hit, range) || Physics.Raycast(rightRay.position - (transform.right * raycastsideoffset), transform.forward, out hit, range))
    //    {
    //        if (hit.collider.gameObject.CompareTag("Obstacles"))
    //        {
    //            isThereAnyThing = true;
    //            int sidechance = Random.Range(0, 3);
    //            if (sidechance == 1)
    //            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);

    //            else if (sidechance == 2)
    //            transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed);
    //        }
    //    }
    //    // Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
    //    // Just making this boolean variable false it means there is nothing in front of object.
    //    if (Physics.Raycast(transform.position - (transform.forward * raycastlength), transform.right, out hit, 10) ||
    //    Physics.Raycast(transform.position - (transform.forward * raycastlength), -transform.right, out hit, 10))
    //    {
    //        if (hit.collider.gameObject.CompareTag("Obstacles"))
    //        {
    //            isThereAnyThing = false;
    //        }
    //    }
    //    // Use to debug the Physics.RayCast.
    //    Debug.DrawRay(transform.position + (transform.right * range), transform.forward * 20, Color.red);
    //    Debug.DrawRay(transform.position - (transform.right * range), transform.forward * 20, Color.red);
    //    Debug.DrawRay(transform.position - (transform.forward * raycastlength), -transform.right * 20, Color.yellow);
    //    Debug.DrawRay(transform.position - (transform.forward * raycastlength), transform.right * 20, Color.yellow);

    //}

    //void Move()
    //{
    //    // Enemy translate in forward direction.
    //    transform.Translate(Vector3.forward * Time.deltaTime * speed);
    //}

    // Update is called once per frame

     

    void Update()
    {
        if (Movement == false)
         {
            timetillmovement += Time.deltaTime;

            if (timetillmovement >= TimeDisabledFor)
            {
                timetillmovement = 0;
                agent.enabled = true;
                Movement = true;
               
            }
         }


            //testing purposes

        if (Input.GetKeyDown(KeyCode.O))
        {
            OnDeath();
        }


                
                
    }


    //Procced on the death of the agent
    void OnDeath()
    {
        //Add to the death count, if any
        //Add to the point value

        //Make it explode
        ExploderSingleton.Instance.ExplodeObject(this.gameObject);

        //Reduce the count on the spawner
        _WaveManager.enemiesalive --;
       // --spawner.enemyCount;
    }


    //Procced by an outside agent when taking damage
    public void TakeDamage(float takenDamage)
    {
        //Add some points when hit??

        //Take health away equal to the parameter given
        health -= takenDamage;

        //Check if health is less than zero
        if (health <= 0)
            OnDeath();
    }
}
