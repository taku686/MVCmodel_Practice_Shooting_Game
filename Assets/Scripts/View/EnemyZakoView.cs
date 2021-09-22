using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZakoView : EnemyView
{
    public override void Move(Vector3 moveSpeed, Transform enemyTransform)
    {
        enemyModel.StraightMove(moveSpeed,enemyTransform);
    }
}
