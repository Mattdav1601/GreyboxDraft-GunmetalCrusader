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

    private bool falling = true;

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

    [SerializeField]
    private GameObject Trail;

   //sound
   private SoundManager sound;
    private AudioSource audios;

    //minimap marker
    public GameObject marker;
  
    void Start () {
        sound = FindObjectOfType<SoundManager>();
        audios = this.GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        _WaveManager = GameObject.FindObjectOfType<WaveManager>();
        TimeTillAttack = AttackWindUpTime;
        Movement = false;
        agent.enabled = false;
    }

    //Called at a fixed rate
    private void FixedUpdate()
    {
        dist = Vector3.Distance(target.transform.position, transform.position);

        if (Movement && falling == false)
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
            target.GetComponent<PlayerHealth>().TakeDamage(Damagearoony);
            Debug.Log("Enemy called attack");
            TimeTillAttack = AttackWindUpTime;
        }
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
        //agent.SetDestination(target.transform.position);
        MyAnim.SetBool("Moving", false);
    }
    

     

    void Update()
    {
        if (Movement == false && falling == false)
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
    public void OnDeath()
    {
        Destroy(marker);
        //Add to the death count, if any
        //Add to the point value
        sound.AndroidDie(audios);
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

    void OnCollisionEnter(Collision Col )
    {
        if (Col.collider.tag == "Floor")
        {
            agent.enabled = true;
            falling = false;
            Movement = true;
            Destroy(Trail);
        }
    }
}
