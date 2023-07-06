using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LessRectilinearMapGenerator : MapGenerator
{
    // Adjust this value to control the randomness of the path
    [SerializeField] private float randomnessFactor = 0.5f;

    private bool ShouldMoveRandomly()
    {
        return Random.value < randomnessFactor;
    }

    private void RandomMove()
    {
        if (ShouldMoveRandomly())
        {
            int randomDirection = Random.Range(0, 4);

            switch (randomDirection)
            {
                case 0: moveUp(); break;
                case 1: moveDown(); break;
                case 2: moveLeft(); break;
                case 3: moveRight(); break;
            }
        }
    }

    public new void generateMap()
    {
        base.generateMap(); // Call the base MapGenerator's generateMap() method to create the map

        currentTile = startTile;
        reachedX = false;
        reachedY = false;

        int loopCount = 0;
        while (!reachedX)
        {
            loopCount++;
            if (loopCount > 100)
            {
                Debug.Log("Loop broken");
                break;
            }

            if (currentTile.transform.position.x > endTile.transform.position.x)
            {
                moveLeft();
            }
            else if (currentTile.transform.position.x < endTile.transform.position.x)
            {
                moveRight();
            }
            else
            {
                reachedX = true;
            }

            RandomMove(); // Add randomness to the path after each movement
        }

        while (!reachedY)
        {
            if (currentTile.transform.position.y > endTile.transform.position.y)
            {
                moveDown();
            }
            else if (currentTile.transform.position.y < endTile.transform.position.y)
            {
                moveUp();
            }
            else
            {
                reachedY = true;
            }

            RandomMove(); // Add randomness to the path after each movement
        }

        pathTiles.Add(endTile);

        foreach (GameObject obj in pathTiles)
        {
            obj.GetComponent<SpriteRenderer>().color = pathColor;
        }

        startTile.GetComponent<SpriteRenderer>().color = startColor;
        endTile.GetComponent<SpriteRenderer>().color = endColor;
    }
}
