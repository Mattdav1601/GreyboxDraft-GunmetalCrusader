using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEVTOOLS : MonoBehaviour {

    bool isPaused = false;
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k"))
        {
            if (FindObjectsOfType<Enemy>().Length > 0)
            {
                foreach(Enemy e in FindObjectsOfType<Enemy>())
                {
                    e.OnDeath();
                }
            }
        }

        //Hit R to reload the level
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Pause when P is pressed
        if (Input.GetKeyDown("p"))        
            isPaused = !isPaused;

        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
	}
}
