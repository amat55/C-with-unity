using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltinKontrol : MonoBehaviour {

    public Sprite[] animasyonKareleri;
    SpriteRenderer spriterenderer;
    float zaman = 0;
    int animasyonKareleriSayaci = 0;
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.03f)
        {
            spriterenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];
            if (animasyonKareleri.Length == animasyonKareleriSayaci)
            {
                animasyonKareleriSayaci = 0;
            }
            zaman = 0;
        }
    }
}
