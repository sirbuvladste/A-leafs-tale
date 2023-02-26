using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Hex;
using static MapConstants;
using static ConvertPositions;

public class HexMap : MonoBehaviour
{
    private Hex[,] hexes;
    private int mapWidth;
    private int mapHeight;

    // In order to have negative coordinates, 
    // we need to shift the hex coordinates to the right and up.
    private int mapCenterX;
    private int mapCenterY;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap(100, 100);
        
        // print the hexes in the map
        // for (int i = 0; i < hexes.GetLength(0); i++)
        // {
        //     for (int j = 0; j < hexes.GetLength(1); j++)
        //     {
        //         if(hexes[i, j] != null)
        //             Debug.Log(hexes[i, j].ToString());
        //     }
        // }
    }

    // Create "map" class to manage the hex data. The map is a 2D array of hexes (width and height).
    //TODO: Make this to procedurally generate the map.
   
    public void GenerateMap(int width, int height)
    {
        hexes = new Hex[width, height];
        mapWidth = width;
        mapHeight = height;

        mapCenterX = width / 2;
        mapCenterY = height / 2;

        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            Vector2 mapCoords = ConvertHexCoordsToMap(hex.hexCoords, mapWidth, mapHeight);
            hex.column = (int)mapCoords.x;
            hex.row = (int)mapCoords.y;
            hexes[hex.column, hex.row] = hex;
        }
    }

    // Get the hex at the given coordinates.
    public Hex GetHexAt(int column, int row)
    {
        if (column < 0 || 
            column >= hexes.GetLength(0) ||
            row < 0 ||
            row >= hexes.GetLength(1))
        {
            Debug.LogError("Hex out of range!" + hexes[column, row].ToString());
            throw new System.IndexOutOfRangeException();
            
        }
        return hexes[column, row];
    }

    // Get a list of all the neighbors of the given hex.
    public List<Hex> GetNeighborsOfHex(Hex hex)
    { 

        List<Hex> neighbors = new List<Hex>();
        List<Vector3Int> directions;

        if(hex.row % 2 == 0)
        {
            directions = MapConstants.evenDirectionsCoordinates;
        }
            else 
        {
            directions = MapConstants.oddDirectionsCoordinates;
        }

        foreach (Vector3Int direction in directions)
        {
            Hex neighbor = GetHexAt(hex.column + direction.x, hex.row + direction.z);
            
            if (neighbor != null)
                neighbors.Add(neighbor);
        }
        return neighbors;
    }

}
