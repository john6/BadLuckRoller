using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float maxPitch;
    [SerializeField] private float minPitch;
    [SerializeField] private float currLaunchSpeed;
    [SerializeField] private float maxLaunchSpeed;
    [SerializeField] private GameObject ChargeMeter;
    [SerializeField] private float spread;
    [SerializeField] private GameObject die;
    [SerializeField] private Transform dieSpawnPosition;


    public void Start()
    {
        currLaunchSpeed = 0;
        maxLaunchSpeed = 25;
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
            StartCoroutine(ChargeThrow());
        }
    }

    private IEnumerator ChargeThrow()
    {
        float launchCharge = 3.14f;
        while (!Input.GetButtonUp("Fire1"))
        {
            launchCharge += 0.1f;
            currLaunchSpeed = maxLaunchSpeed * ((Mathf.Cos(launchCharge) + 1) / 2);
            ChargeMeter.GetComponent<Text>().text = "Charge At " + Mathf.RoundToInt((currLaunchSpeed / maxLaunchSpeed) * 100) + "%";
            yield return null;
        }
        Launch();
    }

    private void Launch()
    {
        GameObject obj = Instantiate(die, dieSpawnPosition.position, dieSpawnPosition.rotation);
        //obj.GetComponent<Die>().ViewFollowDie();
        Rigidbody body = obj.GetComponent<Rigidbody>();
        Vector3 velocity = transform.forward * (currLaunchSpeed + Random.Range(-spread, spread));
        body.velocity = velocity;

        obj = Instantiate(die, dieSpawnPosition.position - transform.right * 0.25f, dieSpawnPosition.rotation);
        body = obj.GetComponent<Rigidbody>();
        velocity = transform.forward * (currLaunchSpeed + Random.Range(-spread, spread));
        body.velocity = velocity;
        ChargeMeter.GetComponent<Text>().text = "Charge At 0%";
    }
}
