using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Tile : MonoBehaviour
{
    public TileState tileState;
    public TerrainTypes terrainType;
    public PlayerNumber tileOwner;
    public List<Tile> adjacencyList;
    public GameObject tileModel;
<<<<<<< HEAD
    public int currentPoints;
    public HexGrid grid;
    public int totalPointValue;
    public bool stolen;

    private void Awake()
    {
        totalPointValue = 0;
=======

    private void Awake()
    {
        
>>>>>>> parent of 778a026... Changing Terrain Types
    }

    public Tile SetTile(GameObject tileModel)
    {
        adjacencyList = new List<Tile>();
        tileOwner = PlayerNumber.NONE;
        tileState = TileState.UNCLAIMED;
        terrainType = TerrainTypes.PLAINS;
        this.tileModel = tileModel;
        return this;
    }

    public Tile SetTile(TileState tileState)
    {
        this.tileState = tileState;
        return this;
    }

    public bool DevelopTile(Player player)
    {
        if (IsClaimed() && player.playerID == tileOwner)
        {
            if (!terrainType.Equals(TerrainTypes.WATER))
            {
                tileState = TileState.IN_DEVELOPMENT;
            }
            //special case for water tile
            else {
                tileState = TileState.TERRAFORMED;
            }
            return true;
        }
        else if (IsInDevelopment() && player.playerID == tileOwner)
        {
            tileState = TileState.TERRAFORMED;
            return true;
        }
        else return ClaimTile(player.playerID);
    }

    public bool ClaimTile(PlayerNumber player)
    {
        if (IsUnclaimed()) {
            tileOwner = player;
            tileState = TileState.CLAIMED;
            return true;
        }
        else return ClaimEnemyTile(player);
    }

    public int CalculateBonusPoints()
    {
        if (terrainType.Equals(TerrainTypes.FERTILE) && tileState.Equals(TileState.TERRAFORMED)){
            return 2;
        }
        else if (terrainType.Equals(TerrainTypes.FERTILE) && tileState.Equals(TileState.TERRAFORMED))
        {
            return (int) tileState; //this represents the points from the development stage that were skipped
        }
            return 0;
    }

    public bool ClaimEnemyTile(PlayerNumber player)
    {
        if (IsAvailable() && CanDevelop() && tileOwner != player)
        {
            return true;
        }
        else return false;
    }

    public bool IsAvailable()
    {
        return !tileState.Equals(TileState.UNAVAILABLE);
    }

    public bool CanDevelop()
    {
        return (tileState.Equals(TileState.UNCLAIMED) || tileState.Equals(TileState.CLAIMED) || tileState.Equals(TileState.IN_DEVELOPMENT));

    }

    private bool IsUnclaimed()
    {
        return tileState.Equals(TileState.UNCLAIMED);
    }
    private bool IsClaimed()
    {
        return tileState.Equals(TileState.CLAIMED);
    }
    private bool IsInDevelopment()
    {
        return tileState.Equals(TileState.IN_DEVELOPMENT);
    }


}
