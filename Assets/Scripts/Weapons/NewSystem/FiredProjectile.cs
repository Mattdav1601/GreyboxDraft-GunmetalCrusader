using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class FiredProjectile : FiredObject {

    // How long this shot survives for before garbage collection.
    [Tooltip("How long this shot survives for before garbage collection.")]
    [SerializeField]
    private float lifeTime = 3.0f;

    // How far does this projectile move every second?
    [Tooltip("How far does this projectile move every second?")]
    [SerializeField]
    protected float moveSpeed = 3.0f;

    private Rigidbody rb;

    /*
     * Called on instance create
     */
    protected override void Start()
    {
        base.Start();

        rb = this.GetComponent<Rigidbody>();
        this.GetComponent<Collider>().isTrigger = true;
    }

    /*
	 * Called once per frame.
	 */
    protected override void Update () {
        rb.transform.Translate(this.transform.forward * (moveSpeed * Time.deltaTime));
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
        Destroy(other.gameObject);
    }
}
