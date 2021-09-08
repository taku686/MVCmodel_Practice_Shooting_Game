using UnityEngine;

public class PlayerModel : CharacterModel
{


    PlayerView view;    // プレイヤービュークラスの参照変数
    public int shellSpeed = 10;
    public int playerSpeed = 10;
    public const float ShotWaitTime = 0f;
    public const float MoveWaitTime = 0f;
    private float waitTime = 0.2f;
    private float countTime;


    public void Init(GameController controller, PlayerView view)
    {
        this.controller = controller;
        this.view = view;
        this.view.Init(controller, this);
    }

    // 更新処理
    public void Update()
    {
        countTime += Time.unscaledDeltaTime;
        // マウスが押された時、プレイヤー側の攻撃として実行する
        if (Input.GetKey(KeyCode.Space) && countTime > waitTime)
        {
            var taskShot = new TaskManager.Task(ShotWaitTime, Shot, TaskManager.Task.Type.Time);
            this.controller.TaskManager.Add(taskShot);
            countTime = 0;
        }


    }

    public void Shot()
    {
        GameObject shell = view.CreateShell();
        shell.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, shellSpeed);
    }





    // 敗北処理
    public void Lose()
    {
        view.Lose();
    }
}
