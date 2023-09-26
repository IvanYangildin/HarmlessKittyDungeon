using System.Collections.Generic;
using UnityEngine;


public class GrowthSword : AbstractSword
{
    [SerializeField]
    private float strength;

    public override void TargetAffect(GameObject target)
    {
        EnemyScaling enemy = target.GetComponent<EnemyScaling>();
        if (enemy != null)
        {
            enemy.AddToFinalSize(strength);
        }
    }
}