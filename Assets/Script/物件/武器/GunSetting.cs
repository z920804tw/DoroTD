using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public bool isAuto; //單發或連發

    [Header("子彈設定")]
    public int maxBullet;
    int currentBullet;
    public float bulletSpreadRange;
    public float bulletSpeed;

    PlayerController playerController;

    float fireTime;


    [Header("武器事件設定")]
    public UnityEvent fireEvent;


    void Start()
    {
        currentBullet = maxBullet;
        playerController = GameObject.Find("PlayerComponets").GetComponent<PlayerComponet>().playerController;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerController.isAim)
        {
            if (isAuto)
            {
                //連發
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    AutoFireSetting();
                }


            }
            else
            {
                //單發
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    NoAutoFireSetting();
                }
            }
        }
    }


    public void GunFire()
    {
        Vector3 randomSpread = Random.insideUnitCircle * bulletSpreadRange;
        Vector3 fireDir = firePos.forward + randomSpread;


        GameObject bb = Instantiate(bullet, firePos.position, Quaternion.identity);

        bb.GetComponent<Rigidbody>().AddForce(fireDir * bulletSpeed, ForceMode.Impulse);

        currentBullet--;

    }
    void AutoFireSetting()
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

    void NoAutoFireSetting()
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

}
