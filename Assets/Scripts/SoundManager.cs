using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    public AudioClip[] Songs;
    public AudioSource audio;


    void Start()
    {
        if (!audio.playOnAwake)
        {
            audio.clip = Songs[Random.Range(0, Songs.Length)];
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            audio.clip = Songs[Random.Range(0, Songs.Length)];
            audio.Play();
        }
    }


    



}
