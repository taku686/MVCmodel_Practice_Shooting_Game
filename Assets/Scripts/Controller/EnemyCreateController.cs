using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreateController : BaseController
{
    public const float MoveWaitTime = 0f;
    public const float ShotWaitTime = 0f;

    [SerializeField]
    private GameObject enemyZakoObj;
    [SerializeField]
    private GameObject enemyNormalObj;
    [SerializeField]
    private GameObject enemyStrongObj;
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
    
    public int deadEnemyCount = 1;
    private GameController gameController;

    public enum EnemyType
    {
        Weak,
        Normal,
        Strong,
        Boss,
    }

    private EnemyModel enemyModel;
    public List<EnemyView> enemyViewList = new List<EnemyView>();
    private const float CreateWaitTime = 3f;
    private float countTime=CreateWaitTime-1;
   
    

    public override void Init()
    {
        base.Init();
        enemyModel = GetComponent<EnemyModel>();
        enemyModel.Init();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public override void GameUpdate()
    {
        if (!gameController.IsGameStart)
        {
            return;
        }
        base.GameUpdate();
        countTime += Time.unscaledDeltaTime;
        if(countTime > CreateWaitTime)
        {
            var taskEnemyCreate = new TaskManager.Task(0, EnemyCreate, TaskManager.Task.Type.Time);
            this.TaskManager.Add(taskEnemyCreate);
            countTime = 0f;
        }
    }

    private void EnemyCreate()
    {
      //  if(deadEnemyCount %3== 0)
      //  {
            enemyModel.EnemyCreate(EnemyType.Strong, strongShellSpeed, strongEnemyMoveSpeed, this, enemyViewList, enemyStrongObj, enemyCreatePos);
        /*
        }
        else if(deadEnemyCount % 3 == 1)
        {
            enemyModel.EnemyCreate(EnemyType.Weak, weakShellSpeed, weakEnemyMoveSpeed, this, enemyViewList, enemyZakoObj, enemyCreatePos);
        }
        else if (deadEnemyCount % 3 == 2)
        {
            enemyModel.EnemyCreate(EnemyType.Normal, normalShellSpeed, normalEnemyMoveSpeed, this, enemyViewList, enemyNormalObj, enemyCreatePos);
        }
        */
    }

    public void GetPlayerTransform(Transform playerTransform)
    {
      //  enemyModel.playerTransform = playerTransform;
    }

    
  
}
