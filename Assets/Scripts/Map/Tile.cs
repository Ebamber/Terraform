﻿using UnityEngine;
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
    public int currentPoints;
    public HexGrid grid;
    public int totalPointValue;
    public bool stolen;
    public GameManager manager;

    private void Awake()
    {
        totalPointValue = 0;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public Tile SetTile(HexGrid grid, GameObject tileModel)
    {
        adjacencyList = new List<Tile>();
        tileOwner = PlayerNumber.NONE;
        tileState = TileState.UNCLAIMED;
        terrainType = TerrainTypes.PLAINS;
        this.tileModel = tileModel;
        currentPoints = 0;
        this.grid = grid;
        return this;
    }

    public Tile SetTile(TileState tileState)
    {
        this.tileState = tileState;
        return this;
    }

    public bool DevelopTile(Player player)
    {
        GetComponent<MeshRenderer>().material.color = player.playerColour;
        if (IsClaimed() && player.playerID == tileOwner)
        {
            if (!terrainType.Equals(TerrainTypes.WATER))
            {
                tileState = TileState.IN_DEVELOPMENT;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
            }
            //special case for water tile
            else
            {
                tileState = TileState.TERRAFORMED;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1f, gameObject.transform.position.z);
            }
            return true;
        }
        else if (IsInDevelopment() && player.playerID == tileOwner)
        {
            tileState = TileState.TERRAFORMED;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1f, gameObject.transform.position.z);
            return true;
        } else if (IsTerraformed())
        {
            return false;
        }
        else return ClaimTile(player.playerID);
    }


    private bool ClaimTile(PlayerNumber player)
    {
        if (IsUnclaimed())
        {
            tileOwner = player;
            tileState = TileState.CLAIMED;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.25f, gameObject.transform.position.z);
            return true;
        }
        else return ClaimEnemyTile(player);
    }

    internal void SwitchOwnership(Player oldPlayer, Player currentPlayer)
    {
        Debug.Log($"From {oldPlayer} to {currentPlayer}");
        oldPlayer.ownedTiles.Remove(this);
        currentPlayer.ownedTiles.Add(this);
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

    private bool ClaimEnemyTile(PlayerNumber player)
    {
        if (IsAvailable() && CanDevelop() && tileOwner != player)
        {
            SwitchOwnership(manager.GetPlayer(this.tileOwner), manager.GetPlayer(player));
            tileOwner = player;
            tileState = TileState.CLAIMED;
            return true;
        }
        return false;
    }

    public bool ClaimTerraformedEnemyTile(PlayerNumber player)
    {
        if (IsAvailable() && CanDevelop() && tileOwner != player && tileState.Equals(TileState.TERRAFORMED))
        {
            SwitchOwnership(manager.GetPlayer(this.tileOwner), manager.GetPlayer(player));
            tileOwner = player;
            tileState = TileState.CLAIMED;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.25f, gameObject.transform.position.z);
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
    private bool IsTerraformed()
    {
        return tileState.Equals(TileState.TERRAFORMED);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Tile"))
        {
            Tile otherTile = other.GetComponent<Tile>();
            otherTile.adjacencyList.Add(this);
        }
    }
}
