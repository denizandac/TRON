using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorcycleCollider : MonoBehaviour
{
    public bool isHit;
    private void Start()
    {
        isHit = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        isHit = true;
    }
    void OnCollisionStay(Collision collision)
    {
        isHit = true;
    }
    void OnTriggerEnter(Collider other)
    {
        isHit = true;
    }
}
