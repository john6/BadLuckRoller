using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishBone : LuckObject
{
    [SerializeField] private GameObject explosionParticle;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            Destroy(gameObject);
            AlterLuck();
            if (explosionParticle != null)
            {
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
            }
        }
    }
}
