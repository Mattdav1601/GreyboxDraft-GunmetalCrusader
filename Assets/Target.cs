using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject Player;
    private float dist;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        dist = Vector3.Distance(Player.transform.position, this.transform.position);

            if (dist <= 10)
            {
            this.GetComponent<Renderer>().enabled = false;
             }

        else this.GetComponent<Renderer>().enabled = true;
    }
}
