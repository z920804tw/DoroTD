using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimtor : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public Rig aimRig;
    [SerializeField] float aimTime;
    PlayerController playerController;
    PlayerStatus playerStatus;
    bool isMove;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        isMove = anim.GetBool("Run");
        if (!isMove && playerController.MoveDir != Vector3.zero) //如果目前isMove=false,但玩家有移動，就設定成true
        {
            anim.SetBool("Run", true);
            Debug.Log("玩家正在跑步");
        }
        else if (isMove && playerController.MoveDir == Vector3.zero)                     //如果目前isMove=true,但玩家沒有移動，就設定成false
        {
            anim.SetBool("Run", false);
            Debug.Log("玩家沒有正在跑步");
        }

        //瞄準
        if (playerController.IsAim)
        {
            if (aimRig.weight < 1)
            {
                aimRig.weight += Time.deltaTime / aimTime;
            }
        }
        else if (!playerController.IsAim)
        {
            if (aimRig.weight > 0)
            {
                aimRig.weight -= Time.deltaTime / aimTime;
            }
        }
    }
}
