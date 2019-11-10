using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : LuckObject
{
    [SerializeField] private float flipForce;
    [SerializeField] private Vector3 bounceVelocity;

    private Rigidbody body;
    private Collider collider;
    private bool flipped;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            Bounds b = collider.bounds;
            Vector3 v = new Vector3(b.min.x, transform.position.y, transform.position.z);
            body.AddForceAtPosition(Vector3.up * flipForce, v, ForceMode.VelocityChange);
            collision.rigidbody.velocity = bounceVelocity;
        }
    }

    void Update()
    {
        if (!flipped && Vector3.Dot(transform.up, Vector3.up) < 0)
        {
            flipped = true;
            AlterLuck();
        }
    }
}
