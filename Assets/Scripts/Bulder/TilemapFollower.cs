using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TilemapFollower : MonoBehaviour
{
    private Camera MainCamera;
    [FormerlySerializedAs("_surface")] public GameObject Surface;
    private Tilemap TilemapCells;
    private Vector3Int CellPosition;
    public Player Player;
    private SpriteRenderer RendererComponent;
    private Vector3 targetPosition;
    private GameObject LowwerGameObject;
    private bool en=true;
    // public Tile tile;
    private bool InObj;
    // public GameObject gm;
    private void Start()
    {
        MainCamera = Camera.main;
        TilemapCells = Surface.GetComponent<Tilemap>();
        Player = GameObject.FindObjectOfType<Player>();
        RendererComponent = gameObject.GetComponent<SpriteRenderer>();
        RendererComponent.material.color=new Color(1f, 1f, 1f, 0.5f);
    }

    private void Update()
    {
        try
        {
            RendererComponent.enabled = true;
            if (Player.SelectCeil.Item.IsBuild != null && Player.SelectCeil.Item.IsBuild && Time.timeScale>0f )
            {
                // RendererComponent.sprite = Player.SelectCeil.Item.Icon;
                Vector3 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                CellPosition = TilemapCells.WorldToCell(mousePosition);
                targetPosition = TilemapCells.GetCellCenterWorld(CellPosition);


                // Перемещаем объект курсора по центру текущей ячейки Tilemap
                transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
                if (Vector3.Distance(transform.position, Player.transform.position) > 3 || InObj)
                {
                    RendererComponent.color=Color.red;
                }
                else
                {
                    
                    RendererComponent.color=Color.green;
                }
            }
            else
            {
                RendererComponent.enabled=false;
            }
        }
        catch
        {
            RendererComponent.enabled=false;
        }
        
    }

    private void OnMouseUpAsButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (RendererComponent.color == Color.green)
            {
                GameObject newGameObject = Instantiate(Player.SelectCeil.Item.IsBuild);
                newGameObject.transform.position = targetPosition;
                Player.SelectCeil.Count -= 1;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Item"))
        {
            InObj = true;
        }
        else if (other.CompareTag("Build"))
        {
            LowwerGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InObj = false;
    }
}