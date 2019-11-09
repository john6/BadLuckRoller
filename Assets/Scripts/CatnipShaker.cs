﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatnipShaker : MonoBehaviour
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

            BlackCat[] cats = FindObjectsOfType<BlackCat>();
            for (int i = 0; i < cats.Length; ++i)
            {
                cats[i].MoveTo(transform.position);
            }
        }
    }

    private bool IsTooTilted()
    {
        float dot = Vector3.Dot(Vector3.up, transform.up);
        return dot < tiltThreshold;
    }
}