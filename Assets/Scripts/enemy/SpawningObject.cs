using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObject : MonoBehaviour {

    /// <summary>
    /// This class is intended to dynamically change which objects are being spawned from
    /// The minimum acceptable range should be around 10-15 Units
    /// </summary>     

    //Gameobject that this will compare against to be activated
    public GameObject player;
    public float acceptableRange;

    //Controls how often the object will check if it can spawn things
    public float spawnCheckRate;
    float spawnCheckCount;

    //A reference to the spawner
    EnemySpawning enemySpawning;

	// Use this for initialization
	void Start () {
        //Initialise variables
        enemySpawning = FindObjectOfType<EnemySpawning>();
        spawnCheckCount = spawnCheckRate = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        SpawnEnemies();
	}

    void SpawnEnemies()
    {
        //Countdown
        spawnCheckCount -= Time.deltaTime;

        //If the count is less than 0
        if (spawnCheckCount <= 0)
        {
            //Check the distance between this object and the player
            if (Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position) <= acceptableRange && !enemySpawning.spawningObjects.Contains(this.gameObject))
            {
                enemySpawning.spawningObjects.Remove(this.gameObject);
            }
            else if (Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position) >= acceptableRange && !enemySpawning.spawningObjects.Contains(this.gameObject))

                enemySpawning.spawningObjects.Add(this.gameObject);

            spawnCheckCount = spawnCheckRate;
        }
    }
}
