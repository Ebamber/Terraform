using UnityEngine;
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
        adjacencyList = new List<Tile>();
        tileOwner = PlayerNumber.NONE;
        this.tileModel = tileModel;
    }

    public void ClaimTile(PlayerNumber player)
    {
        tileOwner = player;
        tileState = TileState.CLAIMED;
    }

    public bool CanDevelop()
    {
        if (tileState.Equals(TileState.CLAIMED) || tileState.Equals(TileState.IN_DEVELOPMENT))
        {
            return false;
        }
        else return true;
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
}
