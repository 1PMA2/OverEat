using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EdgeTrigger : MonoBehaviour
{
    Sausage sausage;
    void Start()
    {
        ObjectManager.Instance.GetSausage().TryGetComponent(out sausage);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        sausage.TriggerEnter(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        sausage.TriggerStay(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sausage.TriggerExit(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sausage.CollisionEnter(collision);
    }
}
