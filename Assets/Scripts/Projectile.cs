using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Tooltip("SPEEEEEEEEEEEEEEEEEEEEEEDOOWWAAGOOOOOOOOOOOOOOOOOOOOOOONNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN!!!!!!!!!")]
    public float ProjSpeed = 5.0f;

    public float ProjectileLifeTime = 3.0f;

    public float GrenadeDirectDamage;

    public float GrenadeSplashDamage;
    public float grenadepushback;

    public GameObject explosionIndicator;

    private bool exploded = false;

    private void Start()
    {
        Destroy(this.gameObject, ProjectileLifeTime);
        
    }

    // Update is called once per frame
    void Update () {
        this.transform.position += this.transform.forward * (ProjSpeed * Time.deltaTime);
	}

    void Splash()
    {
        //splash code
       
       
        Vector3 explosionPosition = transform.position;
        float explosionRadius = 5;

        explosionIndicator.transform.localScale =  new Vector3(explosionRadius / 2, explosionRadius / 2, explosionRadius / 2);

             Instantiate(explosionIndicator, transform.position, Quaternion.identity);

        if (exploded == false)
        {
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

            foreach (Collider col in colliders)
            {
                if (col.tag == "Enemy")
                {
                   col.GetComponent<Enemy>().floating = true;
                   col.GetComponent<Enemy>().TakeDamage(GrenadeSplashDamage);

                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.AddExplosionForce(grenadepushback, explosionPosition, explosionRadius, 15.0F);
                    
                }
            }
            Debug.Log("Exploded");
            exploded = true;
        }
        

        Destroy(this.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit shit");
        Splash();
        //if (other.tag == "Enemy")
        //{
        //    other.GetComponent<Enemy>().TakeDamage(GrenadeDirectDamage);
        //}

      

    }
}
