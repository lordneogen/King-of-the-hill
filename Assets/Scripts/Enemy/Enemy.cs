using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : Drop
{
    // Start is called before the first frame update

    [FormerlySerializedAs("Radius")] [FormerlySerializedAs("_radius")] public float RadiusAttack;
    [FormerlySerializedAs("target")] public Transform TargetObject;
    private NavMeshAgent NavAgent;
    [FormerlySerializedAs("_speed")] public float Speed;
    [FormerlySerializedAs("_stop_radius")] public float StopRadius;
    [FormerlySerializedAs("_health")] public int HealthPoints=100;
    
    
    
    private void Start()
    {
        NavAgent=GetComponent<NavMeshAgent>();
        NavAgent.updateRotation = false;
        NavAgent.updateUpAxis = false;
        NavAgent.speed = Speed;
        NavAgent.stoppingDistance = StopRadius;

    }
    void Update()
    {
        float Distance = Vector2.Distance(TargetObject.position, transform.position);
        // Debug.Log(distance);
        if (Distance <= RadiusAttack)
        {
            NavAgent.SetDestination(new Vector3( TargetObject.position.x,TargetObject.position.y,transform.position.z));
        }

        if (HealthPoints < 0)
        {
            Drop_item();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            try
            {
                Bullet bullet = other.gameObject.GetComponent<Bullet>();
                HealthPoints -= bullet.Damage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Player pl = other.GetComponent<Player>();
            pl.GetHit();
            NavAgent.speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        NavAgent.speed = Speed;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,RadiusAttack);
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(transform.position,StopRadius);
    }
}
