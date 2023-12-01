using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public TileBase seaTile;
    public TileBase landTile;
    public TileBase sandTile;

    public int width = 50;
    public int height = 50;

    public float islandFrequency = 0.03f;
    public float islandThreshold = 0.4f;

    public float sandFrequency = 0.05f;
    public float sandThreshold = 0.6f;

    void Start()
    {
        GenerateIsland();
    }

    void GenerateIsland()
    {
        terrainTilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Генерация шума Перлина для островов
                float islandValue = Mathf.PerlinNoise((float)x * islandFrequency, (float)y * islandFrequency);

                // Генерация шума Перлина для песчаных участков
                float sandValue = Mathf.PerlinNoise((float)x * sandFrequency, (float)y * sandFrequency);

                // Определение тайла в зависимости от значения шума
                TileBase selectedTile;

                if (islandValue > islandThreshold)
                {
                    // Зона острова
                    if (sandValue > sandThreshold)
                    {
                        // Песчаная часть острова
                        selectedTile = sandTile;
                    }
                    else
                    {
                        // Земля острова
                        selectedTile = landTile;
                    }
                }
                else
                {
                    // Вода
                    selectedTile = seaTile;
                }

                terrainTilemap.SetTile(new Vector3Int(x, y, 0), selectedTile);
            }
        }
    }
}