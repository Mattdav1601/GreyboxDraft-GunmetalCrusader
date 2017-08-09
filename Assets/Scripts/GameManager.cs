using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Keep track of the player's points
    float playerPoints = 0;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // IncreaePoints is called whenever the player should gain points
    public void IncreasePoints(float _points)
    {
        playerPoints += _points;
    }
}
