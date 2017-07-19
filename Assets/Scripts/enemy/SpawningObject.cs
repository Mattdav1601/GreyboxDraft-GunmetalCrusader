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
    public float checkRate;
    float checkCount;

    //A reference to the spawner
    EnemySpawning spawner;

	// Use this for initialization
	void Start () {
        //Initialise variables
        spawner = FindObjectOfType<EnemySpawning>();
        checkCount = checkRate = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
        //Countdown
        checkCount -= Time.deltaTime;

        //If the count is less than 0
        if (checkCount <= 0)
        {
            //Check the distance between this object and the player
            if (Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position) <= acceptableRange && !spawner.spawningObjects.Contains(this.gameObject))
            {
                spawner.spawningObjects.Add(this.gameObject);
            }
            else if (Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position) >= acceptableRange && spawner.spawningObjects.Contains(this.gameObject))
                spawner.spawningObjects.Remove(this.gameObject);
        }
	}
}
