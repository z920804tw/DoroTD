using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgText : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("傷害UI設定")]
    public float lifeTime;
    public float moveSpeed;
    public TMP_Text dmgText;
    
    Vector3 rndDir;
    float timer;
    void Start()
    {

        rndDir = transform.up - Random.insideUnitSphere * 1f;
        // Debug.DrawRay(transform.position,rndDir,Color.red,10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);

        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += rndDir * moveSpeed * Time.deltaTime;
        }
    }
}
