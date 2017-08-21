using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_AmmoReadout : MonoBehaviour {

    public MechaWeaponInterfacePoint externalInterface;
    TextMesh t;

    // Use this for initialization
    void Start()
    {
        t = this.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        string ammo = externalInterface.field;
        t.text = ammo.ToString();
    }
}
