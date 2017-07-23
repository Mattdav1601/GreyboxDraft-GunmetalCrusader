using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exploder.Utils;

public class PlayerHealth : MonoBehaviour {

    public God _GodScript;

    [SerializeField]
    private float Maxhealth;

    private float CurrentHealth;

    public GameObject RWep;
    public GameObject LWep;


	// Use this for initialization
	void Start ()
    {
        FullHeal();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void FullHeal()
    {
        CurrentHealth = Maxhealth;
    }


    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log("PlayerTookDamage");
        if (CurrentHealth <= 0)
        {
            Die();
            Debug.Log("PlayerDied");
        }
    }

    public void Die()
    {
        //me lol
        ExploderSingleton.Instance.ExplodeObject(RWep);
        ExploderSingleton.Instance.ExplodeObject(LWep);

        Invoke("TellGodWeDead", 3);
    }

    public void TellGodWeDead()
    {
        _GodScript.PlayerDied();
    }
}
