using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject sausage;
    Rigidbody2D sausageRB;
    Camera cam;

    [SerializeField]
    float fSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        //SetSausage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (sausage == null)
            return;

        float fDT = Time.deltaTime;

        Vector2 FixedPos =
            new Vector2(
                sausage.transform.position.x,
                sausage.transform.position.y);

        transform.position = Vector2.Lerp(transform.position, FixedPos, fDT * sausageRB.velocity.magnitude * fSpeed);
        transform.Translate(0, 0, -10);

       

        if (20 < sausageRB.velocity.magnitude)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Min(20f, sausageRB.velocity.magnitude), fDT);
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10, fDT * 0.5f);
        }

        FixedCamera();

    }

    public void SetSausage(GameObject gameObject)
    {
        sausage = gameObject;

        if(sausage)
            sausageRB = sausage.GetComponent<Rigidbody2D>();
    }

    void FixedCamera()
    {
        if (transform.position.x < -10f)
        {
            Vector3 FixPosX = transform.position;
            FixPosX.x = -10f;
            transform.position = FixPosX;
        }

        if(transform.position.y < -15f)
        {
            Vector3 FixPosY = transform.position;
            FixPosY.y = -15f;
            transform.position = FixPosY;
        }
    }
}
