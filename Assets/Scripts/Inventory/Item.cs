using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item", menuName = "Custom/Item")]
public class Item : ScriptableObject
{
    public string Name = "Item";
    public Sprite Icon;
    [FormerlySerializedAs("max_count")] public int MaxCount=64;
    [FormerlySerializedAs("craft")] public List<ItemCount> Сraft;
    [FormerlySerializedAs("build")] public GameObject IsBuild;
    public string Type="";
    public int Level=0;
    public bool IsGun;
}

