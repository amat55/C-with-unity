using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnaMenuKontrol : MonoBehaviour {
    #region
    //1.yontemin global isimleri
    //GameObject level1, level2, level3;
    //GameObject kilit1, kilit2, kilit3;
    #endregion
    GameObject leveller, Kilitler;

	void Start ()
    {
        #region yontem!
        //yesil olan 1.yontem
        //level1 = GameObject.Find("lvl1");
        //level2 = GameObject.Find("lvl2");
        //level3 = GameObject.Find("lvl3");

        //kilit1 = GameObject.Find("kilit1");
        //kilit2 = GameObject.Find("kilit2");
        //kilit3 = GameObject.Find("kilit3");

        //level1.SetActive(false);
        //level2.SetActive(false);
        //level3.SetActive(false);

        //kilit1.SetActive(false);
        //kilit2.SetActive(false);
        //kilit3.SetActive(false);
        #endregion

        //alttaki 2.yontem bu yontemler kilitler ve levverller klasorune erisim sagliyor
        leveller = GameObject.Find("Leveller");
        Kilitler = GameObject.Find("Kilitler");

        for (int i = 0; i < leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < Kilitler.transform.childCount; i++)
        {
            Kilitler.transform.GetChild(i).gameObject.SetActive(false);
        }
        //PlayerPrefs.DeleteAll(); (tum kayitli olan levelleri siler)
        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
        
    }
    public void buttonSec(int gelenButton)
    {
        if (gelenButton == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (gelenButton == 2)
        {
            #region 1.yontem
            //level1.SetActive(true);
            //level2.SetActive(true);
            //level3.SetActive(true);
            #endregion

            for (int i = 0; i < Kilitler.transform.childCount; i++)
            {
                Kilitler.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < leveller.transform.childCount; i++)
            {
                leveller.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
            {
                Kilitler.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (gelenButton == 3)
        {
            Application.Quit();
        }
    }
    public void levellerButton(int gelenLevel)
    {
        SceneManager.LoadScene(gelenLevel);
    }              
	
	
	
}
