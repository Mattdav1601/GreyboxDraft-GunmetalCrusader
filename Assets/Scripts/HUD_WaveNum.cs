using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_WaveNum : MonoBehaviour {

    TextMesh t;

    // Use this for initialization
    void Start()
    {
        t = this.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        t.text = FindObjectOfType<WaveManager>().getRoundNum().ToString();
    }
}
