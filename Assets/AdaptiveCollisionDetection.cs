using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveCollisionDetection : MonoBehaviour {

    [SerializeField]
    float ContinuousCollisionMinVelocity;

    Rigidbody2D RigidBody;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate ()
    {
		if ((RigidBody.velocity.magnitude > ContinuousCollisionMinVelocity) == (RigidBody.collisionDetectionMode != CollisionDetectionMode2D.Continuous))
        {
            RigidBody.collisionDetectionMode = RigidBody.velocity.magnitude > ContinuousCollisionMinVelocity ? CollisionDetectionMode2D.Continuous : CollisionDetectionMode2D.Discrete;
        }
	}
}
