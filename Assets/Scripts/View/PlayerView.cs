
using UnityEngine;

<<<<<<< HEAD
public class PlayerModel : CharacterModel
{

    
    PlayerView view;    // プレイヤービュークラスの参照変数
    public int shellSpeed = 10;
    public int playerSpeed = 10;
    public const float ShotWaitTime = 0f;
    public const float MoveWaitTime = 0f;
    private float waitTime = 0.2f;
    private float countTime;
=======
>>>>>>> 611a35310998388a0e33ad53c83ba6f01b3f7dfd

public class PlayerView : CharacterView
{
    PlayerModel model;
    GameController controller;

    public void Init(GameController controller, PlayerModel model)
    {
        this.model = model;
        this.controller = controller;
        this.Init();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            //   var taskMove = new TaskManager.Task(PlayerModel.MoveWaitTime, Move, TaskManager.Task.Type.Time);
            //   this.controller.TaskManager.Add(taskMove);
            Move();
        }
    }

    public override GameObject CreateShell()
    {
        GameObject shell = base.CreateShell();
        return shell;
    }

    public void Move()
    {

        float xVelocity = Input.GetAxis("Horizontal") * model.playerSpeed * Time.deltaTime;
        float zVelocity = Input.GetAxis("Vertical") * model.playerSpeed * Time.deltaTime;
        Debug.Log("xVelocity" + xVelocity);
        transform.position += new Vector3(xVelocity, 0, zVelocity);
    }
}
