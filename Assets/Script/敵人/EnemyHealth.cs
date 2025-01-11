using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Image bodyImg;
    public float transformTime;
    bool isChange;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeDmgColor()
    {
        if (!isChange)
        {
            StartCoroutine(changeColor(Color.white, new Color32(255, 110, 110, 255)));
        }

    }


    IEnumerator changeColor(Color star, Color end)
    {
        float timer = 0;
        isChange=true;
        while (timer < transformTime)
        {
            bodyImg.color = Color.Lerp(star, end, timer / transformTime);
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("完成");
        bodyImg.color = Color.white;
        isChange=false;
    }
}
