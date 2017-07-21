using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour {

    Vector3 TrailEndPoint;

    public float SMGDamage;

    LineRenderer trail;

    public float TrailLifeTime = 1.0f;

    // Use this for initialization
    void Start () {
        trail = GetComponent<LineRenderer>();
        RaycastHit hit;

        if (Physics.Raycast(transform.position, this.transform.forward, out hit))
        {
            TrailEndPoint = hit.point;
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(SMGDamage);
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
