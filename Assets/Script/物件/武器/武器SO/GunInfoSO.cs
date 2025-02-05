using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum WeaponType
{
    None,
    Pistol,
    Rifle,
    Shotgun,
}
[CreateAssetMenu(fileName = "GunSetting", menuName = "Weapon/Gun")]
public class GunInfoSO : ScriptableObject
{
    [Header("槍枝基本內容設定")]
    public Sprite gunImg;
    public string gunName;
    public float price;
    [Header("槍枝參數設定")]
    public WeaponType weaponType;
    public float fireDelay;
    public float reloadTime;
    public float weaponDmg;
    public float spreadRange;
    public bool isAuto; //單發或連發

    [Header("子彈設定")]
    public GameObject bullet;
    public int maxAmmo;
    public float bulletSpeed;
    public bool canPenetrate; //子彈能不能穿透
    public float penCount;

    [Header("音效設定")]
    public AudioClip fireClip;
    public AudioClip empytClip;

}
