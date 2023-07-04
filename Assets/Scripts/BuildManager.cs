using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;
    public List<GameObject> uitiles;

    public int selectedTile = 4;

    public Transform tileGridUI;

    private void Start()
    {
        int i = 0;
        foreach (Tile tile in tiles)
        {
            GameObject UITile = new GameObject("UI Tile");
            UITile.transform.parent = tileGridUI;
            UITile.transform.localScale = new Vector3(1f, 1f, 1f);

            Image UIImage = UITile.AddComponent<Image>();
            UIImage.sprite = tile.sprite;

            Color tileColor = UIImage.color;
            tileColor.a = 0.5f;

            if (i == selectedTile)
            {
                tileColor.a = 1f;
            }

            UIImage.color = tileColor;

            uitiles.Add(UITile);

            i++;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(position);

            if (tilemap.HasTile(gridPosition))
            {
                tilemap.SetTile(gridPosition, tiles[selectedTile]);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(position);

            if (tilemap.HasTile(gridPosition))
            {
                tilemap.SetTile(gridPosition, null);
            }
        }
    }

}
