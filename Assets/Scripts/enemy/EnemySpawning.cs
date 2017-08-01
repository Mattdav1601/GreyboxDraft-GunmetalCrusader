using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    /// <summary>
    /// Enemy spawning manager should manage the spawning of enemies throughout the map
    /// </summary>

    //List of gameobjects that can be spawned from
    public List<GameObject> spawningObjects = new List<GameObject>();

    bool isSpawning;

    //Keep count of the current number of enemies on the map
    public int enemyCount;
    public int maxEnemyCount;

    //Keep tabs on when I'm meant to be spawning enemies
    public float spawningRate;
    float spawningCount;
            
    //Enemy Game object to spawn
    public GameObject enemyObject;

    //WaveManager
    WaveManager waveManager;

    //Enemy spawning values for this round
    float eHealth;
    float eSpeed;
    float eDamage;

	// Use this for initialization
	void Start () {
        //On round begin or end start or stop spawning enemies
        EventManager.instance.OnRoundStart.AddListener((p) => {
            isSpawning = true;
        });
        EventManager.instance.OnRoundEnd.AddListener(() =>
        {
            isSpawning = false;
        });
        EventManager.instance.OnEnemyDeath.AddListener(() => {
            --enemyCount;
        });

        if (!(waveManager = FindObjectOfType<WaveManager>()))
            Debug.LogError("There is no WaveManager present in the scene!");

        //Initialise spawning rate and count
        spawningCount = spawningRate;
	}
	
	// Update is called once per frame
	void Update () {
        //If the round is up, start spawning enemies
        if(isSpawning)
            SpawnEnemies();
	}

    void SpawnEnemies()
    {
        spawningCount -= Time.deltaTime;

        //When spawn count is zero, spawn an enemy
        if (spawningCount <= 0 && enemyCount < maxEnemyCount && spawningObjects.Count > 0)
        {
            int obj = (int)Random.Range(0, spawningObjects.Count);

            GameObject newEnemy = enemyObject;
            Enemy e = newEnemy.GetComponent<Enemy>();
            e.maxHealth = eHealth;
            e.moveSpeed = eSpeed;
            e.damage = eDamage;

            //Find a game object to spawn at
            Instantiate(newEnemy, spawningObjects[obj].gameObject.transform.position, spawningObjects[obj].gameObject.transform.rotation);
            ++enemyCount;

            //Reset spawning counter
            spawningCount = spawningRate;
        }
    }

    //Set the new spawning values to this
    void AlterSpawnValues(OnRoundStartPacket pack)
    {
        eHealth = pack.enemyMaxHealth;
        eSpeed = pack.enemySpeed;
        eDamage = pack.enemyDamage;
    }
}
