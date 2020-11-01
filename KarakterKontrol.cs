using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;  //Bu kutuphane mobil entegrasyonu sagliyor

public class KarakterKontrol : MonoBehaviour {

    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;
    public Text altinText;
    public Text canText;
    public Image siyahArkaPlan;
    int can = 100;
   

    int beklemeAnimSayac=0;
    int yurumeAnimSayac=0;
    int altinSayaci = 0;

    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;
    Vector3 vec;
    Vector3 kameraSonpos;
    Vector3 kameraIlkPos;

    float horizontal = 0;
    float beklemeAnimZaman=0;
    float yurumeAnimZaman = 0;
    float siyahArkaPlanSayaci = 0;
    
    bool birKereZipla = true;
    float anaMenuyeDonZaman = 0;

    GameObject kamera;

	void Start ()
    {
        Time.timeScale = 1;
        siyahArkaPlan.gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        }
        kameraIlkPos = kamera.transform.position - transform.position;
        canText.text = "Can " + can;
        altinText.text = "Ganimet= " + altinSayaci;

    }
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (birKereZipla)
            {
                fizik.AddForce(new Vector2(0, 500));
                birKereZipla = false;
            }
            
        }
      
    }

    void FixedUpdate ()
    {
        karakterHaraket();
        animasyon();
        if (can <= 0) //olunce
        {
            Time.timeScale = 0.4f;
            canText.enabled = false;
            siyahArkaPlanSayaci += 0.03f;
            siyahArkaPlan.gameObject.SetActive(true);
            siyahArkaPlan.color = new Color(0, 0, 0, siyahArkaPlanSayaci);
            anaMenuyeDonZaman += Time.deltaTime;
            if (anaMenuyeDonZaman > 1)
            {
                SceneManager.LoadScene("anamenu");
            }
        }
	}
    void LateUpdate()
    {
        kameraKontrol();
    }
    void karakterHaraket()
    {
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        birKereZipla = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "kursun")
        {
            can-=10;
            canText.text = "Can " + can;
        }
        if (col.gameObject.tag == "dusman")
        {
            can-=10;
            canText.text = "Can " + can;
        }
        if (col.gameObject.tag == "testere")
        {
            can -= 20;
            canText.text = "Can " + can;
        }
        if (col.gameObject.tag == "levelbitsin")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (col.gameObject.tag == "canver")
        {
            can += 10;
            canText.text = "Can " + can;
            col.GetComponent<BoxCollider2D>().enabled = false;
            col.GetComponent<CanVer>().enabled = true;
            Destroy(col.gameObject, 3);
        }
        if (col.gameObject.tag == "altin")
        {
            altinSayaci++;
            altinText.text = "Ganimet= " + altinSayaci;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "su")
        {
            can = 0;
        }
    }

    void kameraKontrol()
    {
        kameraSonpos = kameraIlkPos + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonpos, 0.1f);
    }
    void animasyon()
    {
        if (birKereZipla)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.05f)
                {
                    spriteRenderer.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    beklemeAnimZaman = 0;
                }


            }
            else if (horizontal > 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }
        else
        {
            if (fizik.velocity.y > 0)
            {
                spriteRenderer.sprite = ziplamaAnim[0];
            }
            else
            {
                spriteRenderer.sprite = ziplamaAnim[1];
            }
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            
        }
        
    }
}
