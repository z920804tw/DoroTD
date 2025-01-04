using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weapons;
    public int currnetIndex;
    float mouseScrollY;
    InputMap inputActions;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.PlayerInput.MouseScroll.ReadValue<float>() != 0)
        {
            SwitchWeapon();
        }
    }
    void CloseAllWeapon()
    {
        foreach (GameObject i in weapons)
        {
            i.SetActive(false);
        }
    }
    void MouseScroll(InputAction.CallbackContext context)
    {
        mouseScrollY = context.ReadValue<float>();
    }

    void SwitchWeapon()
    {
        CloseAllWeapon();
        if (mouseScrollY > 0)
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
        else if (mouseScrollY < 0)
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
        weapons[currnetIndex].SetActive(true);
    }

    // void MouseScroll(InputAction.CallbackContext context)
    // {
    //     Debug.Log(context.ReadValue<float>());
    //     mouseScrollY = context.ReadValue<float>();
    // }
}
