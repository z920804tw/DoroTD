using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum WeaponType
{
    None,
    Pistol,
    SMG,
    Rifle,
    Shotgun,
    Melee,
}
public class GunSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("武器設定")]
    public WeaponType weaponType;
    public Transform firePos;
    public GameObject bullet;
    public float fireDelay;
    public float weaponDmg;
    public bool isAuto; //單發或連發
    [SerializeField] bool canFire;
    bool autoFire;


    [Header("子彈設定")]
    public int maxBullet;
    int currentBullet;
    public float bulletSpreadRange;
    public float bulletSpeed;

    PlayerController playerController;

    float fireTime;


    [Header("武器事件設定")]
    public UnityEvent fireEvent;
    InputMap inputActions;
    InputAction fireAction;
    private void Awake()
    {
        inputActions = new InputMap();
    }
    private void OnEnable()
    {
        inputActions.PlayerInput.Fire.Enable();
    }
    private void OnDisable()
    {
        inputActions.PlayerInput.Fire.Disable();
    }
    void Start()
    {

        fireAction = inputActions.PlayerInput.Fire; //訂閱Fire按鍵事件
        fireAction.performed += FirePress;
        fireAction.canceled += FireCancel;

        currentBullet = maxBullet;
        playerController = GameObject.Find("Player").GetComponent<PlayerComponet>().playerController;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAim) //正在瞄準時，就代表可以開火
        {
            canFire = true;

            if (autoFire)   //如果autoFire=true，就代表目前這個武器有連發功能
            {
                AutoFireSetting();
            }
        }
        else            //沒瞄準時就無法開火
        {
            canFire = false;
        }
    }


    public void GunFire() //生子彈等等的功能
    {
        Vector3 randomSpread = Random.insideUnitCircle * bulletSpreadRange;
        Vector3 fireDir = firePos.forward + randomSpread;


        GameObject bb = Instantiate(bullet, firePos.position, Quaternion.identity);
        bb.GetComponent<BulletSetting>().bulletDmg = weaponDmg;
        bb.GetComponent<Rigidbody>().AddForce(fireDir * bulletSpeed, ForceMode.Impulse);

        currentBullet--;

    }
    void AutoFireSetting() //連發武器用
    {
        if (currentBullet > 0)
        {
            fireTime += Time.deltaTime;
            if (fireTime >= fireDelay)
            {
                fireTime = 0;
                fireEvent.Invoke();
                Debug.Log("開槍");
            }
        }
        else
        {
            Debug.Log("沒有彈藥");
        }
    }

    void NoAutoFireSetting()//單發武器用
    {
        if (currentBullet > 0)
        {
            fireEvent.Invoke();
            Debug.Log("開槍");
        }
        else
        {
            Debug.Log("沒有彈藥");
        }
    }

    void FirePress(InputAction.CallbackContext context) //開火按鍵被按下時(只會有一次偵測)
    {
        if (canFire)
        {
            if (!isAuto) //判斷是否是自動武器，如果不是就單發，如果是就將autoFire設定true;
            {
                NoAutoFireSetting();
            }
            else
            {
                autoFire = true;
            }
        }
    }
    void FireCancel(InputAction.CallbackContext context) //開火按件放開時觸發(只會偵測一次)
    {
        autoFire = false;
    }

}
