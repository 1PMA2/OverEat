using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage : MonoBehaviour
{
    [SerializeField]
    private Fade fadeout;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;


        fadeout.FadeOut();

        ObjectManager.Instance.CreateHotdog(1, 0);

        ObjectManager.Instance.CreateFirstBread();

        //GameManager.Instance.CreateBreadStage();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        fadeout = null;
    }
}
