using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltShaker : LuckObject
{
    [SerializeField] private GameObject spillParticle;
    [SerializeField] private Transform spillSpawnPoint;
    [SerializeField] private float tiltThreshold;

    private bool hasSpilled;

    void Update()
    {
        if (!hasSpilled && IsTooTilted())
        {
            hasSpilled = true;
            Instantiate(spillParticle, spillSpawnPoint.position, Quaternion.identity);
            AlterLuck();
        }
    }

    private bool IsTooTilted()
    {
        float dot = Vector3.Dot(Vector3.up, transform.up);
        return dot < tiltThreshold;
    }
}
