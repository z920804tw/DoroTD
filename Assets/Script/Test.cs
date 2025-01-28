using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    
    [SerializeField] int score;

    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {


    }


    void test1()
    {
        Vector3 rndDir1 = transform.forward - Random.insideUnitSphere * 1;
        Vector3 rndDir2 = transform.forward + Random.insideUnitSphere * 1;

        Debug.DrawRay(transform.position, rndDir1 * 2, Color.red, 10);
        Debug.DrawRay(transform.position, rndDir2 * 2, Color.green, 10);
    }
    void test2()
    {
        Vector3 lookRot = Camera.main.transform.position - transform.position;

        lookRot.y = 0;
        Debug.DrawRay(transform.position, lookRot, Color.red, 5);
        Quaternion targetRotation = Quaternion.LookRotation(lookRot);
        transform.rotation = targetRotation;
    }
    void test3()
    {
        Vector3 turnPos = target.transform.position - transform.position;

        Debug.DrawRay(transform.position, turnPos, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        float angle = Vector3.SignedAngle(transform.forward, turnPos, Vector3.up);
        Debug.Log(angle);
    }
    void test4()
    {
        Vector3 rndSpread = Random.insideUnitSphere * 0.5f + target.transform.position;
        Vector3 rndDir = rndSpread - target.transform.position;
        Debug.DrawRay(target.transform.position, rndDir * 2f, Color.red, 5f);

    }

    public void test5()
    {
        score += 10;
        Debug.Log(score);
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("456");
    }
}
