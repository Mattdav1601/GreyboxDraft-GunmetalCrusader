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

  
    //lets move
    private float dist;

    public bool Movement = true;

    private float timetillmovement;

    //disable on explosion
    [Tooltip("after being hit by an explosion, how long is the enemy knocked back for")]
    [SerializeField]
    private float TimeDisabledFor;

    private bool LookOnly = false;


    //attacking stuff
    [SerializeField]
    private float AttackRange;

    private float disttoplayer;

    [SerializeField]
    private float AttackWindUpTime;

    [SerializeField]
    private float Damagearoony;

    private float TimeTillAttack;

    [SerializeField]
    private GameObject DamageIndicator;

    //animations
    [SerializeField]
    private Animator MyAnim;

  
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        _WaveManager = GameObject.FindObjectOfType<WaveManager>();
        TimeTillAttack = AttackWindUpTime;

       

    }

    //Called at a fixed rate
    private void FixedUpdate()
    {
        dist = Vector3.Distance(target.transform.position, transform.position);

        if (Movement)
        {
            
            if (dist >= AttackRange)
            {
                if (agent.isActiveAndEnabled)
                {
                    Move();
                }
            }

            else
            {
                AttackSession();
                Look();
            }
           
        }
        
    }

    void AttackSession()
    {
        TimeTillAttack -= Time.deltaTime;

        if (TimeTillAttack <= 0)
        {
            ActualAttack();
            Debug.Log("Enemy called attack");
            TimeTillAttack = AttackWindUpTime;
        }
    }
    void ActualAttack()
    {
        target.GetComponent<PlayerHealth>().TakeDamage(Damagearoony);
        Debug.Log("player told to take damage");
        Instantiate(DamageIndicator, transform.position + transform.forward * 0.95f + transform.up * 0.8f, Quaternion.identity);
    }

    void Move()
    {
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.SetDestination(target.transform.position);
        MyAnim.SetBool("Moving", true);
    }

    void Look()
    {
        agent.updatePosition = false;
        agent.updateRotation = true;
        agent.SetDestination(target.transform.position);
        MyAnim.SetBool("Moving", false);
    }
    

     

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


    }


    //Procced on the death of the agent
    void OnDeath()
    {
        //Add to the death count, if any
        //Add to the point value

        //Make it explode
        agent.enabled = false;
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
