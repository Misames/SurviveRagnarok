using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;
    public List<GameObject> uitiles;

    public Tile groundTile;

    public int selectedTile = 0;

    public Transform tileGridUI;

    private void Start()
    {
        int i = 0;
        foreach (Tile tile in tiles)
        {
            GameObject UITile = new GameObject("UI Tile");
            UITile.transform.parent = tileGridUI;
            UITile.transform.localScale = new Vector3(1f, 1f, 1f);

            int index = i; 

            UITile.AddComponent<Button>().onClick.AddListener(() => OnUITileClicked(index));

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
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = tilemap.WorldToCell(position);

        if (Input.GetMouseButtonDown(0))
        {
            if (tilemap.HasTile(gridPosition))
            {
                tilemap.SetTile(gridPosition, tiles[selectedTile]);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {

            if (tilemap.HasTile(gridPosition))
            {
                tilemap.SetTile(gridPosition, groundTile);
            }
        }


    }

    private void OnUITileClicked(int index)
    {
        selectedTile = index;

        // Mettez à jour l'apparence des objets de l'UI pour refléter la sélection
        for (int i = 0; i < uitiles.Count; i++)
        {
            Image UIImage = uitiles[i].GetComponent<Image>();

            Color tileColor = UIImage.color;
            tileColor.a = (i == selectedTile) ? 1f : 0.5f;

            UIImage.color = tileColor;
        }
    }

}

