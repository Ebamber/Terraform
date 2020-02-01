using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Tile : MonoBehaviour
{
    public TileState tileState;
    public PlayerNumber tileOwner;
    public List<Tile> adjacencyList;
    public GameObject tileModel;

    private void Awake()
    {
        
    }

    public Tile SetTile(GameObject tileModel)
    {
        adjacencyList = new List<Tile>();
        tileOwner = PlayerNumber.NONE;
        tileState = TileState.UNCLAIMED;
        this.tileModel = tileModel;
        return this;
    }

    public Tile SetTile(TileState tileState)
    {
        this.tileState = tileState;
        return this;
    }

    public void ClaimTile(PlayerNumber player)
    {
        if (IsAvailable() && CanDevelop())
        {
            tileOwner = player;
            tileState = TileState.CLAIMED;
        }
    }

    public bool CanDevelop()
    {
        if (tileState.Equals(TileState.CLAIMED) || tileState.Equals(TileState.IN_DEVELOPMENT))
        {
            return true;
        }
        else return false;
    }

    public void DevelopTile()
    {
        if (tileState.Equals(TileState.CLAIMED)){
            tileState = TileState.IN_DEVELOPMENT;
        }
        else{
            tileState = TileState.TERRAFORMED;
        }
    }

    public bool IsAvailable()
    {
        return !tileState.Equals(TileState.UNAVAILABLE);
    }
}
