using UnityEngine;

public class GameController : BaseController
{

    // 定数リスト
    public const float GameMatchTime = 0.4f;    // 決着がついてから演出を開始する待機時間
    const float FoulExitWaitTime = 1.0f;        // お手付きしてからタイトルに戻るまでの待機時間
    const float ExitWaitTime = 3.0f;            // 決着がついてからタイトルに戻るまで待機時間

    // 攻撃を実行した側
    public enum Execution
    {
        None,   // なし
        Player, // プレイヤー
        Enemy,  // エネミー
    }

    // 誰が攻撃したか識別する変数
    Execution isExecution;
    public Execution IsExecution { get { return isExecution; } }

    bool isGameSet { get { return isExecution == GameController.Execution.None; } }     // 決着がついたかどうか

    private PlayerModel playerModel;    // プレイヤーモデル
    private EnemyModel enemyModel;     // エネミーモデル

    [SerializeField]
    private PlayerView playerView;     // プレイヤーオブジェクト
    [SerializeField]
    private EnemyView enemyView;      // エネミーオブジェクト
    [SerializeField]
    private CanvasView canvasView;     // UIのキャンバス
    [SerializeField]
    private CameraView cameraView;     // カメラオブジェクト
    [SerializeField]
    private GameObject playerObj;
   
    private ShellController shellController;
    
    private EnemyCreateController enemyCreateController;

    private Vector3 initPos = new Vector3(0, 1, -9);

    // 実行受付開始時刻
    float executionTime;
    public float ExecutionTime { get { return executionTime; } }

    // 初期化（BaseControllerより継承）
    public override void Init()
    {
        base.Init();

        // 合図を出す時間（3秒～5秒の間をランダム）
        executionTime = 3.0f + Random.Range(0.0f, 2.0f);

        // UIキャンバスの初期化
        //canvasView.Init(this, executionTime);

        // カメラの初期化
        //cameraView.Init(this);

        // 実行した側をリセット
        isExecution = Execution.None;

        GameObject playerObj = Instantiate(this.playerObj, initPos, Quaternion.identity);
        shellController = GameObject.FindGameObjectWithTag("ShellController").GetComponent<ShellController>();
        enemyCreateController = GameObject.FindGameObjectWithTag("EnemyCreateController").GetComponent<EnemyCreateController>();
        playerModel = playerObj.GetComponent<PlayerModel>();
        playerView = playerObj.GetComponent<PlayerView>();
        playerModel.Init(this, playerView, shellController);

        // このInit処理で発生したGCを開放（ゲーム中のカクつきを防止）
        System.GC.Collect();
    }

    // メインループ処理（毎フレーム実行）
    public override void GameUpdate()
    {
        base.GameUpdate();

        // 各Modelクラスの更新処理を実行
        playerModel.Update();
        //    enemyModel.Update();
    }

    /// <summary>
    /// キャラの攻撃を実行
    /// プレイヤーかエネミーが攻撃した時に呼ばれます。
    /// </summary>
    public void SetExecution(Execution execution)
    {
        // すでに実行されている場合、受け付けない
        if (IsExecution != GameController.Execution.None)
        {
            return;
        }

        // PlayerかEnemyが入ります
        isExecution = execution;

        // お手付きだった場合
        if (TaskManager.GameTime < ExecutionTime)
        {
            // 「反則」を表示
            canvasView.Out();

            // 追加されたタスクをすべて削除
            TaskManager.Clear();

            // お手付きとしてタイトルに戻すタスクを追加
            var taskTitleBack = new TaskManager.Task(FoulExitWaitTime, TitleBack, TaskManager.Task.Type.Time);
            TaskManager.Add(taskTitleBack);
            return;
        }

        // 勝敗処理実行
        GameSet();
    }

    // 勝敗が決まった時に呼ばれます。
    void GameSet()
    {
        // 各オブジェクトの勝敗処理
        //playerModel.GameSet();
        //enemyModel.GameSet();
        cameraView.GameSet();
        canvasView.GameSet();

        // 一定時間後にタイトル画面に戻るタスク追加
        var taskTitleBack = new TaskManager.Task(ExitWaitTime, TitleBack, TaskManager.Task.Type.Time);
        TaskManager.Add(taskTitleBack);
    }

    // タイトル画面に戻す
    public void TitleBack()
    {
        this.LoadScene("Title");
    }
}
