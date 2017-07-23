using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public GameObject Player;
    public GameObject PlayerLeftGun;
    public GameObject PlayerRightGun;


    public float ResupplyDistance;

    private float dist;

    private bool resupplied = false;


	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerLeftGun = GameObject.Find("SMG");
        PlayerRightGun = GameObject.Find("Grenade Launcher");

    }
	
	// Update is called once per frame
	void Update ()
    {
        dist = Vector3.Distance(this.transform.position, Player.transform.position);
            
        if (dist <= ResupplyDistance && resupplied)
        {
            PlayerRightGun.GetComponent<MechaWeaponBehaviour>().FullRestock();
            PlayerLeftGun.GetComponent<MechaWeaponBehaviour>().FullRestock();
            Debug.Log("Should have restocked both guns");
            Player.GetComponent<PlayerHealth>().FullHeal();
            resupplied = true;
        }

        
	}


}
