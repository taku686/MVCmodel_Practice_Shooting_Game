using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : BaseController
{
    
   
    [SerializeField]
    private CameraView cameraView;     // カメラオブジェクト
   
    private ShellController shellController;
    
    private EnemyCreateController enemyCreateController;

    private PlayerController playerController;

    private CanvasController canvasController;

    private bool isGameStart = false;

    public int gameStartWaitTime = 3;

    public bool IsGameStart { get => isGameStart; set =>isGameStart =value; }

    // 初期化（BaseControllerより継承）
    public override void Init()
    {
        base.Init();

        shellController = GameObject.FindGameObjectWithTag("ShellController").GetComponent<ShellController>();
        enemyCreateController = GameObject.FindGameObjectWithTag("EnemyCreateController").GetComponent<EnemyCreateController>();
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        canvasController = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasController>();
        canvasController.StartButton.onClick.AddListener(() => StartCoroutine(GameStart()));
        // このInit処理で発生したGCを開放（ゲーム中のカクつきを防止）
        System.GC.Collect();
    }

    IEnumerator GameStart()
    {
        yield return StartCoroutine( canvasController.CountDown(r => isGameStart = r));
        if (isGameStart)
        {
            playerController.GameStart();
            //    enemyCreateController.GetPlayerTransform(playerController.playerView.transform);
        }
    }



    // メインループ処理（毎フレーム実行）
    public override void GameUpdate()
    {
       // Debug.Log(isGameStart);
        if (!isGameStart)
        {
            return;
        }
        base.GameUpdate();
        
    }

    void GameSet()
    {
     
    }

    // タイトル画面に戻す
    public void TitleBack()
    {
        this.LoadScene("Title");
    }
}
