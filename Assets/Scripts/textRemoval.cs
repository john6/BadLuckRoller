using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textRemoval : MonoBehaviour
{
    public GameObject ObjectToBeRemoved;
    public ParticleSystem Particles;
    public Transform Explode;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ObjectToBeRemoved.SetActive(false);
        }
        if (ObjectToBeRemoved.activeSelf == false)
        {
            Particles.Play();
        }
    }

}

