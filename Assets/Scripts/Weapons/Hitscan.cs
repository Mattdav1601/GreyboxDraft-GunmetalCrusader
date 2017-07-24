using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour {

    Vector3 TrailEndPoint;

    public float SMGDamage;

    private SoundManager sound;
   // public GameObject BulletImpact;

    LineRenderer trail;

    public float TrailLifeTime = 1.0f;

    // Use this for initialization
    void Start () {
        sound = FindObjectOfType<SoundManager>();
        sound.SMGFIre();
        trail = GetComponent<LineRenderer>();
        RaycastHit hit;

        if (Physics.Raycast(transform.position, this.transform.forward, out hit))
        {
            TrailEndPoint = hit.point;
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(SMGDamage);
               // Instantiate(BulletImpact, hit.point, Quaternion.identity);
            }
            // Do code here
        }

        trail.SetPosition(0, this.transform.position);
        trail.SetPosition(1, hit.point);

        Destroy(this.gameObject, TrailLifeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
