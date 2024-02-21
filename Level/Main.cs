using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    //private Fade fade;

    private void Awake()
    {
        
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        //Screen.SetResolution(1920, 1080, false);

        ObjectManager.Instance.CreateHotdog(0, -8);

    }


    //bool once = true;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{

        //    if (0.5f <= Time.timeScale)
        //        if (once)
        //        {
        //            once = false;
        //            fade.FadeIn();
        //        }

        //}
    }

    private void OnDestroy()
    {

    }
}
