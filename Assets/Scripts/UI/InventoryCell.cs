using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    public void SetItem(Weapon item)
    {
        itemIcon.sprite = item.WeaponIcon;
    }
}
