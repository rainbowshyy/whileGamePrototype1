using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile baseTile;

    [SerializeField] private int width;
    [SerializeField] private int height;

    #region Properties
    public int Width
    {
        get { return width; }
    }

    public int Height
    {
        get { return height; }
    }
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        ClearTilemap();
        DrawBaseTilemap();
    }

    public bool IsTileWalkable(int x, int y)
    {
        if (x < 0 || x >= width ||  y < 0 || y >= height)
            return false;
        else
            return true;
    }

    public void ClearTilemap()
    {
        tilemap.ClearAllTiles();
    }

    public void DrawBaseTilemap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), baseTile);
            }
        }
    }
}
