using UnityEngine;
using UnityEditor;

public class Coordinate {
    public int x, y;

    public Coordinate(int mapConstraintX, int mapConstraintY)
    {
        this.x = Random.Range(0,mapConstraintX);
        this.y = Random.Range(0, mapConstraintY);
    }
}