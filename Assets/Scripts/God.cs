using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Exploder.Utils;

public class God : MonoBehaviour {


    [SerializeField]
    private GameObject Shop;

    [SerializeField]
    private GameObject[] ShopSpots;


    private bool GameStarted;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

	}

    public void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ShopPlacement()
    {
        int obj = (int)Random.Range(1, ShopSpots.Length);
        //Find a game object to spawn at
        Instantiate(Shop, ShopSpots[obj].gameObject.transform.position, ShopSpots[obj].gameObject.transform.rotation);
    }

    public void ShopDestroy()
    {
        GameObject ToExplode = GameObject.FindGameObjectWithTag("Shop");
        ExploderSingleton.Instance.ExplodeObject(ToExplode);
    }
}
