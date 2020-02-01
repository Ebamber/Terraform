using UnityEngine;
using System.Collections;
using System;

public class HexGrid : MonoBehaviour
{
    public int maxHeight;
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
    public GameObject emptyGO;

    void Awake()
    {
        //instantiate the grid to be used in the turn manager
        grid = new Tile[maxHeight, maxHeight];
        currentHeight = minHeight - 1;
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
            //Debug.Log(xOffset * counter);
            for (int x = 0; x < currentHeight; x++)
            {
                //grid[z, x] = new Tile(defaultTilePrefab);
                grid[z, x] = Instantiate(defaultTilePrefab).AddComponent<Tile>().SetTile(defaultTilePrefab);
                grid[z, x].tileState = TileState.UNCLAIMED;
                if (z % 2 == 0)
                {
                    grid[z, x].transform.position = new Vector3((x * xOffset) -xOffset * counter, 0, z * zOffset);
                }
                else
                {
                    grid[z, x].transform.position = new Vector3((x * xOffset + (xOffset / 2)) - xOffset * counter, 0, z * zOffset);
                }
            }
            for (int x = currentHeight; x < maxHeight; x++) {
                grid[z, x] = Instantiate(emptyGO).AddComponent<Tile>().SetTile(TileState.UNAVAILABLE);
            }
            if ((z + 1) % 2 == 0 && z >= maxHeight / 2)
            {
                counter--;
            }
        }
    }

}
