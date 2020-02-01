﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Tile : MonoBehaviour
{
    public TileState tileState;
    public PlayerNumber tileOwner;
    public List<Tile> adjacencyList;
    public GameObject tileModel;

    public Tile(GameObject tileModel)
    {
        gameObject.tag=
        adjacencyList = new List<Tile>();
        tileOwner = PlayerNumber.NONE;
        tileState = TileState.UNCLAIMED;
        this.tileModel = tileModel;
    }

    public Tile(TileState tileState)
    {
        this.tileState = tileState;
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
