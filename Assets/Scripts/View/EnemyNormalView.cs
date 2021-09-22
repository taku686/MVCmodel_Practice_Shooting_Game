using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalView : EnemyView
{
    public override void Move(Vector3 moveSpeed, Transform enemyTransform)
    {
        enemyModel.ZigzagMove(moveSpeed, enemyTransform);
    }
}
