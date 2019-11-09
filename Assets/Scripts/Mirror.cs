using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : LuckObject
{
    [SerializeField] private Material unbrokenMaterial;
    [SerializeField] private Material brokenMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            Break();
        }
    }

    private void Break()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = brokenMaterial;
        AlterLuck();
    }
}
