
using UnityEngine;


public class PlayerView : CharacterView
{
    PlayerModel playerModel;
    GameController gameController;

    public void Init(GameController controller, PlayerModel model)
    {
        this.playerModel = model;
        this.gameController = controller;
        this.Init();
    }

    private void Update()
    {
        if (playerModel.OnClickButton())
        {
              var taskMove = new TaskManager.Task(PlayerModel.MoveWaitTime, Move, TaskManager.Task.Type.Time);
              this.gameController.TaskManager.Add(taskMove);
        }
    }


    //後でPlayerModelに移しておく
    public void Move()
    {

        float xVelocity = Input.GetAxis("Horizontal") * playerModel.moveSpeed * Time.deltaTime;
        float zVelocity = Input.GetAxis("Vertical") * playerModel.moveSpeed * Time.deltaTime;
        //Debug.Log("xVelocity" + xVelocity);
        transform.position += new Vector3(xVelocity, 0, zVelocity);
    }


}
