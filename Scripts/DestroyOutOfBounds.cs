using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField] private float destroyAtY;

    void Update()
    {
       if (transform.position.y <= destroyAtY)
       {
            Destroy(gameObject);
       } 
    }
}
