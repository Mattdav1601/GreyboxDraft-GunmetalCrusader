﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {


    private God _GodScript;

    public List<GameObject> spawningObjects = new List<GameObject>();


    private float spawncount;
    public float BaseSpawnAmount;
    public float SpawnMultiplier;


    private float enemiesspawned;
    public float enemiesalive;

    private float waveCount;
    public float TimeBetweenWaves;
    private float currentTimeBetweenWaves;

    public float TimeBetweenSpawns;
    public float SpawnTimeDecreasePerRound;
    private float currentTimeBetweenSpawns;

    //make sure things only happen once
    private bool calledwaveend = false;
    private bool calledtensecondwarning = false;
    private bool calledstartwarning = false;

    private bool MaxEnemiesSpawned = false;

    //  public float maxspawndistance;

    public GameObject EnemyToSpawn;

    public GameObject Player;

    public SoundManager soundcontrol;

    private bool DownTime = true;
    // Use this for initialization
    void Start() {
        currentTimeBetweenSpawns = TimeBetweenSpawns;
        currentTimeBetweenWaves = TimeBetweenWaves;
        _GodScript = FindObjectOfType<God>();
    }

    // Update is called once per frame
    void Update()
    {

        DownTimeChecks();
        TimeCheckBetweenWaves();

        if (DownTime == false)
        {
            if (currentTimeBetweenSpawns <= 0 && !MaxEnemiesSpawned)
            SpawnEnemies();

        }

        EnemyChecks();
    }
   
    //run down the timers for waves and spawners where appriopriate
    void DownTimeChecks()
    {
        if (DownTime == true)
        {
            currentTimeBetweenWaves -= Time.deltaTime;
            if (currentTimeBetweenWaves <= 11 && !calledtensecondwarning)
            {
                soundcontrol.TenSecondsLeft();
                calledtensecondwarning = true;
            }
            if (currentTimeBetweenWaves <= 3 && !calledstartwarning)
            {
                soundcontrol.RoundStarted();
                calledstartwarning = true;
            }

        }

        else
            currentTimeBetweenSpawns -= Time.deltaTime;
    }

    void TimeCheckBetweenWaves()
    {
        if (currentTimeBetweenWaves <= 0)
        {
            NextWave();
        }
    }


    //are enemies all dead? and did we spawn them all?
    void EnemyChecks()
    {
        if (MaxEnemiesSpawned && enemiesalive == 0)
        {
            if (!calledwaveend)
            WaveEnd();
            calledwaveend = true;
        }
    }
    //okay the the wave. 
    void WaveEnd()
    {
        _GodScript.ShopPlacement();
        DownTime = true;
        soundcontrol.EndSoundTrack();
        soundcontrol.RoundEnded();
       // Debug.Log("Wave ended");
    }


    void  NextWave()
    {
        if (waveCount <= 10)
        {
            waveCount++;
            
            spawncount = (BaseSpawnAmount * waveCount);

            TimeBetweenSpawns -= SpawnTimeDecreasePerRound;

            Debug.Log(spawncount);
            enemiesspawned = 0;
            DownTime = false;
            soundcontrol.StartSoundTrack();
            currentTimeBetweenWaves = TimeBetweenWaves;

            //reset one time bools
            MaxEnemiesSpawned = false;
            calledwaveend = false;
            calledtensecondwarning = false;
            calledstartwarning = false;

            _GodScript.ShopDestroy();

         //   Debug.Log("Next wave");
        }
       
    }


    void SpawnEnemies()
    {

        int obj = (int)Random.Range(1, spawningObjects.Count);
        //Find a game object to spawn at
        Instantiate(EnemyToSpawn, spawningObjects[obj].gameObject.transform.position, spawningObjects[obj].gameObject.transform.rotation);

        //update and reset
        enemiesspawned++;
        enemiesalive++;
        currentTimeBetweenSpawns = TimeBetweenSpawns;
      //  Debug.Log("Enemy Spawned");

        //check max enemy status
        if ( enemiesspawned >= spawncount)
        {
            MaxEnemiesSpawned = true;
        }
    }
}