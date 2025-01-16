using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSetting : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletDmg;


    void Start()
    {
        Destroy(gameObject, 3f);


    }

    // Update is called once per frame
    void Update()
    {
        // Ray ray = new Ray(transform.position, transform.forward);
        // RaycastHit hit;
        // if (Physics.Raycast(ray, out hit, 0.5f))
        // {
        //     if (hit.collider.gameObject.CompareTag("Enemy"))
        //     {
        //         hit.collider.gameObject.GetComponent<EnemySetting>().TakeDmg(bulletDmg);
        //     }
        //     Debug.Log("Hit");
        //     Destroy(gameObject);
        // }
        // Debug.DrawRay(transform.position,transform.forward*0.5f,Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemySetting>().TakeDmg(bulletDmg);
        }
        Debug.Log("Hit");
        Destroy(gameObject);
    }
}
