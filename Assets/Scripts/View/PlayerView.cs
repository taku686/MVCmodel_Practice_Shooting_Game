
using UnityEngine;


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
