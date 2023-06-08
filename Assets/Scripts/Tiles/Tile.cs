using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject objectPrefab;
    public TileManager TileManager;

    private bool isAvailable = true;

    public void Awake()
    {
        TileManager = FindObjectOfType<TileManager>();
    
        if(TileManager == null)
        {
            Debug.LogError("TILE MANAGER NON TROUVÉ");
        }
    }

    public void Update()
    {
        objectPrefab = TileManager.selectedPrefab;
    }

    private void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0) && isAvailable)
        {
            Instantiate(objectPrefab, transform.position, Quaternion.identity);
            isAvailable = false;
        }
    }

}
