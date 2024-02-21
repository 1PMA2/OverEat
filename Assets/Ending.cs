using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField]
    Fade Fadein;

    private void Awake()
    {
        ObjectManager.Instance.AddObject(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Fadein.FadeIn(1);
        }
    }

    private void OnDisable()
    {

    }
}
