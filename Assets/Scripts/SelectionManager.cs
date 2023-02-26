using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static BFS_GraphSearch;

public class SelectionManager : MonoBehaviour
{
    public Camera mainCamera;

    public LayerMask selectionMask;

    public HexMap hexMap;
    private List<Hex> pathHexes;

    public int costRange = 25;

    private void Start()
    {
        if(mainCamera == null)
            mainCamera = Camera.main;
        
        if(hexMap == null)
            hexMap = FindObjectOfType<HexMap>();
    }

    public void HandleSelection(Vector3 mousePosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        GameObject result = null;

        if (Physics.Raycast(ray, out hit, selectionMask))
        {
            result = hit.collider.gameObject;
            Hex hex = result.GetComponent<Hex>();

            // Get the bfs result and highlight the hexes, when another hex is selected, unhighlight the previous hexes
            if (hex.isWalkable)
            {
                if (pathHexes != null)
                {
                    for (int i = 0; i < pathHexes.Count; i++)
                    {
                        if (pathHexes[i] != null)
                            pathHexes[i].DisableHighlight();
                    }
                }

                BFSResult bfsResult = BFS_GraphSearch.BFSGetRange(hexMap, hex, costRange);
                pathHexes = bfsResult.hexesInCostRange;
                for (int i = 0; i < pathHexes.Count; i++)
                {
                    if (pathHexes[i] != null)
                        pathHexes[i].EnableHighlight();
                }
            }

        }
        
    }
}
