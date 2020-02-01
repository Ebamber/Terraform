using UnityEngine;
using System.Collections;
using System;

public class HexGrid : MonoBehaviour
{
    public int maxWidth,maxHeight;

    [Range(0f,2f)]
    public float xOffset, zOffset;

    [SerializeField]
    public Tile[,] grid;

    public GameObject defaultTilePrefab;
    public GameObject camera;
    private GameObject midpoint;

    void Awake()
    {
        //instantiate the grid to be used in the turn manager
        grid = new Tile[maxHeight, maxWidth];
        CreateHexTileMap();
    }

    public void CreateHexTileMap() {
        for (int z = 0; z < maxHeight; z++)
        {
            for (int x = 0; x < maxWidth; x++)
            {
                grid[z, x] = new Tile(defaultTilePrefab);
                if (grid[z, x].tileState != TileState.UNAVAILABLE)
                {
                    GameObject tile = Instantiate(grid[z, x].tileModel);
                    if (z % 2 == 0)
                    {
                        tile.transform.position = new Vector3(x * xOffset, 0, z * zOffset);
                    }
                    else {
                        tile.transform.position = new Vector3(x * xOffset + xOffset/2, 0, z * zOffset);
                    }
                }
            }
        }
        //dynamically place camera according to grid size to fit all pieces (breaks when over 50*50, but tiles become too small to see at that point anyway)
        camera.transform.position = new Vector3(0, Mathf.Max(maxWidth,maxHeight) + 5, 0);
    }

}
