using UnityEngine;

public class EnemyView : CharacterView
{
    EnemyModel enemyModel;
    EnemyCreateController enemyCreateController;
    public int shellSpeed;
    public Vector3 moveSpeed;
    public float shotWaitTime;
    public float timeCount;
    [SerializeField]
    Transform shotPos;

    public void Init(EnemyCreateController enemyCreateController,EnemyModel enemyModel,int shellSpeed,Vector3 moveSpeed,float shotWaitTime)
    {
        this.enemyModel = enemyModel;
        this.enemyCreateController = enemyCreateController;
        this.shellSpeed = shellSpeed;
        this.moveSpeed = moveSpeed;
        this.shotWaitTime = shotWaitTime;
    }

    private void Update()
    {
        timeCount += Time.unscaledDeltaTime;
        var taskMove = new TaskManager.Task(PlayerModel.MoveWaitTime, Move, TaskManager.Task.Type.Time);
        this.enemyCreateController.TaskManager.Add(taskMove);
        if(timeCount > this.shotWaitTime)
        {
            var taskShot = new TaskManager.Task(PlayerModel.ShotWaitTime, Shot, TaskManager.Task.Type.Time);
            this.enemyCreateController.TaskManager.Add(taskShot);
            timeCount = 0f;
        }
    }

    private void Move()
    {
        enemyModel.Move(moveSpeed, transform);
    }

    private void Shot()
    {
        enemyModel.Shot(shellSpeed,shotPos);
    }

    public void Die()
    {

    }
}
