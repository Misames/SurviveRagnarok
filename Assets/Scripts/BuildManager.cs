using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;
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

    private void Start()
    {
        int i = 0;
        foreach (Buildings building in GameManager.Instance.buildings)
        {
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
            {
                tileColor.a = 1f;
            }

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
            Buildings building = GameManager.Instance.buildings[index];

            // Mettez à jour le panneau d'informations avec les détails du bâtiment survolé
            buildingNameText.text = building.Name;
            buildingImage.sprite = building.associatedTile.sprite;
            buildingHealthText.text = "Health: " + building.Health.ToString();
            buildingCostText.text = "Cost: " + building.Cost.ToString();

            // Affichez le panneau d'informations
            buildingInfoPanel.SetActive(true);
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
                tilemap.SetTile(gridPosition, GameManager.Instance.buildings[selectedTile].associatedTile);
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

