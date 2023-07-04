using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject selectedPrefab;
    public GameObject TilePrefab;

    public int gridSizeX = 1;
    public int gridSizeY = 1;

    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectObject(GameObject prefabSelected)
    {
        selectedPrefab = prefabSelected;
    }

    public void CreateGrid()
    {
        for (int x = -6; x < gridSizeX; x++)
        {
            for (int y = -2; y < gridSizeY; y++)
            {
                Vector3 position = new Vector3(x* 1.2f, y * 1.2f, 0);

                GameObject newTile = Instantiate(TilePrefab, position, Quaternion.identity);

                //Tile tileComponent = newTile.GetComponent<Tile>();

                //tileComponent.objectPrefab = selectedPrefab;
            }
        }
    }
}
