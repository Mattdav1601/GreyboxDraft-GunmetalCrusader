using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    /// <summary>
    /// The purpose of the WaveManager script is now only to care 
    /// about telling everyone when a round has started and/or ended.
    /// </summary>
    /// 

    public AnimationCurve enemyHealthCurve;
    
    //Enemy strength variables
    [Tooltip("What is the maximum health the enemy will have at endgame?")]
    public float maximumEnemyHealth;
    float roundEnemyHealth;

    public AnimationCurve enemySpeedCurve;

    [Tooltip("What is the maximum speed the enemy will have at endgame?")]
    public float maximumEnemySpeed;
    float roundEnemySpeed;

    public AnimationCurve enemyDamageCurve;

    [Tooltip("What is the maximum amount of damage the enemy will deal at endgame")]
    public float maximumEnemyDamage;
    float roundEnemyDamage;

    [Tooltip("Used to manage how many enemies will spawn the entirety of this round")]
    public AnimationCurve enemyRoundTotalCurve;

    [Tooltip("What is the total number of enemies you want spawning at endgame?")]
    public int maximumEnemyTotal;
    int roundEnemyTotal;
    int deadEnemyCount;

    public AnimationCurve RoundCooldownCurve;

    [Tooltip("What is the maximum amount of time you want between rounds?")]
    public float maximumRoundCooldownTime;
    float roundCooldownTime;
    float roundCountdown;

    [Tooltip("What is the maximum number of rounds for this game?")]
    public int maximumRoundNumber;
    int roundNumber;

    //One packet to be altered and sent as data for other classes
    OnRoundStartPacket roundStartPacket;

    //Gamemanager reference
    GameManager gameManager;

    private void Start()
    {
        EventManager.instance.OnEnemyDeath.AddListener(EnemyDeathCheck);

        if (maximumRoundNumber == 0)
            maximumRoundNumber = 10;
        //Initialise some stuff;
        if (!(gameManager = FindObjectOfType<GameManager>()))
            Debug.LogError("There is no Game Manager present on the scene!");

        //Shorten the time to the start of the first round
        roundCountdown = roundCooldownTime / 4;
    }

    private void Update()
    {
        RoundStarter();
    }

    public int getRoundNum()
    {
        return roundNumber;
    }

    //Count down and start the round when asked
    void RoundStarter()
    {
        if (roundCountdown <= 0)
        {
            ++roundNumber;

            ValueUpdateCheck();

            //Create a packet of data to send to other classes for use
            OnRoundStartPacket onStartPack = new OnRoundStartPacket();

            ///ASSIGN ONSTART PACKS VARIABLES HERE
            onStartPack.enemySpeed = roundEnemySpeed;
            onStartPack.enemyMaxHealth = roundEnemyHealth;
            onStartPack.enemyDamage = roundEnemyDamage;

            EventManager.instance.OnRoundStart.Invoke(onStartPack);
        }
        else        
            //Countdown on timer
            roundCountdown -= Time.deltaTime;
    }

    //Perform checks to end the round
    void EnemyDeathCheck()
    {
        //Add to the currnt enemy death counter
        ++deadEnemyCount;

        //If the dead enemy count is equal to the total number of enemies possible this round, end the round
        if(deadEnemyCount == roundEnemyTotal)
        {
            deadEnemyCount = 0;
            roundCountdown = roundCooldownTime;
            EventManager.instance.OnRoundEnd.Invoke();
        }
    }

    //Update this round's values
    void ValueUpdateCheck()
    {
        float percentageCompletion = (float)roundNumber / maximumRoundNumber;

        //Set enemy values for this round
        roundEnemyHealth = maximumEnemyHealth * enemyHealthCurve.Evaluate(percentageCompletion);
        roundEnemyDamage = maximumEnemyDamage * enemyDamageCurve.Evaluate(percentageCompletion);
        roundEnemySpeed = maximumEnemySpeed * enemySpeedCurve.Evaluate(percentageCompletion);
        roundEnemyTotal = (int)(maximumEnemyTotal * enemyRoundTotalCurve.Evaluate(percentageCompletion));
    }



   // private God _GodScript;

   // public List<GameObject> spawningObjects = new List<GameObject>();

   // private float enemiesspawned;
   // public float enemiesalive;

   // private float waveCount;
   // public float TimeBetweenWaves;
   // private float currentTimeBetweenWaves;

   // public float TimeBetweenSpawns;
   // public float SpawnTimeDecreasePerRound;
   // private float currentTimeBetweenSpawns;

   // //make sure things only happen once
   // private bool calledwaveend = false;
   // private bool calledtensecondwarning = false;
   // private bool calledstartwarning = false;

   // private bool MaxEnemiesSpawned = false;

   // //  public float maxspawndistance;

   // public GameObject EnemyToSpawn;

   // public GameObject Player;

   // public SoundManager soundcontrol;

   // private bool DownTime = true;
   // // Use this for initialization
   ///* void Start() {
   //     currentTimeBetweenSpawns = TimeBetweenSpawns;
   //     currentTimeBetweenWaves = TimeBetweenWaves;
   //     _GodScript = FindObjectOfType<God>();
   // }

   // // Update is called once per frame
   // void Update()
   // {

   //     DownTimeChecks();
   //     TimeCheckBetweenWaves();

   //     if (DownTime == false)
   //     {
   //         if (currentTimeBetweenSpawns <= 0 && !MaxEnemiesSpawned)
   //         SpawnEnemies();

   //     }

   //     EnemyChecks();
   // }*/
   
   // //run down the timers for waves and spawners where appriopriate
   // void DownTimeChecks()
   // {
   //     if (DownTime == true)
   //     {
   //         currentTimeBetweenWaves -= Time.deltaTime;
   //         if (currentTimeBetweenWaves <= 11 && !calledtensecondwarning)
   //         {
   //             soundcontrol.TenSecondsLeft();
   //             calledtensecondwarning = true;
   //         }
   //         if (currentTimeBetweenWaves <= 3 && !calledstartwarning)
   //         {
   //             soundcontrol.RoundStarted();
   //             calledstartwarning = true;
   //         }

   //     }

   //     else
   //         currentTimeBetweenSpawns -= Time.deltaTime;
   // }

   // void TimeCheckBetweenWaves()
   // {
   //     if (currentTimeBetweenWaves <= 0)
   //     {
   //         NextWave();
   //     }
   // }


   // //are enemies all dead? and did we spawn them all?
   // void EnemyChecks()
   // {
   //     if (MaxEnemiesSpawned && enemiesalive == 0)
   //     {
   //         if (!calledwaveend)
   //         WaveEnd();
   //         calledwaveend = true;
   //     }
   // }
   // //okay the the wave. 
   // void WaveEnd()
   // {
   //     _GodScript.ShopPlacement();
   //     DownTime = true;
   //     soundcontrol.EndSoundTrack();
   //     soundcontrol.RoundEnded();
   //    // Debug.Log("Wave ended");
   // }


   // void  NextWave()
   // {
   //     if (waveCount <= 10)
   //     {
   //         waveCount++;
            
   //         spawncount = (BaseSpawnAmount * waveCount);

   //         TimeBetweenSpawns -= SpawnTimeDecreasePerRound;

   //         Debug.Log(spawncount);
   //         enemiesspawned = 0;
   //         DownTime = false;
   //         soundcontrol.StartSoundTrack();
   //         currentTimeBetweenWaves = TimeBetweenWaves;

   //         //reset one time bools
   //         MaxEnemiesSpawned = false;
   //         calledwaveend = false;
   //         calledtensecondwarning = false;
   //         calledstartwarning = false;

   //         _GodScript.ShopDestroy();

   //      //   Debug.Log("Next wave");
   //     }
       
   // }


   // void SpawnEnemies()
   // {

   //     int obj = (int)Random.Range(1, spawningObjects.Count);
   //     //Find a game object to spawn at
   //     Instantiate(EnemyToSpawn, spawningObjects[obj].gameObject.transform.position, spawningObjects[obj].gameObject.transform.rotation);

   //     //update and reset
   //     enemiesspawned++;
   //     enemiesalive++;
   //     currentTimeBetweenSpawns = TimeBetweenSpawns;
   //   //  Debug.Log("Enemy Spawned");

   //     //check max enemy status
   //     if ( enemiesspawned >= spawncount)
   //     {
   //         MaxEnemiesSpawned = true;
   //     }
   // }
}
