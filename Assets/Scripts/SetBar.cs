using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SetBar : MonoBehaviour
{
    public Transform Bar;
    private float HP;
    private float MaxHP;
    public float Offset=1;

    private void SetHP(float MaxHP,float HP)
    {
        this.MaxHP = MaxHP;
        this.HP = HP;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        Bar.localScale = new Vector3(Offset * HP / MaxHP, Bar.localScale.y, Bar.localScale.z);
    }
}