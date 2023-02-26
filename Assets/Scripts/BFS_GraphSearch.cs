using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS_GraphSearch : MonoBehaviour
{
    public static BFSResult BFSGetRange(HexMap hexMap, Hex startHex, int costRange)
    {
        List<Hex> hexesInCostRange = new List<Hex>(); 
        // List<Hex> hexesInPath = new List<Hex>(); // TODO: May be useful when using movement abilities

        Queue<Hex> hexesToVisit = new Queue<Hex>();
        Dictionary<Hex, int> hexDistances = new Dictionary<Hex, int>();
        Dictionary<Hex, int> costSoFar = new Dictionary<Hex, int>(); // the total cost of getting to this hex from the start

        hexesToVisit.Enqueue(startHex);
        hexDistances.Add(startHex, 0);
        costSoFar.Add(startHex, 0);

        while (hexesToVisit.Count > 0)
        {
            Hex currentHex = hexesToVisit.Dequeue();
            int currentDistance = hexDistances[currentHex];
            int currentCost = costSoFar[currentHex];

            hexesInCostRange.Add(currentHex);

            foreach (Hex neighborHex in hexMap.GetNeighborsOfHex(currentHex))
            {
                int newCost = currentCost + neighborHex.movementCost;
                // debugToLog(neighborHex, newCost, currentCost, costRange, costSoFar);
                
                // check if:
                // - is already in the dictionary
                // - the new cost is less than cost range
                // - is walkable
                // Plus if there is a new cheaper path to this hex, update the cost to recheck the neighbors
                // Add the hex to the queue to be checked
                if (neighborHex.isWalkable && newCost <= costRange)
                {
                    if(!costSoFar.ContainsKey(neighborHex)){
                        costSoFar.Add(neighborHex, newCost);
                        hexesToVisit.Enqueue(neighborHex);
                        hexDistances.Add(neighborHex, currentDistance + 1);
                    }
                    else if (newCost < costSoFar[neighborHex])
                    {
                        costSoFar[neighborHex] = newCost;
                        hexesToVisit.Enqueue(neighborHex);
                        hexDistances[neighborHex] = currentDistance + 1;
                    }

                }
                
            }
        }

        return new BFSResult(hexesInCostRange, null);

    }

    private static void debugToLog(Hex neighborHex, int newCost, int currentCost, int costRange, Dictionary<Hex, int> costSoFar)
    {
        Debug.Log("Neighbor: " + neighborHex.ToString() 
                            + "\nnewCost: " + newCost
                            + "\ncurrentCost: " + currentCost
                            + "\nmovementCost: " + neighborHex.movementCost
                            + "\ncostSoFar.ContainsKey(neighborHex): " + costSoFar.ContainsKey(neighborHex)
                            + "\nisWalkable: " + neighborHex.isWalkable);
    }

}

public class BFSResult
{
    public List<Hex> hexesInCostRange;
    public List<Hex> hexesInPath;

    public BFSResult(List<Hex> hexesInCostRange, List<Hex> hexesInPath)
    {
        this.hexesInCostRange = hexesInCostRange;
        this.hexesInPath = hexesInPath;
    }
}