using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperQuickSelfKiller : MonoBehaviour {

    // Use this for initialization
    private SoundManager sound;

    private AudioSource thisone;
    void Start () {
        sound = FindObjectOfType<SoundManager>();
        Destroy(this.gameObject, 0.8f);
        sound.GrenadeExplode(thisone);
        thisone = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
