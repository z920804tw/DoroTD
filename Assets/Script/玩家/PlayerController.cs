using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("玩家控制設定")]
    public GameObject forwardDir;
    public GameObject playerFace;

    public float moveSpeed;

    [Header("Debug")]
    public bool isAim;


    GameObject mainCam;
    Vector3 moveDirection;


    InputMap inputAction;
    [SerializeField] Rigidbody rb;
    private void Awake()
    {
        inputAction = new InputMap();
    }
    private void OnEnable()
    {
        inputAction.PlayerInput.Enable();
    }
    private void OnDisable()
    {
        inputAction.PlayerInput.Disable();
    }
    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").gameObject;



    }
    void Update()
    {
        forwardDir.transform.localEulerAngles = new Vector3(0, mainCam.transform.eulerAngles.y, 0);

        //瞄準模式
        if (Input.GetKey(KeyCode.Mouse1))
        {
            isAim = true;
            //當滑鼠右鍵按住時，角色會面向滑鼠只到的位置
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(mousePos, out hit, 100))
            {

                //turnPonit=玩家在畫面打到的點  turnDir=打到的點與玩家正面的方向
                Vector3 turnPoint = new Vector3(hit.point.x, playerFace.transform.position.y, hit.point.z);
                Vector3 turnDir = turnPoint - playerFace.transform.position;



                //平滑轉動到滑鼠位置
                Quaternion targetRotation = Quaternion.LookRotation(turnDir);
                playerFace.transform.localRotation = Quaternion.Slerp(playerFace.transform.localRotation, targetRotation, Time.deltaTime * 4);

                Debug.DrawRay(playerFace.transform.position, turnDir, Color.red);
            }

        }
        else
        {
            PlayerTurnFace();
            isAim = false;
        }


    }
    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        var cbt = inputAction.PlayerInput.PlayerMove;
        float horizontalInput = cbt.ReadValue<Vector3>().x;
        float verticalInput = cbt.ReadValue<Vector3>().z;

        // Debug.Log($"左右:{cbt.ReadValue<Vector3>().x},前後:{cbt.ReadValue<Vector3>().z}");

        moveDirection = forwardDir.transform.forward * verticalInput + forwardDir.transform.right * horizontalInput;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        if (moveDirection.magnitude > 0.1f && rb.velocity.magnitude < 5)
        {
            rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
        }
    }


    void PlayerTurnFace()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            // 計算角色目標角度
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            // 更新playerFace的旋轉
            playerFace.transform.localRotation = Quaternion.Slerp(playerFace.transform.localRotation, targetRotation, Time.deltaTime * 2f);

            // Debug.Log($"移動方向: {moveDirection}, 角度: {targetAngle}");
        }
    }




}
