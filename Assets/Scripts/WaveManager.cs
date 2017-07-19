using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    private float waveCount;
    public float TimeBetweenWaves;
    private float currentTimeBetweenWaves;
  //  public GameObject[] SpawnPoints;

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
	void Start () {

        NextWave();
    }

    // Update is called once per frame
    void Update ()
    {

     if (DownTime == true)
        {
            currentTimeBetweenWaves -= Time.deltaTime;
        }
     if (currentTimeBetweenWaves <= 0)
        {
            NextWave();
        }



        currentTimeBetweenSpawns -= Time.deltaTime;
        if (currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemies();
            currentTimeBetweenSpawns = TimeBetweenSpawns;
        }

        if (enemiesspawned >= spawncount)
        {
            WaveEnd();
        }
      
	}


    void WaveEnd()
    {

        DownTime = true;
    }


    void  NextWave()
    {
        DownTime = false;
        currentTimeBetweenWaves = TimeBetweenWaves;

        spawncount = BaseSpawnAmount * (waveCount * SpawnMultiplier);

    }


    void SpawnEnemies()
    {
       //foreach(GameObject Point in SpawnPoints)
       // {
       //     float Dist = Vector3.Distance(Player.transform.position, Point.transform.position);
       //     if (Dist < maxspawndistance)
       //     {
       //         Instantiate(EnemyToSpawn, Point.transform.position, Quaternion.identity);
       //     }
       // }
        enemiesspawned++;
    }
}
