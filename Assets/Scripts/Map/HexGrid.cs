using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HexGrid : MonoBehaviour
{
    public int maxHeight;
    public int currentHeight;
    public int minHeight;
    public int delta;
    public GameManager manager;

    public int numberOfWaters;
    public int numberOfFertiles;

    [Range(0f,2f)]
    public float xOffset, zOffset;

    [SerializeField]
    public Tile[,] grid;

    public GameObject defaultTilePrefab;
    public GameObject camera;
    private GameObject midpoint;
    public GameObject emptyGO;

    private bool instantiatedAdjacency = false;

    void Awake()
    {
        //instantiate the grid to be used in the turn manager
        grid = new Tile[maxHeight, maxHeight];
        currentHeight = minHeight - 1;
        CreateHexTileMap();
    }
    
    //Fix this if we can at some stage
    //Set a random tile near player base as a crater
    private void Update()
    {
        if (!instantiatedAdjacency) {
            bool instantiateIt = true;
            foreach (Tile tile in grid) {
                if (tile.tileState != TileState.UNAVAILABLE)
                {
                    instantiateIt = instantiateIt & tile.adjacencyList.Count > 0;
                }
            }
            if (instantiateIt) {
                CreateCraters();
                CreateWaters();
                instantiatedAdjacency = true;
            }
        }
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
                grid[z, x] = Instantiate(defaultTilePrefab).AddComponent<Tile>().SetTile(this, defaultTilePrefab);
                grid[z, x].tileState = TileState.UNCLAIMED;
                grid[z, x].name = $"{x},{z}";
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
                grid[z, x].name = $"Unreachable-{x},{z}";
            }
            if ((z + 1) % 2 == 0 && z >= maxHeight / 2)
            {
                counter--;
            }
        }
        foreach (Player player in manager.players)
        {
            Player.Coordinate coordinates = player.coordinates;
            Tile baseTile = grid[coordinates.x, coordinates.y];
            baseTile.tileState = TileState.UNAVAILABLE;
            baseTile.tileOwner = player.playerID;
            baseTile.terrainType = TerrainTypes.BASE;
        }
        for (int i = 0; i < numberOfFertiles; i++) {
            Tile fertile = grid[maxHeight / 2, maxHeight / 2];
            fertile.terrainType = TerrainTypes.FERTILE;
            fertile.tileOwner = PlayerNumber.NONE;
        }
    }

    private Tile RandomTile() {
        int x = UnityEngine.Random.Range(0, grid.GetLength(1) - 1);
        int y = UnityEngine.Random.Range(0, grid.GetLength(0) - 1);

        if (grid[y, x].tileState == TileState.UNAVAILABLE || ContainsBase(grid[y,x].adjacencyList)) {
            return RandomTile();
        }
        else
        {
            return grid[y, x];
        }
    }

    private void CreateCraters()
    {
        foreach (Player player in manager.players)
        {
            Player.Coordinate coordinates = player.coordinates;
            Tile baseTile = grid[coordinates.x, coordinates.y];
            Tile crater = baseTile.adjacencyList[UnityEngine.Random.Range(0, baseTile.adjacencyList.Count)];
            crater.tileState = TileState.UNAVAILABLE;
            crater.terrainType = TerrainTypes.CRATER;
            crater.tileOwner = PlayerNumber.NONE;
        }
    }

    private void CreateWaters()
    {
        for (int i = 0; i < numberOfWaters; i++)
        {
            Tile water = RandomTile();
            water.terrainType = TerrainTypes.WATER;
            water.tileOwner = PlayerNumber.NONE;
        }
    }

    private bool ContainsBase(List<Tile> adjacencyList) {
        bool containsBase = false;
        foreach (Tile tile in adjacencyList) {
            containsBase = containsBase | tile.terrainType.Equals(TerrainTypes.BASE);
        }
        return containsBase;
    }

}
