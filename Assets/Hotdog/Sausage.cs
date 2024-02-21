using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static Sausage;

public class Sausage : MonoBehaviour, IObserver
{
    Bread bread = null;
    Rigidbody2D sausageRigidbody;
    BoxCollider2D sausageBoxCollider;

    Camera cam;

    protected ParticleSystem sause;

    Vector2 clickPos;
    Vector2 ForceDir;

    Vector2 vBreadSize;
    Vector2 vSausageSize;

    [SerializeField]
    float fJumpPower;

    [SerializeField]
    float fAirShootPower;

    [SerializeField]
    float fMaxPower = 200;

    bool isBread = false;

    bool bAirShoot = false;
    bool bmouse = false;

    bool isCilck = false;

    bool isAir = true;

    bool isJump = false;

    public Vector2 Dir() { return ForceDir; }
    public float JumpPower() { return fJumpPower; }
    public bool jump() { return isJump; }
    public bool Click() { return isCilck; }

    void Awake()
    {
        sausageRigidbody = GetComponent<Rigidbody2D>();
        sausageBoxCollider = GetComponent<BoxCollider2D>();
        sause = GetComponent<ParticleSystem>();

        ObjectManager.Instance.AddObject(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        vBreadSize = new Vector2(2.8f, 5f);
        vSausageSize = new Vector2(1.3f, 3f);

        sausageBoxCollider.size = vSausageSize;
        sausageBoxCollider.offset = Vector2.zero;

        bmouse = false;

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            cam.GetComponent<MainCamera>().SetSausage(gameObject);
        }

        
    }

    void OnEnable()
    {
        StartCoroutine(CoFix());
        StartCoroutine(CoUpdate());
    }

    IEnumerator CoFix()
    {
        while (true)
        {


            if (bmouse)
            {
                isJump = true;
                sausageRigidbody.AddForce(ForceDir * fJumpPower, ForceMode2D.Impulse);
                fJumpPower = 0f;
                bmouse = false;
            }

            if (bAirShoot)
            {
                Vector2 vDir = sausageRigidbody.velocity;
                vDir.y = Mathf.Abs(vDir.y);
                vDir.Normalize();
                sausageRigidbody.AddForce(vDir * fAirShootPower, ForceMode2D.Impulse);
                fJumpPower = 0f;
                bAirShoot = false;
            }


            yield return new WaitForFixedUpdate();
        }
    }

    void FixedUpdate()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        TriggerEnter(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        TriggerStay(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExit(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter(collision);
    }
    IEnumerator CoUpdate()
    {
        while (true)
        {

            if(!Option.Instance.gameObject.activeSelf)
            {
                if (isAir)
                    OnAirShoot();
                else
                    OnDrag();
            }
            

            Docking();

            Out();

            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {

    }

    void OnDisable()
    {

    }

    public RectTransform canvasRectTransform;

    void OnDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            isCilck = true;
        }
        else if (isCilck && Input.GetMouseButton(0))
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            ForceDir = clickPos - mousePos;
            ForceDir.Normalize();

            fJumpPower = Vector2.Distance(clickPos, mousePos) * 0.5f;
            fJumpPower = Mathf.Min(fJumpPower, fMaxPower);
        }
        else// if (Input.GetMouseButtonUp(0))
        {
            isCilck = false;
            bmouse = true;
        }
    }

    void OnAirShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isBread)
            {
                bread.AirShoot();
                bread = null;


                isBread = false;
                sausageBoxCollider.size = vSausageSize;
                sausageBoxCollider.offset = Vector2.zero;

                bAirShoot = true;
            }
        }
    }

    void Docking()
    {
        if (isBread)
            return;

        if (bread)
        {
            bread.Docking();
        }
    }

    void Out()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (transform.position.y < -20)
                GameManager.Instance.Retry();
        }
    }

    public void TriggerEnter(Collider2D collision)
    {


        TriggerStay(collision);

        //»§ »óÅÂ°¡ ¾Æ´Ï°í »§ ¾ÆÀÌÅÛ¿¡ ´ê¾ÒÀ»¶§ 
        if (collision.gameObject.layer == 6 && !isBread)
        {
            if (bread)
                return;

            if (collision.gameObject.TryGetComponent<Bread>(out bread))
            {
                bread.Attach(this);
            }

        }
    }



    public void TriggerStay(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" ||
           collision.gameObject.tag == "Bread")
        {
            sause.Stop();
            isAir = false;
            isJump = false;
        }
    }

    public void TriggerExit(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" ||
           collision.gameObject.tag == "Bread")
        {
            sause.Play();
            isAir = true;
        }
    }

    public void CollisionEnter(Collision2D collision)
    {

    }

    public void Docked()
    {
        sausageBoxCollider.size = vBreadSize;
        sausageBoxCollider.offset = Vector2.zero;
        isBread = true;
    }

    public void OnNotify()
    {
       //Debug.Log("1");
    }
}



