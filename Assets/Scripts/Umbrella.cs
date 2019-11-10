using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : LuckObject
{
    [SerializeField] private Mesh openRenderMesh;
    [SerializeField] private Mesh openColliderMesh;
    [SerializeField] private Vector3 bounceVelocity;

    private bool open;

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            if (!open)
            {
                open = true;
                MeshFilter filter = GetComponent<MeshFilter>();
                filter.mesh = openRenderMesh;
                MeshCollider collider = GetComponent<MeshCollider>();
                collider.sharedMesh = openColliderMesh;
            }
            else
            {
                collision.rigidbody.velocity = bounceVelocity;
            }
        }
    }
}
