using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController :BaseController
{
    public const float ShotWaitTime = 0.2f;
    public const float MoveWaitTime = 0f;
    public const float PlayerCreateWaitTime = 0f;

    
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private Transform playerCreatePos;
    [SerializeField]
    private int moveSpeed;
    [SerializeField]
    private int shellSpeed;
    [SerializeField]
    private Material shellMaterial;

    private PlayerModel playerModel;
    [SerializeField]
    private PlayerView playerView;
    private float clickWaitTime = 0.2f;
    private float countTime;
    private GameController gameController;


    public override void Init()
    {
        base.Init();
        playerModel = GetComponent<PlayerModel>();
        playerModel.Init();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void GameStart()
    {
        Debug.Log("PlayerController GameStart");
        var taskCreatePlayer = new TaskManager.Task(PlayerCreateWaitTime, CreatePlayer, TaskManager.Task.Type.Time);
        this.TaskManager.Add(taskCreatePlayer);
    }

    public override void GameUpdate()
    {
        base.GameUpdate();
        if (!gameController.IsGameStart)
        {
            return;
        }
        countTime += Time.unscaledDeltaTime;
        
        if (Input.GetKey(KeyCode.Space) && countTime > clickWaitTime)
        {

            var taskShot = new TaskManager.Task(ShotWaitTime, playerView.Shot, TaskManager.Task.Type.Time);
            this.TaskManager.Add(taskShot);
            countTime = 0;
        }
        if (playerModel.OnClickButton())
        {
            
            var taskMove = new TaskManager.Task(MoveWaitTime, playerView.Move, TaskManager.Task.Type.Time);
            this.TaskManager.Add(taskMove);
        }
        
        
    }

    private void CreatePlayer()
    {
        GameObject player = playerModel.CereatePlayer(playerObj, playerCreatePos);
        playerView = player.GetComponent<PlayerView>();
        Debug.Log("PlayerView");
        Debug.Log(playerView == null);
        playerView.Init(playerModel, shellSpeed, moveSpeed,shellMaterial);    
    }
}
