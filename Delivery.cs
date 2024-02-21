using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delivery : MonoBehaviour
{
    [SerializeField] Text recordText;
    // Start is called before the first frame update
    void Start()
    {
        recordText.text = GameManager.Instance.RecTime();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
