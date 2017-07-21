using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    /// <summary>
    /// Enemy spawning manager should manage the spawning of enemies throughout the map
    /// </summary>

    //List of gameobjects that can be spawned from
    public List<GameObject> spawningObjects = new List<GameObject>();

    //Keep count of the current number of enemies on the map
    public int enemyCount;
    public int maxEnemyCount;

    public float spawningRate;
    float spawningCount;


    //Enemy Game object to spawn
    public GameObject enemyObject;

	// Use this for initialization
	void Start () {


        //Initialise spawning rate and count
        spawningCount = spawningRate;
	}
	
	// Update is called once per frame
	void Update () {
        spawningCount -= Time.deltaTime;

        //When spawn count is zero, spawn an enemy
        if (spawningCount <= 0 && enemyCount < maxEnemyCount && spawningObjects.Count > 0)
        {
            int obj = (int)Random.Range(0, spawningObjects.Count);

            //Find a game object to spawn at
            Instantiate(enemyObject, spawningObjects[obj].gameObject.transform.position, spawningObjects[obj].gameObject.transform.rotation);
            ++enemyCount;

            //Reset spawning counter
            spawningCount = spawningRate;
        }
	}
}
