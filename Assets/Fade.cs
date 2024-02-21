using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Fade : MonoBehaviour
{
    bool m_bFade = true;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator CoFadeIn(int i)
    {
        while(true)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), Time.deltaTime * 2f);

            if(transform.localScale.x >= 2.9f)
            {
                transform.localScale = new Vector2(3, 3);
                m_bFade = true;

                switch(i)
                {
                    case 0:
                        SceneManager.LoadScene("Stage");
                        break;
                    case 1:
                        SceneManager.LoadScene("End");
                        break;

                }
            }

            yield return null;
        }

    }

    private float lerpTime = 1f;

    private float currentTime = 0f;


    IEnumerator CoFadeOut()
    {
        while (true)
        {
            currentTime += Time.deltaTime;

            float t = Mathf.Clamp01(currentTime / lerpTime);

            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0 ,1), t);

            if (transform.localScale.x <= 0.01f)
            {
                currentTime = 0f;
                transform.localScale = new Vector2(0, 0);
                m_bFade = true;
                gameObject.SetActive(false);
            }

            yield return null;
        }

    }

    public void FadeIn(int i = 0)
    {
        if(m_bFade)
        {
            transform.localScale = new Vector3(0, 0, 0);
            StartCoroutine(CoFadeIn(i));
            m_bFade = false;
        }
    }

    public void FadeOut()
    {
        if (m_bFade)
        {
            transform.localScale = new Vector3(3, 3, 1);
            StartCoroutine(CoFadeOut());
            m_bFade = false;
        }
    }
}
