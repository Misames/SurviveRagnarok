using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacementController : MonoBehaviour
{
    public Tilemap tilemap; // Référence à la Tilemap sur laquelle vous voulez placer les tuiles
    public TileBase tileToPlace; // La tuile que vous voulez placer

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Vérifie si le bouton de la souris gauche est enfoncé
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
            tilemap.SetTile(cellPos, tileToPlace);
        }
    }
}