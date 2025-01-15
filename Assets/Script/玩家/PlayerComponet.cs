using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponet : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController playerController;
    public PlayerStatus playerStatus;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("1334");
        }
    }
}
