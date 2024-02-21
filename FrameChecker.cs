using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FrameChecker : MonoBehaviour
{
    [SerializeField]
    Font Anton;
    GameObject ending;

    float deltaTime = 0.0f;

    float fTime = 0.0f;

    GUIStyle style;
    Rect rect;
    //float msec;
    //float fps;
    //float worstFps = 100f;
    string text;

    void Awake()
    {
        int w = Screen.width, h = Screen.height;

        rect = new Rect(0, 0, w, h * 4 / 100);

        style = new GUIStyle();
        style.font = Anton;
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = Color.white;

        //StartCoroutine("worstReset");
    }

    void Start()
    {
        ending = ObjectManager.Instance.GetObject("End");
    }


    //IEnumerator worstReset() //코루틴으로 15초 간격으로 최저 프레임 리셋해줌.
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(5f);
    //        worstFps = 100f;
    //    }
    //}

    bool once = true;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        if (ending.activeSelf)
            fTime += Time.deltaTime;
        else if(once)
        {
            GameManager.Instance.Record(fTime);
            once = false;
        }


        int hours = Mathf.FloorToInt(fTime / 3600);
        int minutes = Mathf.FloorToInt((fTime / 60) % 60);
        int seconds = Mathf.FloorToInt(fTime % 60);

        text = string.Format(" {0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    void OnGUI()//소스로 GUI 표시.
    {
        GUI.Label(rect, text, style);

    }
}
