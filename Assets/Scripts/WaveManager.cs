using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    private float waveCount;
    public float TimeBetweenWaves;
    private float currentTimeBetweenWaves;


    public List<GameObject> spawningObjects = new List<GameObject>();


    private float spawncount;
    public float BaseSpawnAmount;
    public float SpawnMultiplier;


    private float enemiesspawned;

    public float TimeBetweenSpawns;
    private float currentTimeBetweenSpawns;

    //  public float maxspawndistance;

    public GameObject EnemyToSpawn;

    public GameObject Player;

    private bool DownTime = false;
    // Use this for initialization
    void Start() {

        NextWave();
    }

    // Update is called once per frame
    void Update()
    {


        DownTimeChecks();



        currentTimeBetweenSpawns -= Time.deltaTime;


        if (currentTimeBetweenSpawns <= 0 && DownTime == false)
        {
            SpawnEnemies();
            Debug.Log("SHould have spawned an enemy");

        }
        


    }
    void TimeCheckBetweenWaves()
    {
        if (currentTimeBetweenWaves <= 0)
        {
            NextWave();
        }
    }

    void DownTimeChecks()
    {
        if (DownTime == true)
        {
            currentTimeBetweenWaves -= Time.deltaTime;
        }
        
    }

    void WaveEnd()
    {
        DownTime = true;
        Debug.Log("Wave ended");
    }


    void  NextWave()
    {
        spawncount = 5; // (BaseSpawnAmount * SpawnMultiplier) * waveCount;
        Debug.Log(spawncount);
        DownTime = false;
        currentTimeBetweenWaves = TimeBetweenWaves;

        
       
        Debug.Log("Next wave");
    }


    void SpawnEnemies()
    {
        int obj = (int)Random.Range(0, spawningObjects.Count);

        //Find a game object to spawn at
        Instantiate(EnemyToSpawn, spawningObjects[obj].gameObject.transform.position, spawningObjects[obj].gameObject.transform.rotation);
        //update and reset
        enemiesspawned++;
        currentTimeBetweenSpawns = TimeBetweenSpawns;
        Debug.Log("Enemy Spawned");

        //check wave status
        if ( enemiesspawned >= spawncount)
        {
            WaveEnd();
        }
    }
}
