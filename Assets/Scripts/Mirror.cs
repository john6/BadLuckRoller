using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : LuckObject
{
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            Break(collision.contacts[0].point);
        }
    }

    private void Break(Vector3 point)
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < bodies.Length; ++i)
        {
            bodies[i].isKinematic = false;
            bodies[i].AddExplosionForce(explosionForce, point, explosionRadius);
        }

        AlterLuck();
    }
}
