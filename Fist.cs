using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fist : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    bool Fisting;
    bool reloading;

    [SerializeField]
    float fistPower = 15;

    [SerializeField]
    float fistSpeed = 10;

    float fspeed = 0;
    float diff = 0;


    Vector3 startPos;

    Rigidbody2D fistRb;
    BoxCollider2D fistTrigger;
    void Start()
    {
        fistRb = GetComponent<Rigidbody2D>();
        fistTrigger = GetComponent<BoxCollider2D>();
        Fisting = false;
        startPos = transform.position;

        reloading = false; //������ �Ǿ����� �������� �ƴ�
    }

    private void FixedUpdate()
    {
        if (Fisting)
        {
            fspeed = Mathf.Lerp(fspeed, fistPower, fistSpeed * Time.fixedDeltaTime); // �����ϴ� ���� �����Ͽ� ���

            if(10f < diff)
            {
                fspeed = 0;
                Fisting = false;
                //reloading = true;
                StartCoroutine("Reloading");
            }

            fistRb.velocity = transform.up * fspeed;
        }

    }

    public static readonly WaitForSeconds m_waitForSecond1s = new WaitForSeconds(1f); // ĳ��
    IEnumerator Reloading() //�ָ����� �������� 3���� ���������·�
    {
        while (true)
        {
            yield return m_waitForSecond1s;
            reloading = true;
            break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (reloading)
            return;

        if (collision.gameObject.layer == 3)
        {
            Fisting = true;
            fistTrigger.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fistTrigger.enabled == false)
        {
            Vector2 vec = startPos - transform.position;
            diff = vec.magnitude;
        }

        ReloadFist();

       
    }

    void ReloadFist()
    {
        float fDT = Time.deltaTime;
        //�ָ� ������
        if (reloading)
        {
            transform.position = Vector2.Lerp(transform.position, startPos, 2.0f * fDT);

            if (0.01f >= diff)
            {
                reloading = false; //������ ������
                fistTrigger.enabled = true;
                transform.position = startPos;
            }
        }
    }

    
}
