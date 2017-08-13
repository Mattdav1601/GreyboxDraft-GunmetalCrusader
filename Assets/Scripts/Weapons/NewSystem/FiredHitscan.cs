using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredHitscan : FiredObject {

    // How long should our damage ray be?
    [Tooltip("How long should our damage ray be?")]
    [SerializeField]
    private float scanRange = 512.0f;

    /*
     * Called on instance create. Override to check ray damage;
     */
    protected override void Start()
    {
        base.Start();

        doRaycastHit();
    }

    /*
     * Casts a ray from the spawn location and checks if we hit any enemies.
     */
    protected virtual void doRaycastHit()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, this.transform.forward, out hit, scanRange))
        {
            switch(hit.collider.tag)
            {
                case "Enemy":
                    {
                        // TODO: Add headshot support
                        hitEnemy(hit.collider.gameObject, 1.0f);
                        break;
                    }
            }
        }
    }
}
