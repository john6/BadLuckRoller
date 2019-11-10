using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] Camera DieCamera;
    [SerializeField] private float distanceFromObject = 2f;

    public void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        DieCamera = GameObject.FindGameObjectsWithTag("DieCamera")[0].GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookOnObject = transform.position - DieCamera.transform.position;
        DieCamera.transform.forward = lookOnObject.normalized;
        Vector3 dieLastPosition = transform.position - lookOnObject.normalized * distanceFromObject;
        dieLastPosition.y = transform.position.y + distanceFromObject / 2;
        DieCamera.transform.position = dieLastPosition;
    }

    public void ViewFollowDie()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        DieCamera = GameObject.FindGameObjectsWithTag("DieCamera")[0].GetComponent<Camera>();
        DieCamera.enabled = true;
        //gm.DeActivateMainCamera();
    }



}