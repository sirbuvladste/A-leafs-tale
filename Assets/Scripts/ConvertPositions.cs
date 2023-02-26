using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Hex;
using static MapConstants;

public class ConvertPositions : MonoBehaviour
{
    public static Vector2 ConvertHexCoordsToMap(Vector3Int position, int mapWidth, int mapHeight)
    {
        //using the hex coords, get the map row and column
        int x = position.x + mapWidth / 2;
        int z = position.z + mapHeight / 2;

        return new Vector2(x, z);
    }
    
    public static Vector3 ConvertMapCoordsToWorldPosition(Hex hex, int mapWidth, int mapHeight)
    {
        float x = hex.column * MapConstants.xOffset;
        float z = hex.row * MapConstants.yOffset;
        float y = 0;

        return new Vector3(x, y, z);
    }


}
