using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    InventoryUI inventory;

    private List<Weapon> possibleWeapons = new List<Weapon>();

    [SerializeField]
    private StateSaver arsenal;
    public StateSaver Arsenal => arsenal;

    public int WeaponsCount => possibleWeapons.Count;

    private int currentWeaponInd = 0;

    public Weapon CurrWeapon => possibleWeapons[currentWeaponInd];

    private void Awake()
    {
        foreach (var weapon in Arsenal.transform.GetComponentsInChildren<Weapon>())
        {
            AddNewWeapon(weapon);
        }
    }

    public void Attack()
    {
        CurrWeapon?.OnAttack();
    }

    public void StopAttack()
    {
        CurrWeapon?.OnStopAttack();
    }

    public void ChoseWeapon(int w_ind)
    {
        if ((w_ind >= 0) && (w_ind < possibleWeapons.Count) && (w_ind != currentWeaponInd))
        {
            possibleWeapons[currentWeaponInd].gameObject.SetActive(false);
            currentWeaponInd = w_ind;
            possibleWeapons[currentWeaponInd].gameObject.SetActive(true);
            arsenal.LoadGO(possibleWeapons[currentWeaponInd].gameObject);
        }
    }

    public void ChangeWeapon(float delta)
    {
        int intDelta = Mathf.RoundToInt(delta);
        intDelta = (intDelta != 0) ? intDelta/Mathf.Abs(intDelta) : 0;
        ChoseWeapon((currentWeaponInd + intDelta + possibleWeapons.Count) % possibleWeapons.Count);
    }

    public void AddNewWeapon(Weapon weapon)
    {
        weapon.transform.parent = arsenal.transform;
        arsenal.SaveGO(weapon.gameObject);

        inventory.AddItem(weapon);

        possibleWeapons.Add(weapon);
        ChoseWeapon(WeaponsCount - 1);
    }

}