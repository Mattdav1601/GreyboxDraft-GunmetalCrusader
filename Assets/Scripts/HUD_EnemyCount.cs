using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_EnemyCount : MonoBehaviour {

    TextMesh t;

	// Use this for initialization
	void Start () {
        t = this.GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        int num = 0;/*
        foreach (Enemy e in FindObjectsOfType<Enemy>())
            num++;
        t.text = num.ToString();*/
    }
}
