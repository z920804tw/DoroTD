using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;


public class GunSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("武器設定")]
    public GunInfoSO gunInfoSO;
    public WeaponType weaponType;
    public Transform firePos;
    public GameObject fireEffect;
    public float fireDelay;
    public float reloadTime;
    public float weaponDmg;
    public float spreadRange;

    public bool isAuto; //單發或連發

    [Header("音效設定")]
    public AudioSource audioSource;
    [SerializeField] AudioClip fireClip;
    bool isReload;

    bool fireCold;
    bool autoFire;
    bool canFire;



    [Header("子彈設定")]
    public GameObject bullet;
    public int maxAmmo;
    public int currentAmmo;
    public float bulletSpeed;
    public bool canPenetrate; //子彈能不能穿透
    public float penCount;
    float fireTime;

    PlayerController playerController;
    GunSelectUI gunSelectUI;



    [Header("武器事件設定")]
    public UnityEvent fireEvent;
    InputMap inputActions;
    InputAction fireAction;
    InputAction reloadAction;
    private void Awake()
    {
        inputActions = new InputMap();
    }
    private void OnEnable()
    {
        inputActions.PlayerInput.Fire.Enable();
        inputActions.PlayerInput.Reload.Enable();
    }
    private void OnDisable()
    {
        inputActions.PlayerInput.Fire.Disable();
        inputActions.PlayerInput.Reload.Disable();

        StopAllCoroutines(); //可以避免裝填彈藥到一半切武器時再切回去會卡住
        isReload = false;
        if (gunSelectUI != null)
        {
            gunSelectUI.currentgObj.reloadImg.fillAmount = 0;
        }

    }
    void Start()
    {
        //訂閱事件
        reloadAction = inputActions.PlayerInput.Reload;
        reloadAction.performed += ReloadPress;

        fireAction = inputActions.PlayerInput.Fire; //訂閱Fire按鍵事件
        fireAction.performed += FirePress;
        fireAction.canceled += FireCancel;


        //物件設定
        InitializationGunInfo(); //初始化槍枝設定
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();


        if (GameObject.FindWithTag("SceneUI") != null) gunSelectUI = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().gunSelectUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.IsAim) //正在瞄準時，就代表可以開火
        {
            canFire = true;
            if (autoFire)   //如果autoFire=true，就代表目前這個武器有連發功能
            {
                AutoFire();
            }
        }
        else            //沒瞄準時就無法開火
        {
            canFire = false;
        }
    }

    //生子彈等等的功能
    public void GunFire()
    {
        //先生成子彈隨機位置(模擬擴散)
        Vector3 randomSpread = Random.insideUnitCircle * spreadRange;
        Vector3 fireDir = firePos.forward + randomSpread;

        GameObject bb = Instantiate(bullet, firePos.position, Quaternion.identity);
        bb.transform.eulerAngles = firePos.eulerAngles;
        bb.GetComponent<BulletSetting>().bulletDmg = weaponDmg;
        bb.GetComponent<BulletSetting>().canPenetrate = canPenetrate;
        bb.GetComponent<BulletSetting>().count = penCount;
        bb.GetComponent<Rigidbody>().AddForce(fireDir * bulletSpeed, ForceMode.Impulse);


        //開啟開火特效
        if (weaponType != WeaponType.Shotgun)
        {
            GameObject effect = Instantiate(fireEffect, firePos.position, Quaternion.identity);
            effect.transform.SetParent(firePos);
            effect.transform.forward = firePos.forward;
        }

        //開火音效
        audioSource.PlayOneShot(fireClip);
    }

    void InitializationGunInfo()
    {
        if (gunInfoSO != null)
        {
            //槍枝設定
            weaponType=gunInfoSO.weaponType;
            fireDelay=gunInfoSO.fireDelay;
            reloadTime=gunInfoSO.reloadTime;
            weaponDmg=gunInfoSO.weaponDmg;
            spreadRange=gunInfoSO.spreadRange;
            isAuto=gunInfoSO.isAuto;

            //槍枝子彈設定
            bullet=gunInfoSO.bullet;
            maxAmmo=gunInfoSO.maxAmmo;
            currentAmmo=maxAmmo;
            bulletSpeed=gunInfoSO.bulletSpeed;
            canPenetrate=gunInfoSO.canPenetrate;
            penCount=gunInfoSO.penCount;

            //音效設定
            fireClip=gunInfoSO.fireClip;
        }
    }
    //武器冷卻
    void ResetFireCold()
    {
        fireCold = false;
    }

    //連發武器用
    void AutoFire()
    {
        if (currentAmmo > 0)
        {
            fireTime += Time.deltaTime;
            if (fireTime >= fireDelay)
            {
                fireTime = 0;
                fireEvent.Invoke();
                currentAmmo--;
                Debug.Log("開槍");
            }
        }
        else
        {
            Debug.Log("沒有彈藥");
        }
    }
    //一般武器
    void NormalWeapon()
    {
        if (!isAuto) //判斷是否是自動武器，如果不是就單發，如果是就將autoFire設定true;
        {
            if (currentAmmo > 0)
            {
                fireEvent.Invoke();
                currentAmmo--;
                Debug.Log("開槍");
            }
            else
            {
                Debug.Log("沒有彈藥");
            }
        }
        else
        {
            autoFire = true;
        }
    }

    //霰彈用
    public void ShotGun()
    {
        if (currentAmmo > 0)
        {
            for (int i = 0; i < 10; i++)
            {
                fireEvent.Invoke();
            }
            currentAmmo--;
            GameObject effect = Instantiate(fireEffect, firePos.position, Quaternion.identity);
            effect.transform.SetParent(firePos);
            effect.transform.forward = firePos.forward;
        }
        else
        {
            Debug.Log("沒有子彈");
        }
    }

    void FirePress(InputAction.CallbackContext context) //開火按鍵被按下時(只會有一次偵測)
    {
        if (canFire && !fireCold && !isReload)
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    NormalWeapon();
                    break;
                case WeaponType.Rifle:
                    NormalWeapon();
                    break;
                case WeaponType.Shotgun:
                    ShotGun();
                    break;
            }
            fireCold = true; //槍枝冷卻功能，連發武器不會用到，只有單發武器會有用
            Invoke("ResetFireCold", fireDelay);
        }
    }
    void FireCancel(InputAction.CallbackContext context) //開火按件放開時觸發(只會偵測一次)
    {
        autoFire = false;
        fireTime = 0;
    }

    void ReloadPress(InputAction.CallbackContext context)
    {
        if (!isReload && currentAmmo != maxAmmo)
        {
            isReload = true;
            autoFire = false;
            StartCoroutine(ReloadAmmo());
        }
    }
    IEnumerator ReloadAmmo()
    {
        float rTimer = 0;
        while (rTimer <= reloadTime)
        {
            rTimer += Time.deltaTime;
            if (gunSelectUI != null)
            {
                gunSelectUI.currentgObj.reloadImg.fillAmount = rTimer / reloadTime;
            }
            yield return null;
        }
        currentAmmo = maxAmmo;
        if (gunSelectUI != null) gunSelectUI.currentgObj.reloadImg.fillAmount = 0;
        isReload = false;
        Debug.Log("裝彈完成");
    }


}
