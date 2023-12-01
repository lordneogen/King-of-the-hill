using System;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public float lootSpeed=1f;
    public ItemCount loot;

    private void Start()
    {
        CreateFun();
    }

    public void CreateFun()
    {
        SpriteRenderer rd = gameObject.GetComponent<SpriteRenderer>();
        if (rd!=null)
        {
            rd.sprite = loot.Item.Icon;
        }
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("12");
        
        if (other.GetComponent<Player>())
        {
            Player component = other.GetComponent<Player>();
            if (component != null)
            {
                Vector3 direction = component.transform.position - transform.position;
                direction.Normalize();
                if (Vector3.Distance(component.transform.position,transform.position)<0.1)
                {
                    if (!component.Inventory.LootPush(loot))
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    if(component.Magnit)
                    transform.position += lootSpeed * Time.deltaTime * direction;
                }
            }
        }
    }
}
