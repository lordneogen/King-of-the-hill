using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
public class ItemContainerContainerSlotCrafterUI : MonoBehaviour
{
    private GameObject ItemCraftCeil;
    private GameObject Crafter;
    private List<Item> Items;
    public int LevelOfBuild;
    public string Type;

    private SceneScript SceneScript;
    // private bool Craft;
    // [Inject]
    
    // [Inject]
    // public ISceneScript SceneScript;
    
    private void Start()
    {
        SceneScript = FindObjectOfType<SceneScript>();
        
        Items = SceneScript.Items;
        Crafter = SceneScript.CraftUIScroll;
        ItemCraftCeil = SceneScript.ItemCeilCraft;
        
        foreach (var i in Items)
        {
            GameObject ItemCraftCeilClone = Instantiate(ItemCraftCeil);
            ItemCraftCeilClone.transform.SetParent(Crafter.transform);
            ItemCraftCeilClone.transform.localScale = new Vector3(1f, 1f, 1f);
            ItemContainerSlotCrafterUI Craft=ItemCraftCeilClone.GetComponent<ItemContainerSlotCrafterUI>();
            Craft.Item = i;
            // Craft.Start();
        }
    }

    private void Update()
    {
        // Debug.Log(SceneScript.GetItemCeil());
    }
}