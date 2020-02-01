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
        tileState = TileState.UNEXPLORED;
        this.tileModel = tileModel;
    }

}
