using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCreateController : BaseController
{
    [SerializeField]
    private GameObject enemyObj;
    [SerializeField]
    private Transform enemyCreatePos;
    [SerializeField]
    private int weakShellSpeed;
    [SerializeField]
    private Vector3 weakEnemyMoveSpeed;
    [SerializeField]
    private int normalShellSpeed;
    [SerializeField]
    private Vector3 normalEnemyMoveSpeed;
    [SerializeField]
    private int strongShellSpeed;
    [SerializeField]
    private Vector3 strongEnemyMoveSpeed;


    private EnemyModel enemyModel;
    public List<EnemyView> enemyViewList = new List<EnemyView>();
//    public EnemyType enemyType;
    private const float CreateWaitTime = 100f;
    private float countTime=99;
   
    public enum EnemyType
    {
        Weak,
        Normal,
        Strong,
        Boss,
    }

    public override void Init()
    {
        base.Init();
        enemyModel = GetComponent<EnemyModel>();
        enemyModel.Init(this);
    }

    public override void GameUpdate()
    {
        base.GameUpdate();
        countTime += Time.unscaledDeltaTime;
        if(countTime > CreateWaitTime)
        {
            var taskEnemyCreate = new TaskManager.Task(0, TaskEnemyCreate, TaskManager.Task.Type.Time);
            this.TaskManager.Add(taskEnemyCreate);
            countTime = 0f;
        }
    }

    private void TaskEnemyCreate()
    {
        EnemyCreate(EnemyType.Weak);
    }

    private void EnemyCreate(EnemyType enemyType)
    {
        EnemyView enemyView = Instantiate(this.enemyObj,EnemyCreatePos(),Quaternion.identity)
                              .GetComponent<EnemyView>();

        switch (enemyType)
        {
            case EnemyType.Weak:
                enemyView.Init(this, this.enemyModel, weakShellSpeed, weakEnemyMoveSpeed,2);
                break;
            case EnemyType.Normal:
                enemyView.Init(this, this.enemyModel, normalShellSpeed, normalEnemyMoveSpeed,1);
                break;
            case EnemyType.Strong:
                enemyView.Init(this, this.enemyModel, strongShellSpeed, strongEnemyMoveSpeed,1);
                break;
            case EnemyType.Boss:
                enemyView.Init(this, this.enemyModel, weakShellSpeed, weakEnemyMoveSpeed,2);
                break;
            default:
                enemyView = null;
                break;
        }
        enemyViewList.Add(enemyView);
    }

    private Vector3 EnemyCreatePos()
    {
        Vector3 pos = new Vector3(Random.Range((int)-enemyCreatePos.position.x-10, (int)enemyCreatePos.position.x+10),
                                 enemyCreatePos.position.y,
                                 enemyCreatePos.position.z
                                 );
        return pos;

    }
}
