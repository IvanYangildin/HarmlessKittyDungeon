using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAI : StateMachine
{
    Enemy enemy;

    [SerializeField]
    private bool distanceIgnore;
    // if true, enemy doesn't bother about distance between player
    public bool DistanceIgnore
    {
        get 
        { 
            return distanceIgnore; 
        }
        set
        {
            distanceIgnore = value;
            ClearData();
            BuildTransitions();
        }
    }

    [SerializeField]
    EnemyIdle enemyIdle = new EnemyIdle();
    public EnemyIdle IdleState => enemyIdle;
    [SerializeField]
    EnemyChase enemyChase = new EnemyChase();
    public EnemyChase ChaseState => enemyChase;

    protected override State StartState => enemyIdle;

    protected override void ClearData()
    {
        enemyIdle.Transitions.Clear();
        enemyChase.Transitions.Clear();
    }

    protected override void BuildTransitions()
    {
        if (!DistanceIgnore)
        {
            enemyIdle.Transitions.Add(new Transition(enemyChase, enemyIdle.TargetDetected));
            enemyChase.Transitions.Add(new Transition(enemyIdle, enemyChase.StopChasing));
        }
    }

    protected override void InitializeData()
    {
        enemy = GetComponent<Enemy>();
        data["Enemy"] = enemy;
        data["DistanceIgnore"] = DistanceIgnore;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyIdle.DetecDistance);
        Gizmos.DrawWireSphere(transform.position, enemyChase.DetecDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(enemyIdle.IdlePlace, Vector3.one * 0.5f);
    }

    public void SetChase(Transform target)
    {
        enemyIdle.target = target.transform;
        ChanegState(enemyChase);
    }

    public void SetIdle()
    {
        ChanegState(enemyIdle);
    }

}