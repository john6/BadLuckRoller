using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPCDialogue: MonoBehaviour
{
    [SerializeField] private Text customText;
    [SerializeField] private float duration;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollision");
        if (other.gameObject.CompareTag("Dice"))
        {
            Debug.Log("Dice");
            customText.enabled = true;
            StartCoroutine(StayUp());
        }
    }

    private IEnumerator StayUp()
    {
        yield return new WaitForSeconds(duration);
        customText.enabled = false;
    }

    //void OnCollisionExit(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Dice"))
    //    {
    //        customText.enabled = false;
    //    }
    //}

}
