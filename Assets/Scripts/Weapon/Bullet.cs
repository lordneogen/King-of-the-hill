using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] public float Speed;
    [FormerlySerializedAs("lifetime")] public float LifeTime = 3.0f;
    [FormerlySerializedAs("damage")] public int Damage = 10;
    
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }
    void FixedUpdate()
    {
        
        transform.position += Speed * Time.deltaTime * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
