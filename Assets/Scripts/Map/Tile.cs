using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Tile : MonoBehaviour
{
    private TileState tileState { get; set; }
    private PlayerNumber tileOwner { get; set; }
    List<Tile> adjacencyList;
    public GameObject tileModel;

    public Tile()
    {
        adjacencyList = new List<Tile>();
        tileOwner = PlayerNumber.NONE;
        tileState = TileState.UNEXPLORED;
    }

}
