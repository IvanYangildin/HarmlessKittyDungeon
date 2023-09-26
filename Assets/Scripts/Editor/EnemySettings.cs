using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyAI))]
[CanEditMultipleObjects]
public class EnemySettings : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Set idle position to current position"))
        {
            foreach (EnemyAI enemyAI in targets)
            {
                enemyAI.IdleState.IdlePlace = enemyAI.transform.position;
                EditorUtility.SetDirty(enemyAI);
            }
        }
    }
}
