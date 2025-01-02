using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera mainCam;
    CinemachineOrbitalTransposer orbitalTransposer;
    public float turnAngle;

    // Start is called before the first frame update
    void Start()
    {
        orbitalTransposer = mainCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (orbitalTransposer != null)
        {
            //攝影機旋轉
            if (Input.GetKey(KeyCode.Q))
            {
                orbitalTransposer.m_XAxis.Value += turnAngle*Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                orbitalTransposer.m_XAxis.Value -= turnAngle*Time.deltaTime;
            }
        }

    }
}
