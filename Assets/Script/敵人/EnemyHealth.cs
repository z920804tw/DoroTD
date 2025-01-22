using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer bodyImg;
    Material bodyMat;
    public float transformTime;
    bool isChange;
    void Start()
    {
        bodyMat = bodyImg.material;
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
        isChange = true;
        while (timer < transformTime)
        {
            bodyMat.color = Color.Lerp(star, end, timer / transformTime);
            timer += Time.deltaTime;
            yield return null;
        }
        bodyMat.color = Color.white;
        isChange = false;
    }

}
