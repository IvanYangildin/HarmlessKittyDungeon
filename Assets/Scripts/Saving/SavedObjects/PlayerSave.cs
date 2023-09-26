using UnityEngine;


[RequireComponent(typeof(PlayerProfile))]
public class PlayerSave : SavedObject
{
    private PlayerProfile profile;

    private void Awake()
    {
        profile = GetComponent<PlayerProfile>();
    }

    public override object GetOtherData()
    {
        return null;
    }

    public override void SetOtherData(object Data)
    {
        profile.Health = profile.MaxHealth;
    }
}