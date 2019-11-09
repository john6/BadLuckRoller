using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float maxPitch;
    [SerializeField] private float minPitch;
    [SerializeField] private float launchSpeed;
    [SerializeField] private float spread;
    [SerializeField] private GameObject die;
    [SerializeField] private Transform dieSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        float yaw = transform.eulerAngles.y;
        yaw += x * lookSensitivity;

        float pitch = transform.eulerAngles.x;
        pitch -= Mathf.Clamp(y * lookSensitivity, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }
    }

    private void Launch()
    {
        GameObject obj = Instantiate(die, dieSpawnPosition.position, dieSpawnPosition.rotation);
        Rigidbody body = obj.GetComponent<Rigidbody>();
        Vector3 velocity = transform.forward * launchSpeed;
        body.velocity = velocity;
    }
}
