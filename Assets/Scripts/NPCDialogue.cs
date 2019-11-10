using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPCDialogue: MonoBehaviour
{
    [SerializeField] private Text customText;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            customText.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            customText.enabled = false;
        }
    }

}
