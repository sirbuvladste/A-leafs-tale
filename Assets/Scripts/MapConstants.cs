using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapConstants
{
    public static float xOffset = 2f;
    public static float yOffset = 1f;
    public static float zOffset = 1.73f;

    public static List<Vector3Int> oddDirectionsCoordinates = new List<Vector3Int>()
    {
        new Vector3Int(-1, 0, 1), // NW - A
        new Vector3Int(0, 0, 1), // NE - B
        new Vector3Int(1, 0, 0), // E - C
        new Vector3Int(0, 0, -1), // SE - D
        new Vector3Int(-1, 0, -1), // SW - E
        new Vector3Int(-1, 0, 0) // W - F
    };

    public static List<Vector3Int> evenDirectionsCoordinates = new List<Vector3Int>()
    {
        new Vector3Int(0, 0, 1), // NW - A
        new Vector3Int(1, 0, 1), // NE - B
        new Vector3Int(1, 0, 0), // E - C
        new Vector3Int(1, 0, -1), // SE - D
        new Vector3Int(0, 0, -1), // SW - E
        new Vector3Int(-1, 0, 0) // W - F
    };

    

}