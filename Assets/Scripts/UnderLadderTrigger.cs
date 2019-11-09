using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderLadderTrigger : LuckObject
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Dice"))
        {
            AlterLuck();
        }
    }
}
