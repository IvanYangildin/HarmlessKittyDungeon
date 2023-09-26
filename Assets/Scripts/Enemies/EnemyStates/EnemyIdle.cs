using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyIdle : StateMachine.State
{
    [SerializeField]
    // to this point Enemy will trye to come back if he can
    Vector3 idlePlace;
    public Vector3 IdlePlace { get => idlePlace; set { idlePlace = value; } }

    Enemy enemy;
    EnemyAI ai;

    [SerializeField]
    float detectDistance;
    public float DetecDistance => detectDistance;

    public Transform target;

    public override void Behave(float dt)
    {
        enemy.moveToPoint(idlePlace);
    }

    public override void Begin(StateMachine machine)
    {
        enemy = machine.data["Enemy"] as Enemy;
    }

    public override void End(StateMachine machine)
    {
        machine.data["Target"] = target;
    }

    public bool TargetDetected()
    {
        float sqrDist = detectDistance * detectDistance;
        foreach (var go in GameObject.FindGameObjectsWithTag("EnemyTargets"))
        {
            if ((go.transform.position - enemy.transform.position).sqrMagnitude < sqrDist)
            {
                target = go.transform;
                return true;
            }
        }
        return false;
    }

}