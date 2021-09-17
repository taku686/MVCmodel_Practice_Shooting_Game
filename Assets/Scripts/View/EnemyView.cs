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
    private CanvasView canvasView;

    public void Init(EnemyCreateController enemyCreateController,EnemyModel enemyModel,int shellSpeed,Vector3 moveSpeed,float shotWaitTime)
    {
        this.enemyModel = enemyModel;
        this.enemyCreateController = enemyCreateController;
        this.shellSpeed = shellSpeed;
        this.moveSpeed = moveSpeed;
        this.shotWaitTime = shotWaitTime;
        this.canvasView = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasView>();
    }

    private void Update()
    {
        //Debug.Log("活きてますよ");
        timeCount += Time.unscaledDeltaTime;
        var taskMove = new TaskManager.Task(EnemyCreateController.MoveWaitTime, Move, TaskManager.Task.Type.Time);
        this.enemyCreateController.TaskManager.Add(taskMove);
        if(timeCount > this.shotWaitTime)
        {
            var taskShot = new TaskManager.Task(EnemyCreateController.ShotWaitTime, Shot, TaskManager.Task.Type.Time);
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

    private void Dead()
    {
        enemyModel.Die(this.gameObject);
       int deadEnemyCount= enemyCreateController.deadEnemyCount++;
        //Debug.Log(deadEnemyCount);
        canvasView.DeadEnemy(deadEnemyCount);
    }

    private void OnTriggerEnter(Collider other)
    {
      ShellView shellView =  other.GetComponent<ShellView>();

        if(shellView == null)
        {
            return;
        }
        Dead();
       
    }
}
