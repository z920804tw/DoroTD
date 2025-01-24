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
    GunSelectUI gunSelectUI;

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
        gunSelectUI = GameObject.FindWithTag("SceneUI").GetComponent<SceneUIManager>().gunSelectUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //當滑鼠滾輪向上120 向下 -120
    void MouseScroll(InputAction.CallbackContext context)
    {
        mouseScrollY = context.ReadValue<float>();

        //只有大於1把武器以上才能切換
        if (weapons.Count > 1)
        {
            SwitchWeapon();
        }

    }

    //切換武器的功能
    void SwitchWeapon()
    {
        CloseAllWeapon();

        //會根據滑鼠滾輪的滾向來檢查當前是不是在第一把武器或是最後一把
        if (mouseScrollY > 0)
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
        else if (mouseScrollY < 0)
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
        gunSelectUI.SelectWeaponUI(currnetIndex);
    }
    void CloseAllWeapon()
    {
        foreach (GameObject i in weapons)
        {
            i.SetActive(false);
        }
    }

}
