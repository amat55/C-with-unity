using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR //burada #if ve #endif yazmamizin sebebi oyun icinde derlenmemesini saglamak 
using UnityEditor;//bu kisimda unity editoru kutuphanesini cagirdik nedir bu unity editor (programcinin isine yaricak kodlardir )
#endif

public class Testere : MonoBehaviour {

    public int resim;
    int aradakiMesafeSayaci = 0;
    GameObject []gidilecekNoktalar;
    bool aradakiMesafeyiBirKereAl = true;
    bool ilerimiGerimi = true;
    Vector3 aradakiMesafe;
	void Start ()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];

        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
	  
	}
	
	
	void FixedUpdate ()
    {
        transform.Rotate(0, 0, 5);
        noktalaraGit();
	}

    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime*10;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiBirKereAl = true;
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ilerimiGerimi = false;
            }
            else if (aradakiMesafeSayaci==0)
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

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}



//ALT KISIM EDITOR KISMI!

#if UNITY_EDITOR
[CustomEditor(typeof(Testere))]
[System.Serializable]

class testereEdidor:Editor
{
    public override void OnInspectorGUI()          
    {
        Testere script = (Testere)target;
        if (GUILayout.Button("URET",GUILayout.MinWidth(100),GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
    }
}
#endif