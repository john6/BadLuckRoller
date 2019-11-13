using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : LuckObject
{
    [SerializeField] private GameObject closedObject;
    [SerializeField] private GameObject openObject;

    private bool open;

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            if (!open)
            {
                open = true;
                closedObject.SetActive(false);
                openObject.SetActive(true);
                AudioManager.instance.Play("Umbrella");
            }
        }
    }
}
