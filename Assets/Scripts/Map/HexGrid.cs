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
                CreateWaters(grid[maxHeight / 2, maxHeight / 2]);
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
                grid[z, x].gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
            }
            for (int x = currentHeight; x < maxHeight; x++) {
                grid[z, x] = Instantiate(emptyGO).AddComponent<Tile>().SetTile(TileState.UNAVAILABLE);
                grid[z, x].tileOwner = PlayerNumber.NONE;
                grid[z, x].terrainType = TerrainTypes.CRATER;
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
            player.ownedTiles.Add(baseTile);
            Debug.Log(player.playerID);
            baseTile.tileState = TileState.UNAVAILABLE;
            baseTile.tileOwner = player.playerID;
            baseTile.terrainType = TerrainTypes.BASE;
            baseTile.gameObject.GetComponent<MeshRenderer>().material.color = player.playerColour;
        }
        for (int i = 0; i < numberOfFertiles; i++) {
            Tile fertile = grid[maxHeight / 2, maxHeight / 2];
            fertile.terrainType = TerrainTypes.FERTILE;
            fertile.tileOwner = PlayerNumber.NONE;
            fertile.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    private void CreateCraters()
    {
        foreach (Player player in manager.players)
        {
            Player.Coordinate coordinates = player.coordinates;
            Tile baseTile = grid[coordinates.x, coordinates.y];
            Tile crater;
            do
            {
                crater = baseTile.adjacencyList[UnityEngine.Random.Range(0, baseTile.adjacencyList.Count)];
            } while (crater.terrainType == TerrainTypes.CRATER);
            crater.tileState = TileState.UNAVAILABLE;
            crater.terrainType = TerrainTypes.CRATER;
            crater.tileOwner = PlayerNumber.NONE;
            crater.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }

    private void CreateWaters(Tile fertile)
    {
        for (int i = 0; i < numberOfWaters; i++)
        {
            Tile water;
            do
            {
                water = fertile.adjacencyList[UnityEngine.Random.Range(0, fertile.adjacencyList.Count)];
            } while (water.terrainType == TerrainTypes.WATER);
            water.terrainType = TerrainTypes.WATER;
            water.tileOwner = PlayerNumber.NONE;
            //placeholder
            water.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }


}
