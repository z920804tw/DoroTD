using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("玩家設定")]
    public GameObject forwardDir;
    public GameObject playerFace;
    GameObject mainCam;
    [SerializeField] float moveSpeed;
    [SerializeField] float aimSpeed;
    float preSpeed;
    [Header("Rig設定")]
    public Rig aimRig;
    [SerializeField] float aimTime;

    [Header("Debug")]
    [SerializeField] bool isAim;
    Vector3 moveDirection;
    PlayerStatus playerStatus;
    [SerializeField] Rigidbody rb;

    InputMap inputAction;

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
        playerStatus = GetComponent<PlayerStatus>();
        preSpeed = moveSpeed;

        aimRig.weight = 0;
    }
    void Update()
    {

        if (!playerStatus.IsDead)
        {
            forwardDir.transform.localEulerAngles = new Vector3(0, mainCam.transform.eulerAngles.y, 0);
            //瞄準模式
            if (Input.GetKey(KeyCode.Mouse1))
            {
                isAim = true;
                moveSpeed = aimSpeed;
                //當滑鼠右鍵按住時，角色會面向滑鼠只到的位置
                Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(mousePos, out hit, 100))
                {
                    //turnPonit=玩家在畫面打到的點  turnDir=打到的點與玩家正面的方向
                    Vector3 turnPoint = hit.point;
                    Vector3 turnDir = turnPoint - transform.position;
                    turnDir.y = 0;

                    //平滑轉動到滑鼠位置
                    Quaternion targetRotation = Quaternion.LookRotation(turnDir);
                    playerFace.transform.localRotation = Quaternion.Slerp(playerFace.transform.localRotation, targetRotation, Time.deltaTime * 5);
                    Debug.DrawRay(playerFace.transform.position, turnDir, Color.red);
                }

                aimRig.weight += Time.deltaTime / aimTime;
            }
            else
            {
                PlayerTurnFace();
                isAim = false;
                moveSpeed = preSpeed;
                if (aimRig.weight > 0)
                {
                    aimRig.weight -= Time.deltaTime / aimTime;
                }

            }
        }
        else
        {
            isAim = false;
        }

    }
    private void FixedUpdate()
    {
        if (!playerStatus.IsDead)
        {
            PlayerMove();
            LimitSpeed();
        }

    }

    void PlayerMove()
    {
        var cbt = inputAction.PlayerInput.PlayerMove;
        float horizontalInput = cbt.ReadValue<Vector3>().x;
        float verticalInput = cbt.ReadValue<Vector3>().z;

        moveDirection = forwardDir.transform.forward * verticalInput + forwardDir.transform.right * horizontalInput;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        if (moveDirection.magnitude > 0.1f)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        }
    }

    void LimitSpeed()
    {
        Vector3 flatVal = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVal.magnitude > moveSpeed)
        {
            Vector3 limitVal = flatVal.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVal.x, rb.velocity.y, limitVal.z);
        }
    }


    void PlayerTurnFace()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            // 計算角色目標角度
            // float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            // Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            // // 更新playerFace的旋轉
            // playerFace.transform.localRotation = Quaternion.Slerp(playerFace.transform.localRotation, targetRotation, Time.deltaTime * 3.5f);

            Quaternion rotDir = Quaternion.LookRotation(moveDirection);
            playerFace.transform.localRotation = Quaternion.Slerp(playerFace.transform.localRotation, rotDir, Time.deltaTime * 3.5f);

            Debug.DrawRay(transform.position, moveDirection * 2, Color.red);

            // Debug.Log($"移動方向: {moveDirection}, 角度: {targetAngle}");
        }
    }

    //可以用在其他地方參考
    public bool IsAim
    {
        get { return isAim; }
    }
    public Vector3 MoveDir
    {
        get { return moveDirection; }
    }




}
