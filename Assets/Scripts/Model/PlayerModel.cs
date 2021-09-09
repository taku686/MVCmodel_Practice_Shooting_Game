using UnityEngine;

public class PlayerModel : CharacterModel
{
    public const float ShotWaitTime = 0f;
    public const float MoveWaitTime = 0f;

    private PlayerView playerView;    // プレイヤービュークラスの参照変数
    private GameController gameController;
    public int shellSpeed = 10;
    public int moveSpeed = 10; 
    private float clickWaitTime = 0.2f;
    private float countTime;


    public void Init(GameController controller, PlayerView view,ShellController shellController)
    {
        this.gameController = controller;
        this.shellController = shellController;
        this.playerView = view;
        this.playerView.Init(controller, this);
    }

    // 更新処理
    public void Update()
    {
        countTime += Time.unscaledDeltaTime;
        // マウスが押された時、プレイヤー側の攻撃として実行する
        if (Input.GetKey(KeyCode.Space) && countTime > clickWaitTime)
        {
            var taskShot = new TaskManager.Task(ShotWaitTime, Shot, TaskManager.Task.Type.Time);
            this.gameController.TaskManager.Add(taskShot);
            countTime = 0;
        }


    }

    public void Shot()
    {
        ShellView shell = shellController.GetShell();
        //Debug.Log("Shell");
        //Debug.Log(shell == null);
        shell.shellRigidbody.velocity = new Vector3(0, 0, shellSpeed);
        shell.transform.position = shotPos.position;
    }

    // 敗北処理
    public void Lose()
    {
        playerView.Lose();
    }

    public bool OnClickButton()
    {
        if(Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
