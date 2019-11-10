using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : LuckObject
{
    [SerializeField] private Mesh openRenderMesh;
    [SerializeField] private Mesh openColliderMesh;
    [SerializeField] private PhysicMaterial openMaterial;

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
                collider.material = openMaterial;
                AudioManager.instance.Play("Umbrella");
            }
        }
    }
}
