using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Press : MonoBehaviour
{
    private SpriteRenderer sr;

    Sausage player;

    [SerializeField] Fade fade;

    [SerializeField]
    private Sprite tuto1;

    [SerializeField]
    private Sprite tuto2;

    [SerializeField]
    private Sprite tuto3;

    private bool bw;

    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        bw = true;

        ObjectManager.Instance.GetSausage().TryGetComponent<Sausage>(out player);

        sr.sprite = tuto1;
        transform.localScale = new Vector2(4, 4);
    }

    bool bTuto = false;
    // Update is called once per frame
    void Update()
    {
        if(once)
        {
            if (player.jump() && (-7 < player.transform.position.y))
            {
                sr.sprite = tuto2;
                transform.localScale = new Vector2(3, 3);
                if(!Option.Instance.gameObject.activeSelf)
                    Time.timeScale = 0.1f;
            }

            if (0 < Time.timeScale && 0.2f >= Time.timeScale )
            {
                if (Input.GetMouseButtonDown(0))
                {
                    sr.sprite = tuto3;
                    transform.localScale = new Vector2(3, 3);
                    Time.timeScale = 1;
                    once = false;
                }
            }
        }  

        if(!once)
            Blink();

        if (bTuto)
        {

            if (Input.GetMouseButtonDown(0))
            {

                fade.FadeIn();

            }
        }
    }


    void Blink()
    {
        float fSpeed = 1f;

        float fTime = Time.deltaTime * fSpeed;

        if (bw)
        {

            sr.color += new Color(fTime, fTime, fTime, 0);

            if (sr.color.r >= 0.5f)
            {
                bw = false;
            }
        }
        else
        {
            sr.color -= new Color(fTime, fTime, fTime, 0);

            if (sr.color.r <= 0.1f)
            {
                bw = true;
                bTuto = bw;
            }
        }
    }
}
