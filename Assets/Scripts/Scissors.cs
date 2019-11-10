using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : LuckObject
{
    [SerializeField] private Transform blade1;
    [SerializeField] private Transform blade2;
    [SerializeField] private Vector3 openAngle1;
    [SerializeField] private Vector3 openAngle2;
    [SerializeField] private Vector3 closedAngle1;
    [SerializeField] private Vector3 closedAngle2;
    [SerializeField] private Vector3 bounceVelocity;

    private bool m_open;
    public bool open
    {
        get { return m_open; }
        set
        {
            m_open = value;
            blade1.rotation = Quaternion.Euler(value ? openAngle1 : closedAngle1);
            blade2.rotation = Quaternion.Euler(value ? openAngle2 : closedAngle2);
            AudioManager.instance.Play("Scissors");
            AudioManager.instance.Play("Bread");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            open = !open;
            if (!open)
            {
                AlterLuck();
            }

            collision.rigidbody.velocity = bounceVelocity;
        }
    }
}
