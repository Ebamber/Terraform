using UnityEngine;
using System.Collections;
using System;

public class HexGrid : MonoBehaviour
{
    public int maxWidth,maxHeight;
    public int currentHeight;
    public int minHeight;
    public int delta;

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
    }

    private void Start()
    {
        currentHeight = minHeight-1;
        CreateHexTileMap();
    }

    public void CreateHexTileMap()
    {
        delta = 1;
        int counter = 0;
        for (int z = 0; z < maxHeight; z++)
        {
            if (currentHeight <= maxHeight && z <= maxHeight / 2)
            {
                currentHeight++;
            }
            else if (currentHeight > minHeight && z >= maxHeight / 2)
            {
                currentHeight--;
            }
            if ((z + 1) % 2 == 0 && z <= maxHeight / 2)
            {
                counter++;
            }
            Debug.Log(xOffset * counter);
            for (int x = 0; x < currentHeight; x++)
            {
                grid[z, x] = new Tile(defaultTilePrefab);
                if (grid[z, x].tileState != TileState.UNAVAILABLE)
                {
                    GameObject tile = Instantiate(grid[z, x].tileModel);
                    if (z % 2 == 0)
                    {
                        tile.transform.position = new Vector3((x * xOffset) -xOffset * counter, 0, z * zOffset);
                    }
                    else
                    {
                        tile.transform.position = new Vector3((x * xOffset + (xOffset / 2)) - xOffset * counter, 0, z * zOffset);
                    }
                }
            }
            if ((z + 1) % 2 == 0 && z >= maxHeight / 2)
            {
                counter--;
            }
        }
    }

}
