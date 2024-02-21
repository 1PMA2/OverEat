using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bread : MonoBehaviour
{
    FixedJoint2D joint;
    Rigidbody2D body;
    CapsuleCollider2D capsule;
    Sausage AttachedSausage;

    bool prevState = false;

    public enum State
    { 
       IDLE,
       Attach,
       Attached
    }


    State eState = State.IDLE;


    private void Awake()
    {
        joint = GetComponent<FixedJoint2D>();
        body = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(CoLate());
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        prevState = body.IsSleeping();

    }

    

    void FixedUpdate()
    {
        if (body.IsSleeping() != prevState)
        {
           IsItem();
           prevState = body.IsSleeping();
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    //y null

    

    private void LateUpdate()
    {
       
    }

    //y eof
    IEnumerator CoLate()
    {
        while (true)
        {

            switch (eState)
            {
                case State.Attach:
                    joint.enabled = true;
                    joint.connectedBody = AttachedSausage.GetComponent<Rigidbody2D>();
                    eState = State.Attached;
                    break;

                case State.Attached:
                    transform.parent = AttachedSausage.transform;
                    
                    break;

                default:
                    break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDisable()
    {

    }

    void Initialize()
    {
        gameObject.layer = 6;

        joint.enabled = false;
        capsule.enabled = true;

        body.velocity = Vector2.zero;
        body.angularVelocity = 0f;


        transform.position = transform.localPosition;
        transform.rotation = Quaternion.Euler(0, 0, 90f);
    }

    void IsItem()
    {
        if (gameObject.layer == 7 && body.IsSleeping())
        {
            gameObject.layer = 6;
            body.Sleep();
        }
    }

    public void Attach(Sausage Sausage)
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;

        gameObject.layer = 3;

        AttachedSausage = Sausage;
    }

    private float lerpTime = 0.3f;

    private float currentTime = 0f;

    public void Docking()
    {
        currentTime += Time.deltaTime;

        float t = Mathf.Clamp01(currentTime / lerpTime);

        //float Distance = Vector2.Distance(transform.position, AttachedSausage.transform.position);

        transform.position = Vector2.Lerp(transform.position, AttachedSausage.transform.position, t);

        transform.rotation = Quaternion.Lerp(transform.rotation, AttachedSausage.transform.rotation, t);

        transform.Translate(0, 0, -0.5f);

        //Quaternion difference = Quaternion.Inverse(AttachedSausage.transform.rotation) * transform.rotation;

        if (currentTime >= lerpTime)
        {
            currentTime = 0f;

            transform.position = AttachedSausage.transform.position;
            transform.rotation = AttachedSausage.transform.rotation;
            transform.Translate(0, 0, -0.5f);

            //breadjoint.connectedBody = sausageRigidbody;
            //breadjoint.enabled = true;

            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;

            AttachedSausage.Docked();

            eState = State.Attach;
        }
    }

    public void AirShoot()
    {
        transform.parent = null;

       joint.enabled = false;
       joint.connectedBody = null;

       gameObject.layer = 7;

        AttachedSausage = null;

        eState = State.IDLE;
    }
}
