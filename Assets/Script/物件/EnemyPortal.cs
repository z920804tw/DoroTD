using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPortal : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public float maxHp;
    public float currentHp;
    [SerializeField] GameObject hpBar;
    [SerializeField] Image hpBarImg;
    Vector3 lookVector;
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        lookVector = Camera.main.transform.position - transform.position;
        lookVector.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookVector);
        hpBar.transform.rotation= targetRotation;
    }
    public void TakeDmg(float dmg)
    {
        currentHp -= dmg;
        hpBarImg.fillAmount = currentHp / maxHp;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
