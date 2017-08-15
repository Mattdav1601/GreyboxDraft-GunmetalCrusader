using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

    [Range(0, 2)]
    [Tooltip("Brightness of the game")]
    public float brightness = 1.0f;

    [Range(0, 2)]
    [Tooltip("Contrast of the game")]
    public float contrast = 1.0f;

    [Range(0, 1)]
    [Tooltip("Master Volume percentage (0-1)")]
    public float masterVolume = 1.0f;

    [Range(0, 1)]
    [Tooltip("Music Volume percentage (0-1)")]
    public float musicVolume = 1.0f;

    [Range(0, 1)]
    [Tooltip("Voice Volume percentage (0-1)")]
    public float voiceVolume = 1.0f;

    [Range(0, 1)]
    [Tooltip("SFX Volume percentage (0-1)")]
    public float soundEffectsVolume = 1.0f;

    [Range(0, 100)]
    [Tooltip("How far does the player need to look to rotate (Degrees)?")]
    public float lookingAngle = 65.0f;

    [Range(0, 50)]
    [Tooltip("How fast does the player rotate?")]
    public float lookingSpeed = 5.0f;

	// Use this for initialization
	void Start () {
        OptionsData o = SaveLoadManager.LoadOptions();
	}

    //Load in the options for the game
    void Load(OptionsData o)
    {
        brightness = o.options[0];
        contrast = o.options[1];
        masterVolume = o.options[2];
        musicVolume = o.options[3];
        voiceVolume = o.options[4];
        soundEffectsVolume = o.options[5];
        lookingAngle = o.options[6];
        lookingSpeed = o.options[7];
    }

    //Called to save the current options configuration
    void Save()
    {
        SaveLoadManager.SaveOptions(this);
    }
}
