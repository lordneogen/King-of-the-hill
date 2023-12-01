using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Drop : MonoBehaviour
{
    public List<ItemCount> LootsObjects;
    public void Drop_item()
    {
        for (int i = 0; i < LootsObjects.Count; i++)
        {
            GameObject sphere = new GameObject("Circle");
            sphere.transform.position = transform.position;
            SpriteRenderer rb = sphere.AddComponent<SpriteRenderer>();
            CircleCollider2D cr = sphere.AddComponent<CircleCollider2D>();
            cr.isTrigger = true;
            ItemDrop aComponent = sphere.AddComponent<ItemDrop>();
            aComponent.loot = LootsObjects[i];
            aComponent.CreateFun();
            
        }
    }
}