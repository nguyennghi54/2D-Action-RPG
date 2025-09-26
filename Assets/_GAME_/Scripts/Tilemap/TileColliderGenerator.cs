using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Find Entire Staircase To Generate Collider
/// by checking 4 adjacent tiles.
/// </summary>
public class TileColliderGenerator : MonoBehaviour
{
    [Header("TileGenerator")]
    [SerializeField] private Tilemap stairMap;
    [SerializeField] private GameObject stairColliderPrefab;
    [SerializeField] private TileBase[] stairTile;
    [Header("ElevationEntry")]
    [SerializeField] private GameObject elevationHigh;
    [SerializeField] private GameObject elevationLow;
    [SerializeField] private GameObject player;
    
    private HashSet<Vector3Int> visitedTiles = new HashSet<Vector3Int>();
    void Start()
    {
        var firstPos = stairMap.cellBounds.min;
        foreach (Vector3Int pos in stairMap.cellBounds.allPositionsWithin)  // traverse all cells (unordered!)
        {
            foreach (TileBase stairType in stairTile)   // check if cell contains stair tile
            {
                if (stairMap.GetTile(pos) == stairType && !visitedTiles.Contains(pos))
                {
                    List<Vector3Int> currentConnectedTileList = FindConnectedTile(pos); // create list of connected tiles
                    GenerateCollider(currentConnectedTileList);
                }
            }
            
        }
    }
    
    // Get queue of adjacents & check for their unvisited adjacents 
    private List<Vector3Int> FindConnectedTile(Vector3Int startPos)
    {
        List<Vector3Int> staircase =  new List<Vector3Int>();
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        
        queue.Enqueue(startPos);
        visitedTiles.Add(startPos);
        
        while (queue.Count > 0) 
        {
            Vector3Int currentPos = queue.Dequeue();  // remove checked tiles
            staircase.Add(currentPos);
            // Check adjacent tiles (4-way flood fill)
            Vector3Int[] adjacentTiles =
            {
                new Vector3Int(currentPos.x - 1, currentPos.y, 0), // left
                new Vector3Int(currentPos.x + 1, currentPos.y, 0), // right
                new Vector3Int(currentPos.x, currentPos.y - 1, 0), // up
                new Vector3Int(currentPos.x, currentPos.y + 1, 0), // down
            };
            foreach (var pos in adjacentTiles)
            {
                foreach (TileBase stairType in stairTile)
                {
                    if(stairMap.GetTile(pos) == stairType && !visitedTiles.Contains(pos))
                    {
                        visitedTiles.Add(pos);
                        queue.Enqueue(pos);    
                    }
                }
            }
        }
        return staircase;
    }
    
    private void GenerateCollider(List<Vector3Int> tileList)
    {
        if (tileList.Count == 0) return;
        // Get min/max bounds of collider
        int minX = tileList[0].x, maxX = tileList[0].x;
        int minY = tileList[0].y, maxY = tileList[0].y;
        foreach (var tilePos in tileList)
        {
            if (tilePos.x < minX) minX = tilePos.x;
            if (tilePos.x > maxX) maxX = tilePos.x;
            if (tilePos.y < minY) minY = tilePos.y;
            if (tilePos.y > maxY) maxY = tilePos.y;
        }
        // Calculate size of collider
        Vector3 minBoundPos = stairMap.CellToWorld(new Vector3Int(minX, minY, 0));
        Vector3 maxBoundPos = stairMap.CellToWorld(new Vector3Int(maxX+1, maxY+1, 0));
        Vector3 center = (minBoundPos + maxBoundPos) / 2f;
        Vector3 size = maxBoundPos - minBoundPos;
        // Generate collider
        GameObject newCollider = Instantiate(stairColliderPrefab, center, Quaternion.identity);
        newCollider.GetComponent<ElevationEntry>().elevationHigh =  elevationHigh;
        newCollider.GetComponent<ElevationEntry>().elevationLow = elevationLow;
        newCollider.GetComponent<ElevationEntry>().player = player;
        newCollider.transform.localScale = size;
        newCollider.transform.parent = transform;
    }
}
