using UnityEngine;
using System.Collections;

public class HexGrid : MonoBehaviour
{
    public int x;
    public int y;

    public Tile[,] grid;


    void Awake()
    {
        //instantiate the grid to be used in the turn manager
        grid = new Tile[y,x];
        for (int i = 0; i < y; i++) {
            for (int j = 0; j < x; j++) {
                grid[i, j] = new Tile();
            }
        }
    }

}
