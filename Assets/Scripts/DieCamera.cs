using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject currDie;
    [SerializeField] private bool viewingDie;
    [SerializeField] private Vector3 changeViewThreshold = new Vector3(1.5f,1.5f,1.5f);
    [SerializeField] private int distanceFromObject = 2;
    [SerializeField] private float smoothing = 1;

    public void Awake()
    {
        camera = gameObject.GetComponent<Camera>();
        camera.enabled = false;
        viewingDie = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (viewingDie)
        {
            Time.timeScale = .4f;
            Vector3 lookOnObject = currDie.transform.position - transform.position;
            transform.forward = lookOnObject.normalized;
            Vector3 dieLastPosition = currDie.transform.position - lookOnObject.normalized * distanceFromObject;
            Vector3 velocity = currDie.GetComponent<Rigidbody>().velocity;
            if (velocity.magnitude <= changeViewThreshold.magnitude)
            {
                dieLastPosition.y = currDie.transform.position.y + distanceFromObject / 2;
                transform.position = Vector3.Lerp(transform.position, dieLastPosition, Time.deltaTime * smoothing);
            }
            else
            {
                dieLastPosition.y = currDie.transform.position.y + distanceFromObject * 2;
                transform.position = Vector3.Lerp(transform.position, dieLastPosition, Time.deltaTime * smoothing);
            }
           if (currDie.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                Time.timeScale = 1f;
                viewingDie = false;
                camera.enabled = false;
                currDie = null;
            }
        }
    }

    public void AttachToDie(GameObject dieObj)
    {
        currDie = dieObj;
        viewingDie = true;
        camera.enabled = true;
        camera.transform.position = Camera.main.transform.position;
    }
}
