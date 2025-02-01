using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] fireEffectImg;
    [SerializeField]SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        int rnd = Random.Range(0, 3);
        spriteRenderer.sprite = fireEffectImg[rnd];
        Destroy(gameObject,0.25f);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
