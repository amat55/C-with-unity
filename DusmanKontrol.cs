using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR //burada #if ve #endif yazmamizin sebebi oyun icinde derlenmemesini saglamak 
using UnityEditor;//bu kisimda unity editoru kutuphanesini cagirdik nedir bu unity editor (programcinin isine yaricak kodlardir )
#endif

public class DusmanKontrol : MonoBehaviour
{

    public int resim;
    int aradakiMesafeSayaci = 0;
    int hiz = 5;
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirKereAl = true;
    bool ilerimiGerimi = true;
    Vector3 aradakiMesafe;                //editor kodlamada public girilen degerlr oyun icerisinde gorunmez o yuzden editor kisminida tanim yapilmalidir.
    GameObject karakter;
    RaycastHit2D ray;
    public LayerMask layermusk;  //bu kod oyun icerisindew edtor kisiminda ise yariyor gerekli bilgileri unity.docs dan bak!
    public Sprite onTaraf;
    public Sprite arkaTaraf;
    SpriteRenderer spriterenderer;
    public GameObject kursun;
    float atesZamani = 0;

    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        karakter = GameObject.FindGameObjectWithTag("Player");
        spriterenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }

    }


    void FixedUpdate()
    {
        beniGordumu();
        if (ray.collider.tag==("Player"))
        {
            hiz = 8;
            spriterenderer.sprite = onTaraf;
            atesEt();
            //Debug.Log("Gordu");
        }
        else
        {
            hiz = 4;
            spriterenderer.sprite = arkaTaraf;
            //Debug.Log("beni gormedi");
        }
        noktalaraGit();
    }
    void atesEt()
    {
        atesZamani += Time.deltaTime;
        if (atesZamani > Random.Range(0.2f, 1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamani = 0;
        }
    }


    void beniGordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000,layermusk);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }

    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiBirKereAl = true;
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ilerimiGerimi = false;
            }
            else if (aradakiMesafeSayaci == 0)
            {
                ilerimiGerimi = true;
            }
            if (ilerimiGerimi)
            {
                aradakiMesafeSayaci++;
            }
            else
            {
                aradakiMesafeSayaci--;
            }
        }
    }
    public  Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}


//ALT KISIM EDITOR KISMI!

#if UNITY_EDITOR
[CustomEditor(typeof(DusmanKontrol))]
[System.Serializable]

class DusmanKontrolEdidor : Editor
{
    public override void OnInspectorGUI()
    {
        DusmanKontrol script = (DusmanKontrol)target;
        if (GUILayout.Button("URET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();//editor kisimindaki butonlarin arasina 1 bosluk koyan kod.
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermusk")); //editor kisminda public girilen degerleri diger kodlara tanimlamak icin bu kod yazilir.
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif