using System;
using UnityEngine;

[Serializable]
public class EnemyChase : StateMachine.State
{
    Transform target;
    Enemy enemy;
    [SerializeField]
    float detectDistance;
    public float DetecDistance => detectDistance;


    public override void Begin(StateMachine machine)
    {
        target = machine.data["Target"] as Transform;
        enemy = machine.data["Enemy"] as Enemy;
    }

    public override void Behave(float dt)
    {
        enemy.moveToPoint(target.position);
    }

    public bool StopChasing()
    {
        return (target.position - enemy.transform.position).sqrMagnitude > (detectDistance * detectDistance);
    }
}