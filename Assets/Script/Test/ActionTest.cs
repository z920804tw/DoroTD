using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTest : MonoBehaviour
{
    // Start is called before the first frame update

    public event Action<string> onEnterEvent;
    public event Action onExitEvent;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        onEnterEvent.Invoke(other.name);
    }
    private void OnTriggerExit(Collider other)
    {
        onExitEvent.Invoke();
    }
}
