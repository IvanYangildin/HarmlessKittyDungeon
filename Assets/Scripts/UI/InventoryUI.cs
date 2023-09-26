using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private List<InventoryCell> cells;
    private int currEmpty = 0;

    // return false if can't add item
    public bool AddItem(Weapon weapon)
    {
        if (currEmpty < cells.Count)
        {
            cells[currEmpty].SetItem(weapon);
            currEmpty++;
            return true;
        }
        return false;
    }
}
