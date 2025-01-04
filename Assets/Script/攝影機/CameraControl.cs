using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera mainCam;
    CinemachineOrbitalTransposer orbitalTransposer;
    public float turnAngle;

    InputMap inputActions;
    private void Awake()
    {
        inputActions = new InputMap();
    }
    private void OnEnable()
    {
        inputActions.CameraInput.Enable();
    }
    private void OnDisable()
    {
        inputActions.CameraInput.Disable(); 
    }
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
            if (inputActions.CameraInput.LeftRotateCam.ReadValue<float>()>0)
            {
                orbitalTransposer.m_XAxis.Value += turnAngle * Time.deltaTime;
            }
            else if (inputActions.CameraInput.RightRotateCam.ReadValue<float>()>0)
            {
                orbitalTransposer.m_XAxis.Value -= turnAngle * Time.deltaTime;
            }
        }

    }
}
