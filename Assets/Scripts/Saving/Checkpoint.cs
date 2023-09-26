using UnityEngine;

[RequireComponent(typeof(StateSaver))]
public class Checkpoint : MonoBehaviour
{
    StateSaver stateSaver;
    [SerializeField]
    PlayerProfile player;

    bool isUsed = false;

    private void Awake()
    {
        stateSaver = GetComponent<StateSaver>();
    }

    public void SetCheckpoint()
    {
        if (!isUsed)
        {
            player.LastCheckpoint = stateSaver;
            stateSaver.SaveState();
            isUsed = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}