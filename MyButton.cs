using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    

    private void Awake()
    {
        
    }

    public void Start()
    {

        //float fScaleWidth = ((float)Screen.width / (float)Screen.height) / ((float)16 / (float)9);
        //Vector3 vecButtonPos = GetComponent<RectTransform>().localPosition;
        //vecButtonPos.x = vecButtonPos.x * fScaleWidth;
        //GetComponent<RectTransform>().localPosition = new Vector3(vecButtonPos.x, vecButtonPos.y, vecButtonPos.z);
    }

    public void OnClick()
    {

        


       //gameObject.GetComponent<Button>().interactable = false;

        


    }
    public void SetWindowMode()
    {

        Screen.SetResolution(1920, 1080, true);
    }

    public void SetFull()
    {

        Screen.SetResolution(1920, 1080, false);
    }

}
