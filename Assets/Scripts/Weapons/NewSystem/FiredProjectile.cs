using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class FiredProjectile : FiredObject {

    // How far does this projectile move every second?
    [Tooltip("How far does this projectile move every second?")]
    [SerializeField]
    protected float moveSpeed = 3.0f;

    // How far does this projectile move every second?
    [Tooltip("How far does this projectile rotate every second?")]
    [SerializeField]
    protected float rotateSpeed = 0.25f;

    private Rigidbody rb;

    /*
     * Called on instance create
     */
    protected override void Start()
    {
        base.Start();

        rb = this.GetComponent<Rigidbody>();
        this.GetComponent<Collider>().isTrigger = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    /*
	 * Called once per frame.
	 */
    protected override void Update () {
        rb.transform.Translate(this.transform.forward * (moveSpeed * Time.deltaTime));
        this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0,0, (rotateSpeed * Time.deltaTime));
	}

    /*
     * Called on impact
     */
    protected virtual void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Enemy":
                {
                    // TODO: Add headshot support
                    hitEnemy(other.gameObject, 1.0f);
                    break;
                }
        }
        splashEnemy(other.gameObject, this.transform.position);

        Instantiate(impactEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
