using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager : MonoBehaviour {


    //Should be called when the options are changed and saved
    public static void SaveOptions(Options _o)
    {
        //Make new stuff
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.dataPath + "/GameOptions.opt", FileMode.OpenOrCreate);
        OptionsData od = new OptionsData(_o);

        //serialize and cclose the stream
        bf.Serialize(stream, od);
        stream.Close();
    }

    //Called whenever the options need to be set (preferrably at the start of the game)
    public static OptionsData LoadOptions()
    {
        //Check if the game files contain the options file
        if(File.Exists(Application.dataPath + "/GameOptions.opt"))
        {
            //Make IO stuff
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.dataPath + "/GameOptions.opt", FileMode.Open);
            OptionsData o = bf.Deserialize(stream) as OptionsData;

            //Close and return
            stream.Close();
            return o;
        }
        else
        { 
            //Else return the default options
            Debug.Log("There was no options file on startup");
            return new OptionsData(new Options());
        }
    }
}

[Serializable]
public class OptionsData
{
    public float[] options;

    //Assign all floats in options
    public OptionsData(Options _options)
    {
        options = new float[8];
        options[0] = _options.brightness;
        options[1] = _options.contrast;
        options[2] = _options.masterVolume;
        options[3] = _options.musicVolume;
        options[4] = _options.voiceVolume;
        options[5] = _options.soundEffectsVolume;
        options[6] = _options.lookingAngle;
        options[7] = _options.lookingSpeed;
    }
}
