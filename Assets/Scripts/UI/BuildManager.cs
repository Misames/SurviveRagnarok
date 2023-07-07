using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using TMPro;

public class BuildManager : MonoBehaviour
{
    public Tilemap tilemap;
    public List<GameObject> uitiles;
    public Tile groundTile;
    public int selectedTile = 0;
    public Transform tileGridUI;
    public GameObject buildingInfoPanel;
    public Image buildingImage;
    public TextMeshProUGUI buildingNameText;
    public TextMeshProUGUI buildingHealthText;
    public TextMeshProUGUI buildingCostText;
    public TextMeshProUGUI buildingRangeText;
    public TextMeshProUGUI buildingDamageText;
    public TextMeshProUGUI buildingFireRateText;

    private void Start()
    {
        int i = 0;
        foreach (GameObject buildingObject in GameManager.Instance.buildings)
        {
            Buildings building = buildingObject.GetComponent<Buildings>();
            GameObject UITile = new GameObject("UI Tile");

            UITile.transform.parent = tileGridUI;
            UITile.transform.localScale = new Vector3(1f, 1f, 1f);
            int index = i;

            UITile.AddComponent<Button>().onClick.AddListener(() => OnUITileClicked(index));

            EventTrigger eventTrigger = UITile.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => OnPointerEnter((PointerEventData)data, index));
            eventTrigger.triggers.Add(entry);

            EventTrigger.Entry exit = new EventTrigger.Entry();
            exit.eventID = EventTriggerType.PointerExit;
            exit.callback.AddListener((data) => OnPointerExit((PointerEventData)data));
            eventTrigger.triggers.Add(exit);

            Image UIImage = UITile.AddComponent<Image>();
            UIImage.sprite = building.associatedTile.sprite;

            Color tileColor = UIImage.color;
            tileColor.a = 0.5f;

            if (i == selectedTile)
                tileColor.a = 1f;

            UIImage.color = tileColor;
            uitiles.Add(UITile);
            i++;
        }
    }

    private void OnPointerExit(PointerEventData data)
    {
        buildingInfoPanel.SetActive(false);
    }

    private void OnPointerEnter(PointerEventData data, int index)
    {
        // Vérifiez si l'index est valide
        if (index >= 0 && index < GameManager.Instance.buildings.Length)
        {
            Buildings building = GameManager.Instance.buildings[index].GetComponent<Buildings>();
            Tower tower = GameManager.Instance.buildings[index].GetComponent<Tower>();

            // Mettez à jour le panneau d'informations avec les détails du bâtiment survolé
            buildingNameText.text = building.Name;
            buildingImage.sprite = building.associatedTile.sprite;
            buildingCostText.text = "Coût: " + building.Cost.ToString();
            buildingDamageText.text = "Dégat: " + tower.damage.ToString();
            buildingRangeText.text = "Portée: " + tower.range.ToString();
            buildingFireRateText.text = "Freq. Tir: " + tower.fireRate.ToString();

            // Affichez le panneau d'informations
            buildingInfoPanel.SetActive(true);
        }
    }

    private void Update()
    {
        GameManager gameManager = GameManager.Instance;
        GameObject buildingObject = gameManager.buildings[selectedTile];
        Buildings build = buildingObject.GetComponent<Buildings>();

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = tilemap.WorldToCell(position);
        
        int layerMask = 1 << 7;
        
        

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, position - Camera.main.transform.position,Mathf.Infinity,layerMask);
            bool found = hit.collider != null;
            
            if (tilemap.HasTile(gridPosition))
            {
                if (gameManager.CanAfford(build.Cost) && found == false)
                {
                    gameManager.SpendGold(build.Cost);
                    GameObject newBuilding = Instantiate(buildingObject, tilemap.GetCellCenterWorld(gridPosition), Quaternion.identity);
                    Vector3 p = newBuilding.transform.position;
                    newBuilding.transform.position = new Vector3(p.x,p.y,0);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, position - Camera.main.transform.position,Mathf.Infinity,layerMask);
            bool found = hit.collider != null;

            if (tilemap.HasTile(gridPosition) && found == true)
            {
                Destroy(hit.transform.root.gameObject);
                gameManager.EarnGold(build.Cost);
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
