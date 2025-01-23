using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerWeapons : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weapons;
    public int currnetIndex;
    float mouseScrollY;
    InputMap inputActions;
    GunSwitch gunSwitchUI;

    private void Awake()
    {
        inputActions = new InputMap();
    }
    private void OnEnable()
    {
        inputActions.PlayerInput.MouseScroll.Enable();
    }
    private void OnDisable()
    {
        inputActions.PlayerInput.MouseScroll.Disable();
    }
    void Start()
    {
        currnetIndex = 0;
        weapons[currnetIndex].SetActive(true);
        inputActions.PlayerInput.MouseScroll.performed += MouseScroll;
        gunSwitchUI = GameObject.Find("GunList").GetComponent<GunSwitch>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CloseAllWeapon()
    {
        foreach (GameObject i in weapons)
        {
            i.SetActive(false);
        }
    }
    void MouseScroll(InputAction.CallbackContext context) //當滑鼠滾輪向上120 向下 -120
    {
        mouseScrollY = context.ReadValue<float>();
        SwitchWeapon();
    }

    void SwitchWeapon()
    {
        CloseAllWeapon();
        if (mouseScrollY > 0) //上一把
        {
            if (currnetIndex == 0)
            {
                currnetIndex = weapons.Count - 1;
            }
            else
            {
                currnetIndex--;
            }
        }
        else if (mouseScrollY < 0) //下一把
        {

            if (currnetIndex == weapons.Count - 1)
            {
                currnetIndex = 0;
            }
            else
            {
                currnetIndex++;
            }
        }
        weapons[currnetIndex].SetActive(true);

        if (gunSwitchUI != null)
        {
            gunSwitchUI.SelectWeapon(currnetIndex);
        }
    }


}
