using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("玩家設定")]
    public GameObject forwardDir;
    public GameObject playerFace;
    public GameObject AimTarget;
    GameObject mainCam;
    [SerializeField] float moveSpeed;
    [SerializeField] float aimSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float preSpeed;

    [Header("Debug")]
    [SerializeField] bool isAim;
    [SerializeField] Rigidbody rb;
    Vector3 moveDirection;
    PlayerStatus playerStatus;
    Vector3 offset = new Vector3(0, 0.5f, 0);
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

    }
    void Update()
    {
        if (!playerStatus.IsDead)
        {
            forwardDir.transform.localEulerAngles = new Vector3(0, mainCam.transform.eulerAngles.y, 0);
            AimMode();
            if (!isAim)
            {
                PlayerTurnFace();
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
        }
    }
    //玩家移動
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
        CheckGround();
        LimitSpeed();
    }
    //限制移動速度
    void LimitSpeed()
    {
        Vector3 flatVal = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVal.magnitude > moveSpeed)
        {
            Vector3 limitVal = flatVal.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVal.x, rb.velocity.y, limitVal.z);
        }
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position + offset, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.8f, groundLayer))
        {
            rb.drag = 5;
        }
        else
        {
            rb.drag = 0;
        }
        Debug.DrawRay(transform.position + offset, Vector3.down * 0.8f, Color.red);
    }
    //瞄準模式
    void AimMode()
    {
        //瞄準模式
        if (Input.GetKey(KeyCode.Mouse1))
        {
            isAim = true;
            moveSpeed = aimSpeed;
            AimTarget.SetActive(true);
            //當滑鼠右鍵按住時，角色會面向滑鼠只到的位置
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mousePos, out hit, 100, groundLayer))
            {
                //turnPonit=玩家在畫面打到的點  turnDir=打到的點與玩家正面的方向
                Vector3 turnPoint = hit.point;
                Vector3 turnDir = turnPoint - transform.position;
                turnDir.y = 0;

                //平滑轉動到滑鼠位置
                Quaternion targetRotation = Quaternion.LookRotation(turnDir);
                playerFace.transform.localRotation = Quaternion.Slerp(playerFace.transform.localRotation, targetRotation, Time.deltaTime * 5);
                Debug.DrawRay(playerFace.transform.position, turnDir, Color.red);

                AimTarget.transform.position = hit.point;
            }
        }
        else
        {
            isAim = false;
            moveSpeed = preSpeed;
            AimTarget.SetActive(false);
        }
    }
    //玩家移動時的轉向
    void PlayerTurnFace()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion rotDir = Quaternion.LookRotation(moveDirection);
            playerFace.transform.localRotation = Quaternion.Slerp(playerFace.transform.localRotation, rotDir, Time.deltaTime * 3.5f);
            Debug.DrawRay(transform.position, moveDirection * 2, Color.red);
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
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public float PreSpeed
    {
        get { return preSpeed; }
        set { preSpeed = value; }
    }


}
