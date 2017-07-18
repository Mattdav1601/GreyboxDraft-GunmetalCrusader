using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exploder.Utils;

public class TakeDamage : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
        {
            OnDeath();
        }
	}

    void OnDeath()
    {
        ExploderSingleton.Instance.ExplodeObject(gameObject);
        //should have exploded
    }
}
