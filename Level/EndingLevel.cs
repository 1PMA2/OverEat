using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingLevel : MonoBehaviour
{
    [SerializeField]
    private Fade fadeout;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        fadeout.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
