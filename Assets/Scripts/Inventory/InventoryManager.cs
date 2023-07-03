using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public TileManager TileManager;
    public InventorySlot[] inventorySlots;

    int selectedSlot = -1;

    void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0)
            inventorySlots[selectedSlot].Deselect();

        if (newValue < 0)
            newValue = 0;

        while(inventorySlots[newValue].selectedObject == null)
        {
            newValue--;
        }

        inventorySlots[newValue].Select();

        TileManager.SelectObject(inventorySlots[newValue].selectedObject);

        selectedSlot = newValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeSelectedSlot(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 10)
            {
                ChangeSelectedSlot(number - 1);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            ChangeSelectedSlot(selectedSlot - 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            ChangeSelectedSlot(selectedSlot + 1);
        }

    }
}
