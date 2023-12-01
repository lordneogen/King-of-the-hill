using System;
using UnityEngine;

public class DestroyBuild:MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.CompareTag("Build"))
        {
            Debug.Log(1);
            Build build = other.GetComponent<Build>();
            build.Drop_item();
            Destroy(other.gameObject);
        }
    }
}