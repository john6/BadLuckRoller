using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardToParent : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + ": OnCollisionEnter");
        transform.parent.gameObject.SendMessage("OnCollisionEnter", collision);
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log(gameObject.name + ": OnCollisionExit");
        transform.parent.gameObject.SendMessage("OnCollisionExit", collision);
    }
}
