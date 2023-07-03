using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject objectPrefab;
    public TileManager TileManager;

    private bool isAvailable = true;
    private bool isOver = false;

    private Quaternion rotation = Quaternion.identity;

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
            var newObjectOnTile = Instantiate(objectPrefab, transform.position, rotation);
            newObjectOnTile.transform.parent = gameObject.transform;
            newObjectOnTile.layer = 6;
            isAvailable = false;
        }

    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if(isOver == false && isAvailable)
        {

            var newObjectOnTile = Instantiate(objectPrefab, transform.position, rotation);

            newObjectOnTile.gameObject.tag = "TEMPORARY";
            newObjectOnTile.transform.parent = gameObject.transform;

            var material = newObjectOnTile.GetComponent<SpriteRenderer>().material;
            var color = material.GetColor("_Color");
            color.a = 0.5f;

            material.SetColor("_Color", color);

            isOver = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isAvailable) return;

            rotation *= Quaternion.Euler(0, 0, 90);

            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                // only destroy tagged object
                if (gameObject.transform.GetChild(i).gameObject.layer == 6) // COMPARE TO LAYER "BUILDINGS"
                    Destroy(gameObject.transform.GetChild(i).gameObject);
            }

            isOver = false;
        }

        if (Input.GetMouseButtonDown(1))
        {

            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                // only destroy tagged object
                if (gameObject.transform.GetChild(i).gameObject.layer == 6) // COMPARE TO LAYER "BUILDINGS"
                    Destroy(gameObject.transform.GetChild(i).gameObject);
            }
            isAvailable = true;
        }
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        isOver = false;

        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            // only destroy tagged object
            if (gameObject.transform.GetChild(i).gameObject.tag == "TEMPORARY")
                Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }

}
