using UnityEngine;

public class ShakyObject : MonoBehaviour
{
    [SerializeField]
    DoorRot shakyControl;

    private void Awake()
    {
        shakyControl.OnOpen += shakyControl.Close;
    }

    public void Shake()
    {
        shakyControl.Open();
    }
}