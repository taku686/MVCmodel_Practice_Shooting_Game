using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStrongView : EnemyView
{
    private NavMeshAgent meshAgent;
    private Transform targetTransform;
    public override void Init(EnemyCreateController enemyCreateController, EnemyModel enemyModel, int shellSpeed, Vector3 moveSpeed, float shotWaitTime, EnemyCreateController.EnemyType enemyType)
    {
        base.Init(enemyCreateController, enemyModel, shellSpeed, moveSpeed, shotWaitTime, enemyType);
        meshAgent = GetComponent<NavMeshAgent>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Move(Vector3 moveSpeed, Transform enemyTransform)
    {
        enemyModel.ChaseMove(moveSpeed, this.transform, this.targetTransform);

    }
}
