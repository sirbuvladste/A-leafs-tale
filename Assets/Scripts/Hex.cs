using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ConvertPositions;

public class Hex : MonoBehaviour
{
    // The position of the hex relative to the center of the map.
    [Header("Hex coordinates")]
    public int column; // column
    public int row; // rowow
    public Vector3Int hexCoords; // hex coordinates
    
    [Header("Hex properties")]
    public GlowSelected highlighter;

    [Header("Hex type")]
    public HexType hexType;
    public HexProp hexProp;
    public bool isWalkable; // is the hex walkable?
    public int movementCost; // the cost of moving to this hex


    // TODO:
    // public bool isVisible;  // is the hex visible to the player?
   
    void Awake()
    {
        // get the position of the hex relative to the center of the map
        hexCoords = ConvertPositionToOffset(transform.position);
        highlighter = GetComponent<GlowSelected>();
        isWalkable = SetWalkable();
        movementCost = GetMovementCost();

    }

    private Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x / MapConstants.xOffset);
        int z = Mathf.CeilToInt(position.z / MapConstants.zOffset);
        int y = Mathf.CeilToInt(position.y / MapConstants.yOffset);

        return new Vector3Int(x, y, z);
    }

    public void EnableHighlight()
    {
        highlighter.Glow();
    }

    public void DisableHighlight()
    {
        highlighter.UnGlow();
    }

    public bool SetWalkable()
    {
        //check if the hex type is not walkable
        switch (hexType)
        {
            case HexType.Water:
                return false;
            case HexType.BlackRock:
                return false;
            default:
               if(hexProp == HexProp.Obstacle)
                {
                    return false;
                }
                else
                    return true;
                
        }
        //check if the hex has a prop that is not walkable
        
    }

    public int GetMovementCost()
    {
        // set the movement cost based on the hex type
        switch (hexType)
        {
            case HexType.Grass:
                movementCost = 10;
                break;
            case HexType.Road:
                movementCost = 5;
                break;
            default:
                movementCost = 100;
                break;
        }

        // add the cost of the prop
        switch (hexProp)
        {
            case HexProp.Forest:
                movementCost += 10;
                break;
            case HexProp.Rocks:
                movementCost += 20;
                break;
            case HexProp.House:
                movementCost += 5;
                break;
            case HexProp.Mine:
                movementCost += 15;
                break;
            case HexProp.Castle:
                movementCost += 25;
                break;
            default:
                break;
        }        

        return movementCost;
    }


    public override string ToString()
    {
        return ("Hex coords: " + hexCoords.ToString() + " | " + "Map coords: " + column + ", " + row + " | " + "World position: " + transform.position.ToString() + "");
    }

}

public enum HexType
{
    Grass,
    Water,
    Rock,
    BlackRock,
    Sand,
    Road
}

public enum HexProp
{
    None,
    Obstacle,
    Forest,
    Rocks,
    House,
    Mine,
    Castle
    
}