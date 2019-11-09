using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCat : LuckObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("CatBadLuckZone"))
        {
            AlterLuck();
        }
    }

    public void MoveTo(Vector3 position)
    {
        StartCoroutine(MoveToImpl(position));
    }

    private IEnumerator MoveToImpl(Vector3 position)
    {
        animator.SetBool("Moving", true);

        while (Vector3.Distance(transform.position, position) > stopDistance)
        {
            transform.position += (position - transform.position).normalized * moveSpeed * Time.deltaTime;
            yield return null;
        }

        animator.SetBool("Moving", false);
    }
}
