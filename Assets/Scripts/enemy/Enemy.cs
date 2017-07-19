using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Exploder.Utils;

public class Enemy : MonoBehaviour {

    [Tooltip("Which Gameobject should this enemy be following?")]
    public GameObject followingObject;

    //Health value of the enemy
    float health;
    public float maxHealth;

    // Navmesh Agent
    private NavMeshAgent agent;

    EnemySpawning spawner;

    // Use this for initialization
    void Start () {
        spawner = FindObjectOfType<EnemySpawning>();
        agent = GetComponent<NavMeshAgent>();
	}

    //Called at a fixed rate
    private void FixedUpdate()
    {
        //Consistently ensure that the enemy is following the specified object
        agent.SetDestination(followingObject.transform.position);
    }

    // Update is called once per frame
    void Update () {

	}

    //Procced on the death of the agent
    void OnDeath()
    {
        //Add to the death count, if any
        //Add to the point value

        //Make it explode
        ExploderSingleton.Instance.ExplodeObject(this.gameObject);

        //Reduce the count on the spawner
        --spawner.enemyCount;
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
