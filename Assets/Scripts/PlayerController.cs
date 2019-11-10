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
    [SerializeField] private float currLaunchCharge;
    [SerializeField] private float maxLaunchSpeed;
    [SerializeField] private Image chargeMeter;
    [SerializeField] private float spread;
    [SerializeField] private GameObject die;
    [SerializeField] private Transform dieSpawnPosition;
    [SerializeField] DieCamera dieCamera;

    public void Start()
    {

        dieCamera = GameObject.FindGameObjectsWithTag("DieCamera")[0].GetComponent<DieCamera>();
        Cursor.lockState = CursorLockMode.Locked;
        currLaunchSpeed = 0;
        currLaunchCharge = 0.1f;
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
            launchCharge += currLaunchCharge;
            currLaunchSpeed = maxLaunchSpeed * ((Mathf.Cos(launchCharge) + 1) / 2);
            chargeMeter.fillAmount = currLaunchSpeed / maxLaunchSpeed;
            yield return null;
        }
        Launch();
    }

    private void Launch()
    {
        GameObject obj1 = Instantiate(die, dieSpawnPosition.position, dieSpawnPosition.rotation);
        Rigidbody body = obj1.GetComponent<Rigidbody>();
        dieCamera.AttachToDie(obj1);
        Vector3 velocity = transform.forward * (currLaunchSpeed + Random.Range(-spread, spread));
        body.velocity = velocity;

        GameObject obj2 = Instantiate(die, dieSpawnPosition.position - transform.right * 0.25f, dieSpawnPosition.rotation);
        body = obj2.GetComponent<Rigidbody>();
        velocity = transform.forward * (currLaunchSpeed + Random.Range(-spread, spread));
        body.velocity = velocity;
        chargeMeter.fillAmount = 0;
        GameManager.instance.OnDiceThrown(this, obj1, obj2);
    }
}
