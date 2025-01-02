using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamZone : MonoBehaviour
{

    public CinemachineVirtualCamera camZoom;
    // Start is called before the first frame update
    void Start()
    {
        camZoom.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            camZoom.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            camZoom.enabled=false;
        }
    }
}
