using UnityEngine;

public struct EnemyInfo
{
    public float SpecialScale;
}

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyScaling))]
public class EnemySave : SavedObject
{
    EnemyAI ai;
    EnemyScaling scaling;

    private void Awake()
    {
        ai = GetComponent<EnemyAI>();
        scaling = GetComponent<EnemyScaling>();
    }

    public override object GetOtherData()
    {
        EnemyInfo info = new EnemyInfo()
        {
            SpecialScale = scaling.FinalScale
        };
        return info;
    }

    public override void SetOtherData(object Data)
    {
        ai.ResetMachine();
        EnemyInfo? info = (EnemyInfo?) Data;
        if (Data != null)
        {
            scaling.FinalScale = info.Value.SpecialScale;
        }
    }
}