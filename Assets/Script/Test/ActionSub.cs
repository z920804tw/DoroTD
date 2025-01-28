using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSub : MonoBehaviour
{
    // Start is called before the first frame update
    public ActionTest actionTest;
    void Start()
    {
        actionTest.onEnterEvent += Enter;
        actionTest.onExitEvent += Exit;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Enter(string name)
    {
        if(name==this.name)
        Debug.Log("Enter");
    }
    void Exit()
    {
        Debug.Log("Exit");
    }
}
