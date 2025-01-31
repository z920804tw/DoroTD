using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] fireEffectImg;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {

        int rnd = Random.Range(0, 3);
        spriteRenderer.sprite = fireEffectImg[rnd];
    }


    // Update is called once per frame
    void Update()
    {

    }
}
