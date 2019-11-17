using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float maxPitch;
    [SerializeField] private float minPitch;
    [SerializeField] private float chargeSpeed = 3f;
    [SerializeField] private float maxLaunchSpeed = 25;
    [SerializeField] private Image chargeMeter;
    [SerializeField] private float spread;
    [SerializeField] private GameObject die;
    [SerializeField] private Transform dieSpawnPosition;
    //[SerializeField] DieCamera dieCamera;

    private float currLaunchSpeed;
    private Transform camera;
    private float pitch;

    public void Start()
    {
        pitch = 0f;
        camera = GetComponentInChildren<Camera>().transform;

        // if (dieCamera == null)
        // {
        //     dieCamera = GameObject.FindGameObjectsWithTag("DieCamera")[0].GetComponent<DieCamera>();
        // }

        Cursor.lockState = CursorLockMode.Locked;
        currLaunchSpeed = 0;
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        float yaw = transform.eulerAngles.y;
        yaw += x * lookSensitivity;

        pitch -= y * lookSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(0, yaw, 0f);
        camera.rotation = Quaternion.Euler(pitch, camera.eulerAngles.y, 0f);

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
            launchCharge += chargeSpeed * Time.deltaTime;
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
        //dieCamera.AttachToDie(obj1);
        Vector3 velocity = camera.forward * (currLaunchSpeed + Random.Range(-spread, spread));
        body.velocity = velocity;

        GameObject obj2 = Instantiate(die, dieSpawnPosition.position - transform.right * 0.25f, dieSpawnPosition.rotation);
        body = obj2.GetComponent<Rigidbody>();
        velocity = camera.forward * (currLaunchSpeed + Random.Range(-spread, spread));
        body.velocity = velocity;

        AudioManager.instance.Play("DiceShake");
        chargeMeter.fillAmount = 0;
        GameManager.instance.OnDiceThrown(this, obj1, obj2);
    }
}
