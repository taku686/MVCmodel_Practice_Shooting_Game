
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


    // コンストラクタ
    public PlayerModel(GameController controller, PlayerView view)
    {
        this.controller = controller;
        this.view = view;
        this.view.Init(this);
    }

    public void Init()
    {

    }

    // 更新処理
    public void Update()
    {
        countTime += Time.unscaledDeltaTime;
        // マウスが押された時、プレイヤー側の攻撃として実行する
        if (Input.GetMouseButton(0) && countTime > waitTime)
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

    public void Move(Transform transform)
    {
        float xVelocity = Input.GetAxis("Horizontal") * playerSpeed;
        float zVelocity = Input.GetAxis("Vertical") * playerSpeed;

        // xVelocity = xVelocity > zVelocity ? xVelocity : 0;

        transform.position += new Vector3(xVelocity, 0, zVelocity);
    }



    // 敗北処理
    public void Lose()
    {
        view.Lose();
    }
}
public class PlayerView : CharacterView
{
    PlayerModel model;


    public void Init(PlayerModel model)
    {
        // PlayerModelの参照を入れる
        this.model = model;
        // CharacterView初期化
        this.Init();
    }

    public override GameObject CreateShell()
    {
        GameObject shell = base.CreateShell();
        return shell;
    }


}
