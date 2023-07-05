using System.Collections.Generic;
using UnityEngine;

public class ComplexMapGenerator : MapGenerator
{
    private class MapNode
    {
        public int x;
        public int y;
        public bool isWalkable;
        public MapNode parent;
        public int gCost;
        public int hCost;

        public int fCost => gCost + hCost;

        public MapNode(int x, int y, bool isWalkable)
        {
            this.x = x;
            this.y = y;
            this.isWalkable = isWalkable;
        }
    }

    private void Start()
    {
        generateMap();
    }

    private void generateMap()
    {
        base.generateMap(); // Appel de la méthode generateMap() de la classe parente

        // Convertir la liste de GameObjects en une grille de nœuds de carte
        MapNode[,] grid = new MapNode[mapWidth, mapHeight];
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject tile = mapTiles[x + y * mapWidth];
                bool isWalkable = !pathTiles.Contains(tile);
                grid[x, y] = new MapNode(x, y, isWalkable);
            }
        }

        // Utiliser l'algorithme A* pour générer un chemin plus sinueux
        List<MapNode> modifiedPathNodes = FindPath(grid, startTile.transform.position, endTile.transform.position);

        // Convertir les nœuds de chemin en tuiles de chemin
        pathTiles.Clear();
        foreach (MapNode node in modifiedPathNodes)
        {
            GameObject tile = mapTiles[node.x + node.y * mapWidth];
            pathTiles.Add(tile);
            tile.GetComponent<SpriteRenderer>().color = pathColor;
        }
    }

    private List<MapNode> FindPath(MapNode[,] grid, Vector2 startPos, Vector2 endPos)
    {
        MapNode startNode = GetNodeFromPosition(grid, startPos);
        MapNode endNode = GetNodeFromPosition(grid, endPos);

        List<MapNode> openSet = new List<MapNode>();
        HashSet<MapNode> closedSet = new HashSet<MapNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            MapNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (MapNode neighborNode in GetNeighborNodes(grid, currentNode))
            {
                if (!neighborNode.isWalkable || closedSet.Contains(neighborNode))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighborNode);
                if (newMovementCostToNeighbor < neighborNode.gCost || !openSet.Contains(neighborNode))
                {
                    neighborNode.gCost = newMovementCostToNeighbor;
                    neighborNode.hCost = GetDistance(neighborNode, endNode);
                    neighborNode.parent = currentNode;

                    if (!openSet.Contains(neighborNode))
                        openSet.Add(neighborNode);
                }
            }
        }

        // Si aucun chemin n'a été trouvé, retourner une liste vide
        return new List<MapNode>();
    }

    private List<MapNode> RetracePath(MapNode startNode, MapNode endNode)
    {
        List<MapNode> path = new List<MapNode>();
        MapNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private List<MapNode> GetNeighborNodes(MapNode[,] grid, MapNode node)
    {
        List<MapNode> neighbors = new List<MapNode>();

        int[] offsetX = { 0, 0, 1, -1 };
        int[] offsetY = { 1, -1, 0, 0 };

        for (int i = 0; i < offsetX.Length; i++)
        {
            int neighborX = node.x + offsetX[i];
            int neighborY = node.y + offsetY[i];

            if (neighborX >= 0 && neighborX < mapWidth && neighborY >= 0 && neighborY < mapHeight)
            {
                neighbors.Add(grid[neighborX, neighborY]);
            }
        }

        return neighbors;
    }

    private MapNode GetNodeFromPosition(MapNode[,] grid, Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        return grid[x, y];
    }

    private int GetDistance(MapNode nodeA, MapNode nodeB)
    {
        int distX = Mathf.Abs(nodeA.x - nodeB.x);
        int distY = Mathf.Abs(nodeA.y - nodeB.y);
        return distX + distY;
    }
}
